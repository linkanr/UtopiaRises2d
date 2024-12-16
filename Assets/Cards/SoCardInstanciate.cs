using UnityEngine;



public abstract class SoCardInstanciate : SoCardBase

{
    public SoSceneObjectBase prefab;


    public override void ActualEffect(Vector3 position)
    {
        
        Cell cell = GridCellManager.Instance.gridConstrution.GetCellByWorldPostion(position);

        if (cell == null)
        {
            return;
        }

        Vector3 cellPos = GridCellManager.Instance.gridConstrution.GetWorldPostion(cell.x, cell.y);
        Vector3 cellSizeOffset = new Vector3(cell.size / 2, cell.size / 2, 0f);
        cellPos += cellSizeOffset;
        prefab.Init(cellPos);

    }
}