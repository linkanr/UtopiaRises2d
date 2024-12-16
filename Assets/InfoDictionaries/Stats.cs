using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Stats
{
    public Stats()
    {
        statsInfoDic = new StatsInfoDic();
    }
    public StatsInfoDic statsInfoDic;
    
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
    public Faction GetFaction()
    {
        return statsInfoDic.GetValue<Faction>(StatsInfoTypeEnum.faction);
    }
    public Sprite GetSprite()
    {
        return statsInfoDic.GetValue<Sprite>(StatsInfoTypeEnum.sprite);
    }
    public VisualEffect GetVisualEffect(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<VisualEffect>(statsInfoTypeEnum);
    }
    public Transform GetTransform(StatsInfoTypeEnum statsInfoTypeEnum)
    {
        return statsInfoDic.GetValue<Transform>(statsInfoTypeEnum);
    }
    public TargetPriorityEnum GetLookForEnemyType()
    {
        return statsInfoDic.GetValue<TargetPriorityEnum>(StatsInfoTypeEnum.targetableType);
    }
    public float GetReloadTime()
    {
        return statsInfoDic.GetValue<SoAttackSystem>(StatsInfoTypeEnum.SoEnemyAttackSystem).attackTimerMax;
    }
    public float GetMaxShotingDistance()
    {
        return statsInfoDic.GetValue<SoAttackSystem>(StatsInfoTypeEnum.SoEnemyAttackSystem).maxRange;
    }
    public float GetDamage()
    {
        return statsInfoDic.GetValue<SoAttackSystem>(StatsInfoTypeEnum.SoEnemyAttackSystem).damage;
    }
    public SoAttackSystem GetSoAttackSystem()
    {
        return statsInfoDic.GetValue<SoAttackSystem>(StatsInfoTypeEnum.SoEnemyAttackSystem);
    }

}

public enum StatsInfoTypeEnum
{
    name,
    description,
    reloadTime,
    faction,
    sprite,
    lifeTime,
    damageAmount,
    fireEffect,
    maxShotingDistance,
    targetableType,
    health,
    objectToFollow,
    SoEnemyAttackSystem,
    SoEnemySeekSystem,
    SoDamageEffect,
    IdamageableTypeTarget


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

