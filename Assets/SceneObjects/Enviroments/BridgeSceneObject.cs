using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BridgeSceneObject : EnviromentObject, IStepable
{

    private bool triggered = false;
    public BridgeSceneObject Create(Vector3 position)
    {
        BridgeSceneObject bridgeSceneObject = SceneObjectInstanciator.instance.Execute("bridge", position) as BridgeSceneObject;
        return bridgeSceneObject;
    }
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
        //Debug.Log("CheckTrigger");
        if (!IsConnectedToLand() && !triggered)
        {
          //  Debug.Log("Triggering because not connected to land.");
            Trigger();
        }
    }

    private void Trigger()
    {
        if (triggered)
        {
            return;
        }
        triggered = true;


        var attackRange = GetStats().maxRange;
        var baseDamage = GetStats().damageAmount;

        List<SceneObject> sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(transform.position, objectTypeEnum: SceneObjectTypeEnum.enemy, maxDistance: attackRange);
        foreach (SceneObject sceneObject in sceneObjects)
        {
            sceneObject.GetComponent<HealthSystem>().TakeDamage(baseDamage, this);
        }
        GetComponent<HealthSystem>().Die(null);
    }
    protected override visualEffectsEnum GetDeathEffect()
    {
        return visualEffectsEnum.bridge;
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
    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.health, healthSystem.GetHealth());
    }
    private bool IsConnectedToLand(HashSet<BridgeSceneObject> visited = null)
    {
        if (visited == null)
            visited = new HashSet<BridgeSceneObject>();

        if (visited.Contains(this))
            return false;

        visited.Add(this);

        Cell cell = GridCellManager.instance.gridConstrution.GetCellByWorldPosition(transform.position);
        foreach (Cell neighbor in cell.neigbours)
        {
            if (neighbor.cellTerrain.cellTerrainEnum != CellTerrainEnum.water)
                return true;

            foreach (SceneObject so in neighbor.containingSceneObjects)
            {
                if (so is BridgeSceneObject bridge && bridge != this)
                {
                    if (bridge.IsConnectedToLand(visited))
                        return true;
                }
            }
        }

        return false;
    }

}