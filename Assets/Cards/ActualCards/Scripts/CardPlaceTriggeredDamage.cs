using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cards/TriggerDamage")]
public class CardPlaceTriggeredDamage : SoCardInstanciate
{



    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        failureReason = "";
        Cell centerCell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
        List<Cell> cells = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition(), sizeX, sizeY);
        if (cells.Count < 1)
        {
            failureReason = "NO CELL FOUND";
            return false;
        }





        foreach (Cell cell in cells)
        {

            if (CheckIfCardMathesTerrain(cell))
            {
                SceneObject sceneObject = prefab.Init(cell.worldPosition);
                SoTriggeredDamage soTriggeredDamage = prefab as SoTriggeredDamage;
                
            }

        }
        return true;

    }
}
