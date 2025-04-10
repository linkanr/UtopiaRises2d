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

            if (!string.IsNullOrWhiteSpace(cell.cellStartingObject))
            {
             //   Debug.Log (cell.cellStartingObject + " is starting object");
                //Debug.Log($"[BattleSceneBuilder] Building scene object: {cell.cellStartingObject} at {cell.worldPosition}");
                SceneObject sceneObject = SceneObjectInstanciator.instance.Execute(cell.cellStartingObject, cell.worldPosition); 
            }
        }
        
        
        yield return null;
    }
}