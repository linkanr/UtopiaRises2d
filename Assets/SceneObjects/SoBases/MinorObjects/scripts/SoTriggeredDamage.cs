using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "ScriptableObjects/MinorObject/DamageTriggered")]
public class SoTriggeredDamage : SoMinorObject
{
    public VisualEffect visualEffect;
    public DamagerBaseClass damage;
    

    protected override Stats GetStatsInernal(Stats stats)
    {
        base.GetStatsInernal(stats);
        stats.Add(StatsInfoTypeEnum.FireEffect, visualEffect);
        DamagerBaseClass damagerInstance = damage.Clone();
        damage = damagerInstance;
        stats.Add(StatsInfoTypeEnum.damager, damage);
        
        return stats;
    }


}
