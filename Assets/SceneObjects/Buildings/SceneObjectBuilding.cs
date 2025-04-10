using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public abstract class SceneObjectBuilding : StaticSceneObject
{

    public override void OnCreated()
    {
        List<Cell> celllist = new List<Cell>();
        celllist = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(transform.position, GetStats().influenceRadius, GetStats().influenceRadius);
        foreach (Cell cell in celllist)
        {
            cell.isPlayerInfluenced = true;
        }
        CellActions.OnGenerateHeatTexture?.Invoke(GridTypeEnum.influence);

    }


    protected override void OnObjectDestroyedObjectImplementation()
    {
        List<Cell> celllist = new List<Cell>();
        celllist = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(transform.position, GetStats().influenceRadius, GetStats().influenceRadius);
        foreach (Cell cell in celllist)
        {
            cell.isPlayerInfluenced = false;
            cell.CheckPlayerInfluence();
        }
        CellActions.OnGenerateHeatTexture?.Invoke(GridTypeEnum.influence);
       
    }



}