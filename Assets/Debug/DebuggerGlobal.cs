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
    public static void DrawLine(Vector3 start, Vector3 end)
    {
        Debug.DrawLine(start, end, Color.blue, .1f);
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawLine(start, end, color, .1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           SceneObject s =SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(WorldSpaceUtils.GetMouseWorldPosition());

           Debug.Log("adding rage " + s.GetStats().name);
           EffectAdd.AddEffect(PickupTypes.Rage, s,5f);
   
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            EffectAdd.AddEffect(PickupTypes.Slow, SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(WorldSpaceUtils.GetMouseWorldPosition()), 2f);

        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(WorldSpaceUtils.CheckClickableType().ToString());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AstarPath.active.Scan();
        }




        if (debugSceneObejcts && Input.GetKeyDown(KeyCode.Z))
        {
            foreach (SceneObject s in SceneObjectManager.Instance.sceneObjectsInScene)
            {
                Debug.Log(s.GetStats().GetString(StatsInfoTypeEnum.name)+  "is in manager");
                if (s.transform == null)
                {
                    Debug.Log( s.GetStats().GetString(StatsInfoTypeEnum.name)+ "is missing transform");
                }
            }
        }
    }
}
