using UnityEngine;
using Pathfinding;
using System.Collections;
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

            if (!found)
                Debug.LogWarning($"⚠️ A* tag '{entry.astarTagName}' not found.");
        }

        initialized = true;
        Debug.Log($"✅ TagFromLayerZ initialized with {layerToAstarTag.Count} tag mappings.");
    }

    public void UpdateGraphAndTags()
    {
        AstarPath.active.Scan();
        AssignTags();
    }

    public void UpdateGraphAndTags(Bounds bounds)
    {
        AstarPath.active.UpdateGraphs(bounds);
        AstarPath.active.FlushGraphUpdates();
        AssignTags(bounds);
    }

    public void AssignTags(Bounds? bounds = null)
    {
        if (!initialized)
        {
            Debug.LogWarning("⚠️ TagFromLayerZ is not initialized.");
            return;
        }

        var graph = AstarPath.active.data.gridGraph;
        List<Cell> cells;

        if (bounds == null || bounds.Value.size == Vector3.zero)
        {
            cells = GridCellManager.instance.gridConstrution.GetCellList();
        }
        else
        {
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

            var hits = Physics2D.OverlapBoxAll(worldPos, new Vector2(0.3f, 0.3f), 0f, layerMask);

            Collider2D best = null;
            int bestPriority = int.MinValue;

            foreach (var hit in hits)
            {
                int layer = hit.gameObject.layer;

                if (layerToAstarTag.TryGetValue(layer, out uint tag) &&
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
                uint tag = layerToAstarTag[best.gameObject.layer];
                node.Tag = tag;
                updated++;
            }
        }

        Debug.Log($"🏁 Assigned tags to {updated} nodes {(bounds != null ? "in updated region" : "across entire graph")}.");
    }

    public void UpdateTagsWhenReady(Bounds bounds, Transform expectedObject, float checkRadius = 0.3f)
    {
        StartCoroutine(WaitAndTag(bounds, expectedObject, checkRadius));
    }

    private IEnumerator WaitAndTag(Bounds bounds, Transform expected, float radius)
    {
        Vector3 testPos = bounds.center;
        int maxTries = 10;

        for (int i = 0; i < maxTries; i++)
        {
            yield return null;
            Physics2D.SyncTransforms();

            var hits = Physics2D.OverlapBoxAll(testPos, new Vector2(radius, radius), 0f, layerMask);
            if (System.Array.Exists(hits, h => h.transform == expected))
            {
                Debug.Log($"✅ Collider for '{expected.name}' confirmed after {i + 1} frame(s). Assigning tags.");
                UpdateGraphAndTags(bounds);
                yield break;
            }
        }

        Debug.LogWarning($"❗ Tag update for '{expected.name}' timed out. Collider not detected.");
    }
}
