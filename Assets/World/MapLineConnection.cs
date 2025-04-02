using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLineConnection : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public MapNode startNode;
    public MapNode endNode;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
}
