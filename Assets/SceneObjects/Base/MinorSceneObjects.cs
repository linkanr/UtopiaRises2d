
using UnityEngine;

public class MinorSceneObjects : SceneObject
{

    protected override void AddStatsForClick(Stats _stats)
    {
        
    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
    
    }
    protected override visualEffectsEnum GetDeathEffect()
    {
        return visualEffectsEnum.minor;
    }
    public override void OnCreated()
    {
        Debug.Log("MinorSceneObjects.OnCreated");
        base.OnCreated();
       
        if (SceneObjectManager.Instance.RetriveSceneObjects(SceneObjectTypeEnum.all).Contains(this))
        {
            //Debug.Log("MinorSceneObjects.OnCreated: SceneObjectManager.Instance.RetriveSceneObjects(SceneObjectTypeEnum.all).Contains(this)");
        }
        else
        {
            //Debug.Log("MinorSceneObjects.OnCreated: Not in scene manager");
        }
    }
}