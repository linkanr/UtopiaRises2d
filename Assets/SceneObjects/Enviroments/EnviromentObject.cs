using Mono.Cecil.Cil;
using UnityEngine;

public abstract class EnviromentObject : StaticSceneObject
{
    public float moveFactor;

    public static EnviromentObject Create(ObjectTypeEnums objectTypeEnums, Vector3 position)
    {
        position.z = 0;
        string name = objectTypeEnums.ToString();

        GameObject enviromentObject = Resources.Load(name) as GameObject;

        EnviromentObject envO = enviromentObject.GetComponent<EnviromentObject>().DoCreate(position);

        envO.transform.SetParent(GameObject.Find("PersistantParent").transform);
        return envO;
    }
    protected override void AddStatsForClick(Stats _stats)
    {

        _stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.enviromentObject);
    }


    public abstract EnviromentObject DoCreate(Vector3 position);
    protected override void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }
}

public enum ObjectTypeEnums
{
    stone,
    forest
}