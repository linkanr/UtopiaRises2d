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
        statsMediator = new StatsMediator();
    }

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
        if (key == StatsInfoTypeEnum.damageAmount || key == StatsInfoTypeEnum.reloadTime || key == StatsInfoTypeEnum.maxShotingDistance)
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
        if (key == StatsInfoTypeEnum.damageAmount || key == StatsInfoTypeEnum.reloadTime || key == StatsInfoTypeEnum.maxShotingDistance)
        {
            Debug.LogError("use the damager class to set the damage amount, reload time or max shooting distance");
        }
        return statsInfoDic.GetValue<T>(key);
    }

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

    /// <summary>
    /// Gets the faction of the game object.
    /// </summary>
    public Faction faction => statsInfoDic.GetValue<Faction>(StatsInfoTypeEnum.Faction);

    /// <summary>
    /// Gets the sprite of the game object.
    /// </summary>
    public Sprite sprite => statsInfoDic.GetValue<Sprite>(StatsInfoTypeEnum.Sprite);

    /// <summary>
    /// Gets the visual effect of the game object.
    /// </summary>
    public VisualEffect visualEffect => statsInfoDic.GetValue<VisualEffect>(StatsInfoTypeEnum.FireEffect);

    /// <summary>
    /// Gets the transform of the scene object.
    /// </summary>
    public Transform sceneObjectTransform => statsInfoDic.GetValue<Transform>(StatsInfoTypeEnum.sceneObjectsTransform);

    /// <summary>
    /// Gets the list of enemy types the game object can target.
    /// </summary>
    public List<SceneObjectTypeEnum> lookForEnemyType => statsInfoDic.GetValue<List<SceneObjectTypeEnum>>(StatsInfoTypeEnum.canTargetThefollowingSceneObjects);

    /// <summary>
    /// Gets the type of the scene object.
    /// </summary>
    public SceneObjectTypeEnum sceneObjectType => statsInfoDic.GetValue<SceneObjectTypeEnum>(StatsInfoTypeEnum.sceneObjectType);

    /// <summary>
    /// Gets the damager base class of the game object.
    /// </summary>
    public DamagerBaseClass damagerBaseClass => statsInfoDic.GetValue<DamagerBaseClass>(StatsInfoTypeEnum.damager);

    /// <summary>
    /// Gets or sets the reload time of the game object.
    /// </summary>
    public float reloadTime
    {
        get
        {
            float basestat = damagerBaseClass.CalculateReloadTime();
            return ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.reloadTime);
        }
        set {; }
    }

    /// <summary>
    /// Gets the maximum shooting distance of the game object.
    /// </summary>
    public float maxShootingDistance 
    {
        get
        {
            float basestat = damagerBaseClass.CalculateAttackRange();
            return ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.maxShotingDistance);
        }
                set { Debug.LogError("trying to set damage directly"); }
    }


    /// <summary>
    /// Gets or sets the damage amount of the game object.
    /// </summary>
    public int damageAmount
    {
        get
        {
            Debug.Log("Getting damage");
            float basestat = damagerBaseClass.CaclulateDamage();
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.damageAmount);
        }
        set { Debug.LogError("trying to set damage directly"); }
    }

    /// <summary>
    /// Gets or sets the health of the game object.
    /// </summary>
    public int health { get; set; }

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

    /// <summary>
    /// Gets the name of the game object.
    /// </summary>
    public string name => statsInfoDic.GetValue<string>(StatsInfoTypeEnum.name);

    /// <summary>
    /// Gets the description of the game object.
    /// </summary>
    public string description => statsInfoDic.GetValue<string>(StatsInfoTypeEnum.description);

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
    FireEffect,
    canTargetThefollowingSceneObjects,
    health,
    sceneObjectsTransform,
    SoDamageEffect,
    sceneObjectType,
    speed,
    onClickDisplaySprite,
    seekSystem,
    attackSystem,
    damager,
    reloadTime,
    maxShotingDistance,
    damageAmount,
    takesDamageMultiplier


}
public class StatsInfoDic : IEnumerable<KeyValuePair<StatsInfoTypeEnum, object>>
{
    private Dictionary<StatsInfoTypeEnum, object> _dict = new Dictionary<StatsInfoTypeEnum, object>();

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
    }

    public T GetValue<T>(StatsInfoTypeEnum key)
    {
        if (_dict.TryGetValue(key, out var value) && value is T)
        {
            return (T)value;
        }
        Debug.LogError("key value does not exist of " + key.ToString() );
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

