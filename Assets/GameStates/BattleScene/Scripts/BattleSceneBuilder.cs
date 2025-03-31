using System.Collections;
using UnityEngine;

public class BattleSceneBuilder : MonoBehaviour
{
    public static BattleSceneBuilder instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicate BattleSceneBuilder detected");
        }
    }
    public IEnumerator BuildScene()
    {
        GridConstrution  grid =  GridCellManager.instance.gridConstrution;
        foreach (Cell cell in grid.gridArray)
        {
            if (cell.cellStartingObject  != CellContainsEnum.none)
            {
                if (cell.cellStartingObject == CellContainsEnum.constructionCore)
                {
                    SceneObjectConstructionBase constructionBaseSceneObject = SceneObjectConstructionBase.Create(new Vector3(cell.x + grid.cellSize / 2, cell.z + grid.cellSize / 2, 0f));
                    yield return null;
                }
            }
        }
        
        yield return null;
    }
}