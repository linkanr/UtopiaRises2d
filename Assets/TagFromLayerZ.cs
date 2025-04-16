using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    private void OnEnable()
    {
       TimeActions.OnQuaterTick += AssignTags;
    }

    private void OnDisable()
    {
       TimeActions.OnQuaterTick -= AssignTags;
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
                Debug.LogWarning($"A* tag '{entry.astarTagName}' not found.");
        }

        initialized = true;
    }

    /// <summary>
    /// Performs a full scan of the graph and re-assigns all node tags.
    /// </summary>
    public void UpdateGraphAndTags()
    {
        if (!initialized) return;

        AstarPath.active.Scan();
        AssignTags();
    }

    /// <summary>
    /// Performs a local scan of the graph within the given bounds only.
    /// </summary>
    public void UpdateGraphLocally(Bounds bounds)
    {
        if (!initialized) return;

        AstarPath.active.UpdateGraphs(bounds);
        AstarPath.active.FlushGraphUpdates();
       

    }

    /// <summary>
    /// Assigns tags to nodes based on layer priorities. Called every tick.
    /// </summary>
    public void AssignTags()
    {
        if (!initialized)
        {
            Debug.LogWarning("⚠️ TagFromLayerZ is not initialized.");
            return;
        }

        var graph = AstarPath.active.data.gridGraph;
        if (graph == null)
        {
            Debug.LogWarning("⚠️ No GridGraph found.");
            return;
        }

        var cells = GridCellManager.instance.gridConstrution.GetCellList();
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
                node.Tag = 0;
            }
        }

        //Debug.Log($"🏁 Assigned tags to {updated} nodes across entire graph.");
    }
}
