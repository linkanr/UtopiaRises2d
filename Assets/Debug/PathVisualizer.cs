using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System;
using System.Collections;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(LineRenderer))]
public class AIPathVisualizer : MonoBehaviour
{
    private AIPath aiPath;
    private LineRenderer line;
    private List<Vector3> pathPoints = new List<Vector3>();
    private bool on;
    // Track previous state to know when a new path is calculated or completed
    private bool prevHadPath = false;
    private bool prevPathPending = false;

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;  // hide line until a path is ready
    }
    private void Start()
    {
        GetComponent<Enemy>().aIPathVisualizer = this;
        ActivateLine(false);
    }
    public void ActivateLine(bool active)
    {
        ToggleLine(active);
       

    }

    private Coroutine lineCoroutine;  // Store coroutine reference to avoid multiple instances

    private IEnumerator ActivateLineCoroutine(bool active)
    {
        yield return new WaitForSeconds(.5f);  // Wait until the next frame (better than WaitForEndOfFrame in this case)

        on = active;
        lineCoroutine = null;  // Clear coroutine reference once finished
        
    }

    // Call this method to toggle the line safely
    private void ToggleLine(bool active)
    {
        if (lineCoroutine != null)
        {
            StopCoroutine(lineCoroutine);  // Prevent multiple coroutines stacking up
        }
        lineCoroutine = StartCoroutine(ActivateLineCoroutine(active));
    }

    void Update()
    {
        if (!on)
        {
            line.enabled = false;
            return;
        }
        
        float speed = GetComponent<SceneObject>().GetStats().speed;
        line.material.SetFloat("_speed", speed);
        // Current path status flags
        bool hasPath = aiPath.hasPath;                       // True if there's a path to follow&#8203;:contentReference[oaicite:4]{index=4}
        bool reachedDest = aiPath.reachedDestination;        // True if agent has reached its destination&#8203;:contentReference[oaicite:5]{index=5}
        bool pathPending = aiPath.pathPending;               // True if a path calculation is in progress (repathing)

        // 1. Handle path availability or recalculation events
        if (hasPath && !pathPending)
        {
            // If a new path was just obtained (either no path before, or a recalculation finished)
            if (!prevHadPath || prevPathPending || !line.enabled)
            {
                // Fetch the current path waypoints using the public GetRemainingPath method
                aiPath.GetRemainingPath(pathPoints, out bool stale);  // Fills pathPoints with the remaining path&#8203;:contentReference[oaicite:6]{index=6}
                if (!stale && pathPoints.Count > 0)
                {
                    // Update LineRenderer with new path points
                    line.positionCount = pathPoints.Count;
                    for (int i = 1; i < pathPoints.Count; i++)
                    {
                        Vector3 offset = new Vector3(0, 0, -.1f);
                        line.SetPosition(i, pathPoints[i]+ offset);
                    }
                    line.enabled = true;
                }
                else
                {
                    // No valid path (stale or zero points) – clear the line
                    line.positionCount = 0;
                    line.enabled = false;
                }
            }
            else
            {
                // (Optional) Update the first point to the agent's current position for smoother real-time movement
                if (line.enabled && line.positionCount > 0)
                {
                    line.SetPosition(0, aiPath.transform.position);
                }
            }

        }

        // 2. Handle reaching destination or no path
        if (!hasPath || reachedDest)
        {
            // If the agent has no path or has arrived, clear or disable the line visualization
            line.positionCount = 0;
            line.enabled = false;
        }

        // Update the previous state trackers for the next frame
        prevHadPath = hasPath;
        prevPathPending = pathPending;
        line.enabled = true;
    }
}
