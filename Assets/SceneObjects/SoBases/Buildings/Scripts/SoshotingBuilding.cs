using System.Collections.Generic;
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
    public TargetPriorityEnum lookForEnemyType;
    public List<iDamageableTypeEnum> possibleTargetTypes;


    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        _statsInforDic.Add(StatsInfoTypeEnum.reloadTime, reloadTimerMax);
        _statsInforDic.Add(StatsInfoTypeEnum.damageAmount, damageAmount);
        _statsInforDic.Add(StatsInfoTypeEnum.fireEffect, fireEffect);
        _statsInforDic.Add(StatsInfoTypeEnum.maxShotingDistance, maxShotingDistance);
        _statsInforDic.Add(StatsInfoTypeEnum.targetableType, lookForEnemyType);
        _statsInforDic.Add(StatsInfoTypeEnum.lifeTime, lifeTime);
        _statsInforDic.Add(StatsInfoTypeEnum.sprite, sprite);
        _statsInforDic.Add(StatsInfoTypeEnum.IdamageableTypeTarget, possibleTargetTypes);
        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}