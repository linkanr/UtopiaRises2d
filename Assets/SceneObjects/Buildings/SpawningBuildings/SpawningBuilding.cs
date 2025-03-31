using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningBuilding : SceneObjectBuilding, IcanSpawn
{
    public SpawningBuildingStateMachine stateMachine;
    public SoDamageDealNoDamage damageDealerSpawner { get ; set; }

    public void Spawn()
    {
        List<Cell> cells = GetCells();
        foreach (Cell cell in cells)
        {
            if (cell.contingingMinorObject == null && GetStats().spawningData.canSpawnOn(cell.cellTerrain.cellTerrainEnum))
            {
                

                SoTriggeredDamage soTriggeredDamage = GetStats().spawningData.sceneObjectToSpawn as SoTriggeredDamage;
                TriggeredDamageSceneObject sceneObject = soTriggeredDamage.Init(cell.worldPosition) as TriggeredDamageSceneObject;
              

                break;
            }
        }
    }

    protected List<Cell> GetCells()
    {
        List<Cell> cells = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(transform.position, (int)GetStats().maxRange, (int)GetStats().maxRange);
        GeneralUtils.ShuffleList(cells);
        return cells;
    }
    public override void OnCreated()
    {
        base.OnCreated();
        stateMachine = GetComponent<SpawningBuildingStateMachine>();
    }

}