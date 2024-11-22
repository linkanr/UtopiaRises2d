using Mono.Cecil.Cil;
using UnityEngine;

public abstract class EnviromentObject : StaticSceneObject
{
    public Sprite sprite;

    public static EnviromentObject Create(ObjectTypeEnums objectTypeEnums, Vector3 position)
    {
        position.z = 0;
        string name = objectTypeEnums.ToString();
        Debug.Log(name);
        GameObject enviromentObject = Resources.Load(name) as GameObject;
        Debug.Log(enviromentObject);
        enviromentObject.GetComponent<EnviromentObject>().DoCreate(position);
        return enviromentObject.GetComponent<EnviromentObject>();
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
    tree
}