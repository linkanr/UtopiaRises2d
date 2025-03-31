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
        bounds = c2D.bounds;
        bounds.Expand(.1f);
        BattleSceneActions.OnUpdateBounds?.Invoke(bounds);
        Destroy(gameObject);
    }
    public override void OnCreated()
    {
  
        c2D = GetComponent<Collider2D>();
        bounds = c2D.bounds;
        bounds.Expand(.1f);
        BattleSceneActions.OnUpdateBounds?.Invoke(bounds);
    }
}

public enum ObjectTypeEnums
{
    stone,
    forest,
    none
}