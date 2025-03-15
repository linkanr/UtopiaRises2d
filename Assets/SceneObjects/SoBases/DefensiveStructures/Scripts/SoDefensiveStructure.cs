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


        stats.Add(StatsInfoTypeEnum.lifeTime, lifeTime);

        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.defensiveStructure);
        stats.Add(StatsInfoTypeEnum.damager, damagerBaseClass);
        return stats;
    }

    
}


