using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BridgeSceneObject : EnviromentObject
{
    private bool triggered = false;
    private bool checkedForTrigger = false;
    private bool connectedToLand = false;

    public BridgeSceneObject Create(Vector3 position)
    {
        return SceneObjectInstanciator.instance.Execute("bridge", position) as BridgeSceneObject;
    }

    private void OnEnable()
    {
        TimeActions.GlobalTimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        TimeActions.GlobalTimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(BattleSceneTimeArgs args)
    {
        // Reset per tick
        checkedForTrigger = false;
        connectedToLand = false;
        CheckTrigger();
    }

    public void CheckTrigger()
    {
        if (checkedForTrigger || triggered)
            return;

        checkedForTrigger = true;

        if (!IsConnectedToLand(new HashSet<BridgeSceneObject>()))
        {
            Trigger();
        }
    }

    private void Trigger()
    {
        if (triggered) return;

        triggered = true;

        KillSceneObject(); // This handles the destruction + visuals
    }

    protected override visualEffectsEnum GetDeathEffect()
    {
        return visualEffectsEnum.bridge;
    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        base.OnObjectDestroyedObjectImplementation();
        if (!triggered)
            Trigger();

        TimeActions.GlobalTimeChanged -= OnTimeChanged;
    }

    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.health, healthSystem.GetHealth());
    }

    private bool IsConnectedToLand(HashSet<BridgeSceneObject> visited)
    {
        if (visited.Contains(this))
            return false;

        visited.Add(this);

        Cell cell = GridCellManager.instance.gridConstrution.GetCellByWorldPosition(transform.position);
        foreach (Cell neighbor in cell.neigbours)
        {
            if (neighbor.cellTerrain.cellTerrainEnum != CellTerrainEnum.water)
            {
                connectedToLand = true;
                return true;
            }

            foreach (SceneObject so in neighbor.containingSceneObjects)
            {
                if (so is BridgeSceneObject bridge && bridge != this)
                {
                    if (bridge.IsConnectedToLand(visited))
                    {
                        connectedToLand = true;
                        return true;
                    }
                }
            }
        }

        connectedToLand = false;
        return false;
    }
}
