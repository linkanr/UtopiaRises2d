using UnityEngine;
[CreateAssetMenu(menuName = "Terrain/BasicTerrain")]
public class CellTerrain:ScriptableObject
{

    public CellTerrainEnum cellTerrainEnum;
    /// <summary>
    /// This should be 0 or 1 where 0 represents the first terrain and 1 represents the second terrain
    /// </summary>
    public int binaryInList;
    public bool isWalkable;
    public int walkPenalty;
    public float damageMulti;
    public bool canHaveTower;
        

}