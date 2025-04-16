using Sirenix.OdinInspector;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;
using static SceneObjectShootingBuilding;
/// <summary>
/// Building is the base class for all man made object that has a limited life span
/// </summary>

public abstract class SoBuilding : SoSceneObjectBase
{
    public int influenceRadius = 3;
    public int lifeTime;
    public SoDamageEffect soDamageEffect;

    protected override Stats GetStatsInernal(Stats stats)
    {

        stats.Add(StatsInfoTypeEnum.lifeTime, lifeTime);

        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.playerbuilding);
        stats.Add(StatsInfoTypeEnum.influenceRadius, influenceRadius);
        return stats;
    }


}