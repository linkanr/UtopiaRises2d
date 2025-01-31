using UnityEngine;

public class ConstructionBaseSceneObject : StaticSceneObject
{
    public static ConstructionBaseSceneObject Create(Vector3 pos)
    {
        SoBuildingConstructionBase soBuildingConstructionBase = Resources.Load<SoBuildingConstructionBase>("ConstructionBase");
        ConstructionBaseSceneObject constructionBaseSceneObject = soBuildingConstructionBase.Init(pos) as ConstructionBaseSceneObject;
       return constructionBaseSceneObject;

    }
    protected override void Start()
    {
        Debug.Log("ConstructionBaseSceneObject Start");
        base.Start();
    }
    protected override void AddStatsForClick(Stats _stats)
    {
      
    }

    protected override void OnObjectDestroyed()
    {
        
    }
}   