
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
       
    }
}