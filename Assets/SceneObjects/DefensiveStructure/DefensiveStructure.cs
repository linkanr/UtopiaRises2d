using System;
using System.Collections.Generic;

public class DefensiveStructure : SceneObjectBuilding
{

    protected override void Start()
    {
        base.Start();
        TimeActions.OnQuaterTick += OnGlobalTimeChanged;

    }
    private void OnDisable()
    {
        TimeActions.OnQuaterTick-= OnGlobalTimeChanged;
    }

    private void OnGlobalTimeChanged()
    {
        List<SceneObject> sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(transform.position, objectTypeEnum: SceneObjectTypeEnum.enemy, maxDistance: GetStats().maxRange);
        foreach (Enemy enemy in sceneObjects)
        {
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
        Destroy(gameObject);
    }

    protected override void AddStatsForClick(Stats _stats)
    {
       
    }
}