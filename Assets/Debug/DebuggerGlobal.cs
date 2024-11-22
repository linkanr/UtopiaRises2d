using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Diagnostics;

public class DebuggerGlobal : MonoBehaviour
{
    public bool drawTargetLines;
    public static DebuggerGlobal instance;
    private void Awake()
    {
        instance = this;
    }
    public void DrawLine(Vector3 start, Vector3 end)
    {
        Debug.DrawLine(start, end, Color.blue, .1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AstarPath.active.Scan();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            EnviromentObject.Create(ObjectTypeEnums.stone, WorldSpaceUtils.GetMouseWorldPosition());
        }
    }
}
