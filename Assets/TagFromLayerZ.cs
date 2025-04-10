using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class TagFromLayerZ : MonoBehaviour
{
    [System.Serializable]
    public class LayerToAstarTag
    {
        public string unityLayerName;
        public string astarTagName;
        public int priority;
    }

    public static TagFromLayerZ instance;

    public LayerMask layerMask;
    public LayerToAstarTag[] layerToTagMappings;

    private Dictionary<int, uint> layerToAstarTag = new();
    private Dictionary<int, int> layerToPriority = new();
    private bool initialized = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void Initialize()
    {
        var astarTags = AstarPath.FindTagNames();
        layerToAstarTag.Clear();
        layerToPriority.Clear();

        foreach (var entry in layerToTagMappings)
        {
            int layer = LayerMask.NameToLayer(entry.unityLayerName);
            bool found = false;

            for (uint i = 0; i < astarTags.Length; i++)
            {
                if (astarTags[i] == entry.astarTagName)
                {
                    layerToAstarTag[layer] = i;
                    layerToPriority[layer] = entry.priority;
                    found = true;
                    break;
                }
            }
            Debug.Log($"LayerMask value: {layerMask.value}");

            for (int i = 0; i < 32; i++)
            {
                if ((layerMask.value & (1 << i)) != 0)
                {
                    Debug.Log($"✅ Layer {i}: {LayerMask.LayerToName(i)} is INCLUDED in layerMask");
                }
            }
            if (!found)
                Debug.LogWarning($"A* tag '{entry.astarTagName}' not found.");
        }
        foreach (var kvp in layerToPriority)
        {
            Debug.Log($"Layer {LayerMask.LayerToName(kvp.Key)} → Priority {kvp.Value}, Tag {layerToAstarTag[kvp.Key]}");
        }

        initialized = true;
    }

    public void UpdateGraphAndTags()
    {
        AstarPath.active.Scan();
        AssignTags();
    }

    public void UpdateGraphAndTags(Bounds bounds)
    {
        AstarPath.active.UpdateGraphs(bounds);
        AssignTags(bounds);
    }

    public void AssignTags(Bounds? bounds = null)
    {
        if (!initialized) return;

        var graph = AstarPath.active.data.gridGraph;
        List<Cell> cells;

        if (bounds == null || bounds.Value.size == Vector3.zero)
        {
            // Full scene scan
            cells = GridCellManager.instance.gridConstrution.GetCellList();
        }
        else
        {
            // Partial update
            Bounds b = bounds.Value;
            cells = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(
                b.center,
                Mathf.CeilToInt(b.size.x),
                Mathf.CeilToInt(b.size.y)
            );
        }

        if (cells == null) return;

        int updated = 0;

        foreach (var cell in cells)
        {
            if (cell == null) continue;

            Vector3 worldPos = GridCellManager.instance.gridConstrution.GetWorldPosition(cell.x, cell.z);
            worldPos.z = 0f;

            var hits = Physics2D.OverlapBoxAll(worldPos, new Vector2(0.1f, 0.1f), 0f, layerMask);

            Collider2D best = null;
            int bestPriority = int.MinValue;

            foreach (var hit in hits)
            {
                int layer = hit.gameObject.layer;

                if (layerToAstarTag.TryGetValue(layer, out uint _) &&
                    layerToPriority.TryGetValue(layer, out int priority))
                {
                    if (priority > bestPriority)
                    {
                        best = hit;
                        bestPriority = priority;
                    }
                }
            }

            var node = graph.GetNearest(worldPos).node;

            if (best != null && node != null)
            {
                node.Tag = layerToAstarTag[best.gameObject.layer];
                updated++;
            }
            else if (node != null)
            {
                node.Tag = 0; // fallback
            }
        }

        Debug.Log($"🏁 Assigned tags to {updated} nodes {(bounds == null ? "across entire graph" : "in updated region")}.");
    }



  

}
