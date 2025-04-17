using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Movement/Buildings/BasicSeekSystem")]
public class SoSeekSystemForBuildings : SoSeekSystemBase
{


    public override SceneObject Seek(Vector3 position, List<SceneObjectTypeEnum> sceneObjectTypeEnums, TargeterBaseClass attackerComponent, IMoverComponent moverComponent = null)
    {
        List<SceneObject> targets = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(position, sceneObjectTypeEnumsList: sceneObjectTypeEnums, amount: 1, maxDistance: attackerComponent.attacker.GetStats().maxRange());
        if (targets.Count == 0)
        {
            return null;
        }
        SceneObject newTarget = targets[0];
        if (newTarget != null)
        {
            attackerComponent.SetNewTarget(newTarget);
            return newTarget;
        }
        return null;

    }
}