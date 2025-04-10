using UnityEngine;

public abstract class EnviromentObject : StaticSceneObject
{

    


    public static EnviromentObject Create(ObjectTypeEnums objectTypeEnums, Vector3 position)
    {

        

        position.z = 0;
        string name = objectTypeEnums.ToString();


        string so = "so" + objectTypeEnums.ToString();
        SoEnviromentObject soEnviromentObject = Resources.Load(so) as SoEnviromentObject;
        EnviromentObject sceneObject =  soEnviromentObject.Init(position) as EnviromentObject;




        sceneObject.transform.SetParent(GameObject.Find("PersistantParent").transform);

        return sceneObject;
    }
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

public enum ObjectTypeEnums
{
    stone,
    forest,
    bridge,
    none
}