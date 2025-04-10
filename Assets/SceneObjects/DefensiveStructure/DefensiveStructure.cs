using System;
using System.Collections.Generic;
using UnityEngine;


public class DefensiveStructure : SceneObjectBuilding
{


    private void OnDisable()
    {
        TimeActions.OnQuaterTick-= OnGlobalTimeChanged;
    }

    private void OnGlobalTimeChanged()
    {
        Debug.Log("OnGlobalTimeChanged");
        Debug.Log("reach " + GetStats().maxRange);
        List<SceneObject> sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(transform.position, objectTypeEnum: SceneObjectTypeEnum.enemy, maxDistance: GetStats().maxRange);
        foreach (Enemy enemy in sceneObjects)
        {
            Debug.Log("enemy " + enemy.GetStats().name);
            TargeterBaseClass targeter = enemy.targeter;
            if (targeter != null)
            {
                targeter.SetNewTarget(this);
            }
        }
    }
    protected override void OnObjectDestroyedObjectImplementation()
    {

        TimeActions.OnQuaterTick -= OnGlobalTimeChanged;
        
    }

    protected override void AddStatsForClick(Stats _stats)
    {
       
    }

    public override void OnCreated()
    {
        TimeActions.OnQuaterTick += OnGlobalTimeChanged;
    }
}