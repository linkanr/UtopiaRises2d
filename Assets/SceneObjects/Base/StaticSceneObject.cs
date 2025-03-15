using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticSceneObject : SceneObject
{
    protected override void Start()
    {
        base.Start();


        transform.SetParent(GameObject.Find("PersistantParent").transform);

    }
    
}
