using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/SoFollowers/Follower")]
public class SoFollower : SoSceneObjectBase
{


    public Sprite sprite;

    protected override Stats GetStatsInernal(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.sprite, sprite);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}