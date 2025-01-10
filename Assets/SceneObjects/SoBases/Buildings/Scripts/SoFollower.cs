using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/SoFollowers/Follower")]
public class SoFollower : SoSceneObjectBase
{

    public int health;
    
    protected override Stats GetStatsInernal(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, health);

        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}