using UnityEngine;

public class SceneObjectConstructionBase : StaticSceneObject
{
    public static SceneObjectConstructionBase Create(Vector3 pos)
    {
        SoBuildingConstructionBase soBuildingConstructionBase = Resources.Load<SoBuildingConstructionBase>("ConstructionBase");
        SceneObjectConstructionBase constructionBaseSceneObject = soBuildingConstructionBase.Init(pos) as SceneObjectConstructionBase;
       
        return constructionBaseSceneObject;

    }
    protected override void Start()
    {
       // Debug.Log("ConstructionBaseSceneObject Start");
        base.Start();

        transform.SetParent(GameSceneRef.instance.constructionBaseParent);
    }
    protected override void AddStatsForClick(Stats _stats)
    {
      
    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        
    }
}   