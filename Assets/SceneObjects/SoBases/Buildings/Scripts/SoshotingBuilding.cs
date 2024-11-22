using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "ScriptableObjects/SoBuildnings/shotingBuildings")]
public class SoshotingBuilding : SoBuilding
{
    public float reloadTimerMax;
    public int damageAmount;
    public VisualEffect fireEffect;
    public float maxShotingDistance;
    public LookForEnemyType lookForEnemyType;



    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        _statsInforDic.Add(StatsInfoTypeEnum.reloadTime, reloadTimerMax);
        _statsInforDic.Add(StatsInfoTypeEnum.damageAmount, damageAmount);
        _statsInforDic.Add(StatsInfoTypeEnum.fireEffect, fireEffect);
        _statsInforDic.Add(StatsInfoTypeEnum.maxShotingDistance, maxShotingDistance);
        _statsInforDic.Add(StatsInfoTypeEnum.lookForEnemyType, lookForEnemyType);
        _statsInforDic.Add(StatsInfoTypeEnum.lifeTime, lifeTime);
        _statsInforDic.Add(StatsInfoTypeEnum.sprite, sprite);
        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}