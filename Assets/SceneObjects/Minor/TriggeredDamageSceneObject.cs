using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TriggeredDamageSceneObject : MinorSceneObjects
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


        KillSceneObject();

    }
    protected override void OnObjectDestroyedObjectImplementation()
    {
        base.OnObjectDestroyedObjectImplementation();
        if (!triggered)
        {
            Trigger();
        }

        TimeActions.GlobalTimeChanged -= CheckTrigger;
        
        
    }
    protected override void TriggerDeathExplosionDamage()
    {

        var attackRange = GetStats().damageWhenDiedRadius;
        var fireEffect = GetStats().visualEffectWhenDestroyed;
        var baseDamage = GetStats().damageWhenDestroyed;
        Debug.Log("destroying trigger range fireeffect damage " + attackRange + " - " + fireEffect + " - " + baseDamage);

        AreaDamage.Create(transform.position, attackRange, fireEffect, baseDamage, 0, burnChance: .5f);
    }
    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.minorObject);
        _stats.Add(StatsInfoTypeEnum.health,healthSystem.GetHealth());
    }


}
