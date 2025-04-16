using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cards/Heal")]

public class CardHeal : SoCardBase
{
    public float radius;
    public bool infintite;
    public int healthToAdd;
    public List<SceneObjectTypeEnum> objectTypes;
    public override bool ActualEffect(Vector3 position, out string failuerReason)
    {
        float reach = radius;
        if (infintite)
        {
            reach = Mathf.Infinity;
        }
        failuerReason = "";
        List<SceneObject> sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(position,sceneObjectTypeEnumsList:  objectTypes, maxDistance:reach);
        if (sceneObjects.Count < 1)
        {
            failuerReason = "No player buildings in range";
            return false;
        }
        foreach (SceneObject sceneObject in sceneObjects)
        {
            HealthSystem timeHealthSystem = sceneObject.healthSystem as TimeHealthSystem;
            timeHealthSystem.Heal(healthToAdd);
        }
        return true;
    }


}
