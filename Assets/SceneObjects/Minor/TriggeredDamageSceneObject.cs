using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TriggeredDamageSceneObject : MinorSceneObjects, IStepable
{

    private bool triggered = false;

    public SceneObjectTypeEnum damageableType => SceneObjectTypeEnum.minorObject;




    private void OnEnable()
    {
        TimeActions.GlobalTimeChanged += CheckTrigger;
    }
    private void OnDisable()
    {
        TimeActions.GlobalTimeChanged -= CheckTrigger;
    }

    public void CheckTrigger(BattleSceneTimeArgs args)
    {
    

        if (GetStats().takesDamageFrom.destroyedWhenStepedOn && !triggered)
        {
        
            List<SceneObject> sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(transform.position, objectTypeEnum: SceneObjectTypeEnum.enemy, maxDistance: 1f);
            if (sceneObjects.Count > 0)
            {
              
                foreach (SceneObject sceneObject in sceneObjects)
                {
                    if (sceneObject.GetStats().sceneObjectType == SceneObjectTypeEnum.enemy)
                    {
                       
                        Trigger();
                    }
                }
            }


        }
    }

    private void Trigger()
    {
        triggered = true;


        var attackRange = GetStats().maxRange;
        var fireEffect = GetStats().fireEffect;
        var baseDamage = GetStats().damageAmount;

        AreaDamage.Create(transform.position, attackRange, fireEffect, baseDamage, 0, burnChance: .5f);
        
    }
    protected override void OnObjectDestroyedObjectImplementation()
    {
        base.OnObjectDestroyedObjectImplementation();

        Trigger();
        TimeActions.GlobalTimeChanged -= CheckTrigger;
        
        
    }
    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.minorObject);
        _stats.Add(StatsInfoTypeEnum.health,healthSystem.GetHealth());
    }


}
