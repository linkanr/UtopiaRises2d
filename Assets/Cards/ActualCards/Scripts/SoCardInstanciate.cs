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
        List<Cell> cells = GridCellManager.Instance.gridConstrution.GetCellListByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition(), sizeX,sizeY);
        if (cells.Count < 1)
        {
            failureReason = "No ground to play on found";
            return false;
        }

        foreach (Cell cell in cells) 
        {
            if (cell.hasSceneObejct)
            {
                failureReason = ("objects in the way");
                return false;
            }


      
         
        }
        foreach (Cell cell in cells)
        {
            prefab.Init(cell.worlPosition);
        }
        return true;





    }


}