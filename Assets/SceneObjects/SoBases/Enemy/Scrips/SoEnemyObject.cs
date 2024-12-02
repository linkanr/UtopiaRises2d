using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyType")]
[System.Serializable]
public class SoEnemyObject:SoSceneObjectBase

{
    public int health;
    public SoAttackSystem attackSystem;
    public NonStaticSeekSystem seekSystem;
    public Sprite sprite;   
    public SoDamageEffect damageEffect;
    public List<iDamageableTypeEnum> possibleTargetTypes;
    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        _statsInforDic.Add(StatsInfoTypeEnum.health, health);
        _statsInforDic.Add(StatsInfoTypeEnum.SoEnemyAttackSystem, attackSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.SoEnemySeekSystem, seekSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.sprite, sprite);
        _statsInforDic.Add(StatsInfoTypeEnum.SoDamageEffect, damageEffect);
        _statsInforDic.Add(StatsInfoTypeEnum.damageAmount, attackSystem.damage);
        _statsInforDic.Add(StatsInfoTypeEnum.maxShotingDistance,attackSystem.maxRange);
        _statsInforDic.Add(StatsInfoTypeEnum.targetableType, possibleTargetTypes);



        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
       
    }
}