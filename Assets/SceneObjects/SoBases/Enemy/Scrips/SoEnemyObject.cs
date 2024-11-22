using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyType")]
[System.Serializable]
public class SoEnemyObject:SoSceneObjectBase

{
    public int health;
    public SoEnemyAttackSystem attackSystem;
    public SoEnemySeekSystem seekSystem;
    public Sprite sprite;   
    public SoDamageEffect damageEffect;

    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        _statsInforDic.Add(StatsInfoTypeEnum.health, health);
        _statsInforDic.Add(StatsInfoTypeEnum.SoEnemyAttackSystem, attackSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.SoEnemySeekSystem, seekSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.sprite, sprite);
        _statsInforDic.Add(StatsInfoTypeEnum.SoDamageEffect, damageEffect);
        _statsInforDic.Add(StatsInfoTypeEnum.damageAmount, attackSystem.damage);
        _statsInforDic.Add(StatsInfoTypeEnum.maxShotingDistance,attackSystem.maxRange);




        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
       
    }
}