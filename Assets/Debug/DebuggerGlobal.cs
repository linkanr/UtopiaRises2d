using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Diagnostics;

public class DebuggerGlobal : MonoBehaviour
{
    public bool drawTargetLines;
    public bool debugSceneObejcts;
    public static DebuggerGlobal instance;
    public Sprite spriteMouse;
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
            //MouseDisplayManager.OnSetNewSprite(spriteMouse);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            MouseDisplayManager.OnRemoveDisplay();
        }


        if (debugSceneObejcts)
        {
            foreach (IDamageable s in SceneObjectManager.Instance.iDamagablesInScene)
            {
                Debug.Log(s.sceneObject.GetStats().GetString(StatsInfoTypeEnum.name)+  "is in manager");
                if (s.GetTransform() == null)
                {
                    Debug.Log( s.sceneObject.GetStats().GetString(StatsInfoTypeEnum.name)+ "is missing transform");
                }
            }
        }
    }
}
