using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SoSceneObjectBase: SerializedScriptableObject
{
    public string sceneObjectName;
    public string description;
    public Sprite sprite;
    public SceneObject prefab;
    public Faction faction;
    public SceneObjectTypeEnum sceneObjectType;
    public TakesDamageFrom takesDamageFrom;

    public Stats GetStats() // this sets the base
    {
        Debug.Log("SoSceneObjectBase adding stats intial");
        var stats = new Stats();
        stats.Add(StatsInfoTypeEnum.name, sceneObjectName);
        stats.Add(StatsInfoTypeEnum.description, description);
        stats.Add(StatsInfoTypeEnum.Sprite, sprite);
        stats.Add(StatsInfoTypeEnum.sceneObjectType, sceneObjectType);
        stats.Add(StatsInfoTypeEnum.Faction, faction);
        stats.Add(StatsInfoTypeEnum.takesDamageMultiplier, 1f);
        stats.Add(StatsInfoTypeEnum.takesDamageFrom, takesDamageFrom);
        return GetStatsInernal(stats);

    }
    /// <summary>
    /// This creates the Sceneobject from the prefab
    /// </summary>
    /// <param name="position"></param>
    public SceneObject Init(Vector3 position)
    {
        Quaternion rotation = Quaternion.Euler(-0f,0f,0f);
        SceneObject sceneobjectFromCard = Instantiate(prefab, position, rotation);

        sceneobjectFromCard.SetStats(GetStats());

        sceneobjectFromCard.InitilizeFromSo();
        ObjectInitialization(sceneobjectFromCard);

        return sceneobjectFromCard;
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