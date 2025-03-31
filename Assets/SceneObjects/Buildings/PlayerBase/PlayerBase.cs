using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : StaticSceneObject

{
    public override void OnCreated()
    {
        
    }

    protected override void AddStatsForClick(Stats _stats)
    {
        
    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        Debug.LogWarning("game Over");
    }


}
