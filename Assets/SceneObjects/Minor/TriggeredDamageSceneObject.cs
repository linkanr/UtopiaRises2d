using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TriggeredDamageSceneObject : MinorSceneObjects, IDamageAble,IStepable
{
    private Cell containingCell;
    private bool triggered = false;

    public SceneObjectTypeEnum damageableType => SceneObjectTypeEnum.minorObject;

    public IDamagableComponent iDamageableComponent { get; set; }
   

    public void Init(Vector3 pos)
    {
        containingCell = GridCellManager.Instance.gridConstrution.GetCellByWorldPosition(pos);
        containingCell.AddSceneObjects(this);
        TimeActions.GlobalTimeChanged += CheckTrigger;

    }

    public void CheckTrigger(BattleSceneTimeArgs args)
    {

        if (GetStats().takesDamageFrom.destroyedWhenStepedOn && !triggered)
        {

            if (containingCell.containingSceneObjects.Count > 0)
            {

                foreach (SceneObject sceneObject in containingCell.containingSceneObjects)
                {
                  
                    if (sceneObject.GetStats().sceneObjectType == SceneObjectTypeEnum.enemy)
                    {
      
                        Trigger();
                        return;
                    }
                }
            }
            else
            {
               
            }
        }
        else
        {
           
        }
    }

    private void Trigger()
    {
        triggered = true;

        var worldPosition = containingCell.worldPosition;
        var attackRange = GetStats().maxRange;
        var fireEffect = GetStats().fireEffect;
        var baseDamage = GetStats().damageAmount;

        AreaDamage.Create(worldPosition, attackRange, fireEffect, baseDamage, 0, burnChance: .5f);
        iDamageableComponent.TakeDamage((iDamageableComponent as IdamagablePhysicalComponent).healthSystem.health);
    }
    protected override void OnObjectDestroyedObjectImplementation()
    {
        base.OnObjectDestroyedObjectImplementation();

        Trigger();
        TimeActions.GlobalTimeChanged -= CheckTrigger;
        
        Destroy(gameObject);
    }
    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.minorObject);
        _stats.Add(StatsInfoTypeEnum.health, (iDamageableComponent as IdamagablePhysicalComponent).healthSystem.health);
    }


}
