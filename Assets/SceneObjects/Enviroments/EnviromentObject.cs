using UnityEngine;

public abstract class EnviromentObject : StaticSceneObject
{

    



    protected override void AddStatsForClick(Stats _stats)
    {

        _stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.enviromentObject);
       
    }



    protected override void OnObjectDestroyedObjectImplementation()
    {

        
    }
    public override void OnCreated()
    {
  

    }
}

