
using UnityEngine;

public class EnviromentStone : EnviromentObject
{
    

    public override EnviromentObject DoCreate(Vector3 position)
    {
        Transform transform = Instantiate(this.transform, position,Quaternion.identity);
        EnviromentObject enviromentObject = transform.GetComponent<EnviromentObject>();
        return enviromentObject;
    }


}