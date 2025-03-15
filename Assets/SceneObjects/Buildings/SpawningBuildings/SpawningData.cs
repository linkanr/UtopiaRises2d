using System.Collections.Generic;

[System.Serializable]
public class SpawningData
{
    public SoSceneObjectBase sceneObjectToSpawn;
    public List<CellTerrainEnum> canInstanceOn;

    public bool canSpawnOn(CellTerrainEnum cellTerrainEnum)
    {
        return canInstanceOn.Contains(cellTerrainEnum);
    }
}