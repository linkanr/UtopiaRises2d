using Sirenix.OdinInspector;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;
using static ShootingBuilding;
/// <summary>
/// Building is the base class for all man made object that has a limited life span
/// </summary>

public abstract class SoBuilding : SoSceneObjectBase
{

    public float lifeTime;
    public int health;
   

    protected override Stats GetStatsInernal(Stats stats)
    {
        
        stats.Add(StatsInfoTypeEnum.lifeTime, lifeTime);
        stats.Add(StatsInfoTypeEnum.health, health);
        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.playerbuilding);
        return stats;
    }
    protected override void ObjectInitialization(SceneObject sceneObject)
    {

            IHasLifeSpan hasLifeSpan = sceneObject as IHasLifeSpan;
            hasLifeSpan.SetTimeLimiter();
       
    }

}