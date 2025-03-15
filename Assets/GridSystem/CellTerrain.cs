using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Terrain/BasicTerrain")]
public class CellTerrain:ScriptableObject,IDisposable
{

    public CellTerrainEnum cellTerrainEnum;
    /// <summary>
    /// This should be 0 or 1 where 0 represents the first terrain and 1 represents the second terrain
    /// </summary>
    public int binaryInList;

    public float walkPenalty;
    public float damageMulti;
    public bool canHaveTower;
    public float generateHeat;
    public float generateHumidity;
    public float fuel;
    public float chanceOfCatchingFire;
    public float heatTransferAmount;


    public void Dispose()
    {
        Destroy(this);
    }
}