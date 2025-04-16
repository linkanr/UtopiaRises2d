using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
/// <summary>
/// Represents the stats of a game object, including various attributes and methods to manipulate them.
/// </summary>
public class Stats
{
    public Stats()
    {
        statsInfoDic = new StatsInfoDic();
        statsMediator = new StatsMediator(this);
        statsInfoDic.OnStatsChanged += StatsChanged;
    }

    private void StatsChanged()
    {
        OnStatsChanged?.Invoke();
    }
    public Action OnStatsChanged;
    /// <summary>
    /// Dictionary to store stats information.
    /// </summary>
    public StatsInfoDic statsInfoDic;

    /// <summary>
    /// Mediator to handle stats modifications.
    /// </summary>
    public StatsMediator statsMediator;

    /// <summary>
    /// Adds stats from another Stats object to this one.
    /// </summary>
    /// <param name="_stats">The Stats object to add from.</param>
    public void AddStats(Stats _stats)
    {
        foreach (KeyValuePair<StatsInfoTypeEnum, object> statsObject in _stats.statsInfoDic)
        {
            statsInfoDic.Add(statsObject.Key, statsObject.Value);
        }
    }

    /// <summary>
    /// Adds a stat to the dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the stat value.</typeparam>
    /// <param name="key">The key of the stat.</param>
    /// <param name="value">The value of the stat.</param>
    public void Add<T>(StatsInfoTypeEnum key, T value)
    {
        if (key == StatsInfoTypeEnum.damageAmount || key == StatsInfoTypeEnum.reloadTime || key == StatsInfoTypeEnum.maxRangeForShotingSpawning)
        {
            Debug.LogError("use the damager class to set the damage amount, reload time or max shooting distance");
        }
        statsInfoDic.Add(key, value);
    }

    /// <summary>
    /// Gets the value of a stat.
    /// </summary>
    /// <typeparam name="T">The type of the stat value.</typeparam>
    /// <param name="key">The key of the stat.</param>
    /// <returns>The value of the stat.</returns>
    public T GetValue<T>(StatsInfoTypeEnum key)
    {
        if (key == StatsInfoTypeEnum.damageAmount || key == StatsInfoTypeEnum.reloadTime || key == StatsInfoTypeEnum.maxRangeForShotingSpawning)
        {
            Debug.LogError("use the damager class to set the damage amount, reload time or max shooting distance");
        }
        return statsInfoDic.GetValue<T>(key);
    }

    #region BaseExtrationTypes
    /// <summary>
    /// Gets the string value of a stat.
    /// </summary>
    /// <param name="statsInfoTypeEnum">The key of the stat.</param>
    /// <returns>The string value of the stat.</returns>
    public string GetString(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<string>(statsInfoTypeEnum);
    }

    /// <summary>
    /// Gets the float value of a stat.
    /// </summary>
    /// <param name="statsInfoTypeEnum">The key of the stat.</param>
    /// <returns>The float value of the stat.</returns>
    public float GetFloat(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<float>(statsInfoTypeEnum);
    }

    /// <summary>
    /// Gets the int value of a stat.
    /// </summary>
    /// <param name="statsInfoTypeEnum">The key of the stat.</param>
    /// <returns>The int value of the stat.</returns>
    public int GetInt(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<int>(statsInfoTypeEnum);
    }
    #endregion
    #region BasicSceneobjectProperties

    /// <summary>
    /// Gets the name of the game object.
    /// </summary>
    public string name => statsInfoDic.GetValue<string>(StatsInfoTypeEnum.name);
    /// <summary>
    /// Gets the influence radius of the game object.
    /// </summary>
    public int influenceRadius => statsInfoDic.GetValue<int>(StatsInfoTypeEnum.influenceRadius);

    /// <summary>
    /// Gets the description of the game object.
    /// </summary>
    public string description => statsInfoDic.GetValue<string>(StatsInfoTypeEnum.description);

    /// <summary>
    /// Gets the type of the scene object.
    /// </summary>
    public SceneObjectTypeEnum sceneObjectType => statsInfoDic.GetValue<SceneObjectTypeEnum>(StatsInfoTypeEnum.sceneObjectType);

    public visualEffectsEnum visualEffectWhenDestroyed => statsInfoDic.TryToGetValue<visualEffectsEnum>(StatsInfoTypeEnum.visualEffectWhenDestroyed);
    public int damageWhenDestroyed => statsInfoDic.TryToGetValue<int>(StatsInfoTypeEnum.damageWhenDestroyed);
    public float damageWhenDiedRadius => statsInfoDic.TryToGetValue<float>(StatsInfoTypeEnum.damageWhenDiedRadius);
    /// <summary>
    /// Gets the faction of the game object.
    /// </summary>
    public Faction faction => statsInfoDic.GetValue<Faction>(StatsInfoTypeEnum.Faction);

    /// <summary>
    /// Gets the sprite of the game object.
    /// </summary>
    public Sprite sprite => statsInfoDic.GetValue<Sprite>(StatsInfoTypeEnum.Sprite);

    /// <summary>
    /// Gets the transform of the scene object.
    /// </summary>
    public Transform sceneObjectTransform => statsInfoDic.GetValue<Transform>(StatsInfoTypeEnum.sceneObjectsTransform);


    #endregion
    #region AttackingProperties
    /// <summary>
    /// Gets the list of enemy types the game object can target.
    /// </summary>
    public List<SceneObjectTypeEnum> lookForEnemyType => statsInfoDic.GetValue<List<SceneObjectTypeEnum>>(StatsInfoTypeEnum.canTargetThefollowingSceneObjects);


    /// <summary>
    /// Gets the damager base class of the game object. This is private get since it should use the damage, reload etc
    /// </summary>
    private DamagerBaseClass _damagerBaseClass => statsInfoDic.GetValue<DamagerBaseClass>(StatsInfoTypeEnum.damager);


    public int damageAmount
    {
        get
        {
            float basestat = _damagerBaseClass.CaclulateDamage();
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.damageAmount);
        }
        set {; }
    }
    public float reloadTime
    {
        get
        {
            float basestat = _damagerBaseClass.CalculateReloadTime();
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.reloadTime);
        }
        set {; }
    }
    public float maxRange
    {
        get
        {
            float basestat = _damagerBaseClass.CalculateAttackRange();
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.maxRangeForShotingSpawning);
        }
        set {; }
    }


    /// <summary>
    /// Gets the visual effect of the game object.
    /// </summary>



    #endregion
    #region TakeDamageProperties
    /// <summary>
    /// Gets or sets the health of the game object.
    /// </summary>
    public int health => statsInfoDic.TryToGetValue<int>(StatsInfoTypeEnum.health) is int val ? val : -1;
    public int lifeTime => statsInfoDic.TryToGetValue<int>(StatsInfoTypeEnum.lifeTime) is int val ? val : -1;





    /// <summary>
    /// Gets  the damage multiplier that is read by the idamageable component
    /// </summary>
    public float takesDamageMultiplier
    {
        get
        {
            float basestat = (float)statsInfoDic.GetValue<float>(StatsInfoTypeEnum.takesDamageMultiplier);
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.takesDamageMultiplier);
        }
        set {; }
    }
    public TakesDamageFrom takesDamageFrom => statsInfoDic.GetValue<TakesDamageFrom>(StatsInfoTypeEnum.takesDamageFrom);

    #endregion
    #region SpawnerProrties
    ///<summary>
    /// Gets spawnable object for sceneObjects that spawn other objects
    ///</summary>
    ///
    public SpawningData spawningData => statsInfoDic.GetValue<SpawningData>(StatsInfoTypeEnum.spawningData);

    #endregion
    #region moverProperties
    /// <summary>
    /// Gets or sets the speed of the game object.
    /// </summary>
    public float speed
    {
        get
        {
            float basestat = (float)statsInfoDic.GetValue<float>(StatsInfoTypeEnum.speed);
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.speed);
        }
        set {; }
    }

    #endregion

    #region env object

    /// <summary>
    /// Movefactor is used for env object to detemine the procentage of the speed that they allow
    /// </summary>
    public float moveFactor => statsInfoDic.GetValue<float>(StatsInfoTypeEnum.moveFactor);

    /// <summary>
    /// It says if a object can catch fire and suply the fire with fuel
    /// </summary>
    public bool addFuelToFire
    {
        get => statsInfoDic.GetValue<bool>(StatsInfoTypeEnum.addFuelToFire);
        set => statsInfoDic.Add(StatsInfoTypeEnum.addFuelToFire, value);
    }

    #endregion


    /// <summary>
    /// Applies effects to a base stat.
    /// </summary>
    /// <param name="basestat">The base stat value.</param>
    /// <param name="statsInfoTypeEnum">The type of the stat.</param>
    /// <returns>The modified stat value.</returns>
    private float ApplyEffectToBasestat(float basestat, StatsInfoTypeEnum statsInfoTypeEnum)
    {
        Query query = new Query(statsInfoTypeEnum, basestat);
        statsMediator.PerformQuery(this, query);
        return query.value;
    }
}

public enum StatsInfoTypeEnum
{
    name,
    description,
    Faction,
    Sprite,
    lifeTime,
    canTargetThefollowingSceneObjects,
    health,
    sceneObjectsTransform,
    SoDamageEffect,
    sceneObjectType,
    speed,

    seekSystem,
    attackSystem,
    damager,
    reloadTime,
    maxRangeForShotingSpawning,
    damageAmount,
    takesDamageMultiplier,
    moveFactor, ///for Env objects
    addFuelToFire, ///for Env objects
    spawningData, ///for SpawningBuildings objects
    takesDamageFrom, ///base class for taking damage
    influenceRadius, // How much player influence that is added
    visualEffectWhenDestroyed,
    damageWhenDestroyed,
    damageWhenDiedRadius

}
public class StatsInfoDic : IEnumerable<KeyValuePair<StatsInfoTypeEnum, object>>
{
    private Dictionary<StatsInfoTypeEnum, object> _dict = new Dictionary<StatsInfoTypeEnum, object>();
    public Action OnStatsChanged;
    public void Add<T>(StatsInfoTypeEnum key, T value)
    {
        if (!_dict.ContainsKey(key))
        {
            _dict.Add(key, value);
        }
        else
        {
            _dict[key] = value;
        }
        OnStatsChanged?.Invoke();
    }

    public T GetValue<T>(StatsInfoTypeEnum key)
    {
        if (_dict.TryGetValue(key, out var value) && value is T)
        {
            return (T)value;
        }
        Debug.LogError("key value does not exist of " + key.ToString() + " for scene object" +  GetValue<string>(StatsInfoTypeEnum.name));
        return default;
    }
    public T TryToGetValue<T>(StatsInfoTypeEnum key)
    {
        if (_dict.TryGetValue(key, out var value) && value is T)
        {
            return (T)value;
        }

        return default;
    }

    public IEnumerator<KeyValuePair<StatsInfoTypeEnum, object>> GetEnumerator()
    {
        return _dict.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dict.GetEnumerator();
    }
}

