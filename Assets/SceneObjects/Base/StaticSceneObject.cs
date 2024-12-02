using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticSceneObject : SceneObject
{
    protected override void Start()
    {
        base.Start();

        rB2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

}
