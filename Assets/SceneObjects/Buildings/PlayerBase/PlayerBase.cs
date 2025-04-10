using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : StaticSceneObject


{

    public override void InitilizeFromSo()
    {
        sceneObjectPosition = transform.position;
        BattleSceneActions.OnSceneObjectCreated(this);


        OnCreated();

        spriteRenderer.sprite = GetStats().sprite;


   
            healthSystem = gameObject.AddComponent<PlayerBasePhysicalHealthSystem>();
            healthSystem.Init(GetStats().health, this);

        

    }
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
