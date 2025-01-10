using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyType")]
[System.Serializable]
public class SoEnemyObject:SoSceneObjectBase

{
    public int health;
    public SoAttackSystem attackSystem;
    public SoSeekSytemForEnemies seekSystem;
   
    public SoDamageEffect damageEffect;
    public List<SceneObjectTypeEnum> possibleTargetTypes;
    public float speed;
    public int damage;
    public float maxShootingDistance;
    public float reloadTime;
    

    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        _statsInforDic.Add(StatsInfoTypeEnum.health, health);
        _statsInforDic.Add(StatsInfoTypeEnum.damageAmount, damage);
        _statsInforDic.Add(StatsInfoTypeEnum.speed, speed);
        _statsInforDic.Add(StatsInfoTypeEnum.maxShotingDistance, maxShootingDistance);
        _statsInforDic.Add(StatsInfoTypeEnum.reloadTime, reloadTime);
        _statsInforDic.Add(StatsInfoTypeEnum.Sprite, sprite);
        _statsInforDic.Add(StatsInfoTypeEnum.SoDamageEffect, damageEffect);
        _statsInforDic.Add(StatsInfoTypeEnum.seekSystem, seekSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.attackSystem, attackSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.onClickDisplaySprite, attackSystem.displayRangeSprite);
        _statsInforDic.Add(StatsInfoTypeEnum.canTargetThefollowingSceneObjects, possibleTargetTypes);
        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
       
    }
}