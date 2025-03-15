using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;



public abstract class SoCardInstanciate : SoCardBase

{
    public SoSceneObjectBase prefab;
    public int sizeX;
    public int sizeY;




    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        failureReason = "";
        Cell centerCell = GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse();
        List<Cell> cells = GridCellManager.Instance.gridConstrution.GetCellListByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition(), sizeX, sizeY);
        if (cells.Count < 1)
        {
            failureReason = "NO CELL FOUND";
            return false;
        }
       



        
        foreach (Cell cell in cells)
        {
            if (CheckIfCardMathesTerrain(cell))
            {
                prefab.Init(cell.worldPosition);
            }

        }
        return true;





    }

    
}