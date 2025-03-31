using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/SoBuildnings/DefensiveBuidling")]
public class SoDefensiveStructure : SoBuilding
{
    public DamagerBaseClass damagerBaseClass;
    /// <summary>
    /// Override stats in order to set it as a defensive structure
    /// </summary>
    /// <param name="stats"></param>
    /// <returns></returns>
    protected override Stats GetStatsInernal(Stats stats)
    {

        base.GetStatsInernal(stats);
        stats.Add(StatsInfoTypeEnum.lifeTime, lifeTime);
        stats.Add(StatsInfoTypeEnum.damager, damagerBaseClass);
        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.defensiveStructure);
 
        return stats;
    }

    
}


