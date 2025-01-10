using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Stats
{
    public Stats()
    {
        statsInfoDic = new StatsInfoDic();
        statsMediator = new StatsMediator();

    }

    public StatsInfoDic statsInfoDic;
    public StatsMediator statsMediator;
    public void AddStats(Stats _stats)
    {
        foreach(KeyValuePair<StatsInfoTypeEnum, object> statsObject in _stats.statsInfoDic)
        {
            statsInfoDic.Add(statsObject.Key, statsObject.Value);
        }
    }
    public void Add<T>(StatsInfoTypeEnum key, T value)
    {
        statsInfoDic.Add(key, value);
    }
    public T GetValue<T>(StatsInfoTypeEnum key)
    {
        return statsInfoDic.GetValue<T>(key);
    }
    
    public string GetString(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<string>(statsInfoTypeEnum);
    }
    public float GetFloat(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<float>(statsInfoTypeEnum);
    }
    public int GetInt(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<int>(statsInfoTypeEnum);
    }
    public Faction faction => statsInfoDic.GetValue<Faction>(StatsInfoTypeEnum.Faction);

    public Sprite sprite => statsInfoDic.GetValue<Sprite>(StatsInfoTypeEnum.Sprite);

    public VisualEffect visualEffect
        => statsInfoDic.GetValue<VisualEffect>(StatsInfoTypeEnum.FireEffect);

    public Transform sceneObjectTransform
        => statsInfoDic.GetValue<Transform>(StatsInfoTypeEnum.sceneObjectsTransform);

    public List<SceneObjectTypeEnum> lookForEnemyType
        => statsInfoDic.GetValue<List<SceneObjectTypeEnum>>(StatsInfoTypeEnum.canTargetThefollowingSceneObjects);

    public SceneObjectTypeEnum sceneObjectType
        => statsInfoDic.GetValue<SceneObjectTypeEnum>(StatsInfoTypeEnum.sceneObjectType);

    public float reloadTime
        => statsInfoDic.GetValue<float>(StatsInfoTypeEnum.reloadTime);

    public float maxShootingDistance
        => statsInfoDic.GetValue<float>(StatsInfoTypeEnum.maxShotingDistance);

    public int damage
    {
        get
        {
            float basestat =(float) statsInfoDic.GetValue<int>(StatsInfoTypeEnum.damageAmount);
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.damageAmount);
        }
        
        set {; }
    }

       


    public int health
        => statsInfoDic.GetValue<int>(StatsInfoTypeEnum.health);
    public float speed
    {
        get
        {
            float basestat = (float)statsInfoDic.GetValue<float>(StatsInfoTypeEnum.speed);
            return (int)ApplyEffectToBasestat(basestat, StatsInfoTypeEnum.speed);
        }

        set {; }
    }



    public string name
=> statsInfoDic.GetValue<string>(StatsInfoTypeEnum.name);
    public string description
=> statsInfoDic.GetValue<string>(StatsInfoTypeEnum.description);

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
    reloadTime,
    Faction,
    Sprite,
    lifeTime,
    damageAmount,
    FireEffect,
    maxShotingDistance,
    canTargetThefollowingSceneObjects,
    health,
    sceneObjectsTransform,
    SoDamageEffect,
    sceneObjectType,
    speed,
    onClickDisplaySprite,
    seekSystem,
    attackSystem


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
        Debug.LogWarning("key value does not exist of " + key.ToString() );
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

