using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SoSceneObjectBase: SerializedScriptableObject
{
    public string sceneObjectName;
    public string description;
    public SceneObject prefab;

    public Stats GetStats() // this sets the base
    {
        var stats = new Stats();
        stats.Add(StatsInfoTypeEnum.name, sceneObjectName);
        stats.Add(StatsInfoTypeEnum.description, description);

        return GetStatsInernal(stats);

    }
    /// <summary>
    /// This creates the Sceneobject from the prefab
    /// </summary>
    /// <param name="position"></param>
    public SceneObject Init(Vector3 position)
    {
        SceneObject createdFromCard = Instantiate(prefab, position, Quaternion.identity);

        createdFromCard.SetStats(GetStats());
        ObjectInitialization(createdFromCard);
        return createdFromCard;
    }
    /// <summary>
    /// This is called after stats are added to the sceneobject. Use this to specific initialization
    /// The scene object is the one that is beeing instaticated so attach info to ir
    /// </summary>
    /// <param name="sceneObject"></param>
    protected abstract void ObjectInitialization(SceneObject sceneObject);

    /// <summary>
    /// This is the specifics of the stats that should be added from the SoBase
    /// </summary>
    /// <param name="stats"></param>
    /// <returns></returns>
    protected abstract Stats GetStatsInernal(Stats stats);// this set specific for each bilding
}