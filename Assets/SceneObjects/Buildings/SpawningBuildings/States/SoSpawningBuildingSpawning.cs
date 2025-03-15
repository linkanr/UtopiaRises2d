using System;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/building/SpawningBuildings/Spawning")]
public class SoSpawningBuildingSpawning : BaseState<SpawningBuildingStateMachine>
{
    public float timer = 0;
    public override void OnObjectDestroyed()
    {
        TimeActions.GlobalTimeChanged -= OnGlobalTimeChanged;
    }

    public override void OnStateEnter()
    {
        timer = 0;
        TimeActions.GlobalTimeChanged += OnGlobalTimeChanged;
    }

    public override void OnStateExit()
    {
        TimeActions.GlobalTimeChanged -= OnGlobalTimeChanged;
    }

    private void OnGlobalTimeChanged(BattleSceneTimeArgs args)
    {
        timer += args.deltaTime;
    }

    public override void OnStateUpdate()
    {
        if (timer >= stateMachine.spawningBuilding.GetStats().reloadTime)
        {
            stateMachine.spawningBuilding.Spawn();
            timer = 0;
        }
    }
}