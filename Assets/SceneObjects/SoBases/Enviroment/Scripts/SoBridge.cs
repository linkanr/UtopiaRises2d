using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "ScriptableObjects/EnviromentTypes/Bridge")]
public class SoBridge : SoEnviromentObject
{

    public DamagerBaseClass damage;
 


    protected override Stats GetStatsInernal(Stats stats)
    {
        base.GetStatsInernal(stats);
        DamagerBaseClass damagerInstance = damage.Clone();
        damage = damagerInstance;
        stats.Add(StatsInfoTypeEnum.damager, damage);

        return stats;
    }


}
