using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneObjectConstructionBase : StaticSceneObject
{

    protected override void AddStatsForClick(Stats _stats)
    {
      
    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        
    }

    public override void OnCreated()
    {

        transform.SetParent(GameSceneRef.instance.constructionBaseParent);
        List<Cell> celllist = new List<Cell>();
        celllist = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(transform.position, GetStats().influenceRadius, GetStats().influenceRadius);
        foreach (Cell cell in celllist)
        {
            cell.isPlayerInfluenced = true;
        }
    }
}   