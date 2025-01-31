using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Movement/Buildings/BasicSeekSystem")]
public class SoSeekSystemForBuildings : SoSeekSystemBase
{


    public override SceneObject Seek(Vector3 position, List<SceneObjectTypeEnum> sceneObjectTypeEnums, TargeterBaseClass attackerComponent, IMoverComponent moverComponent = null)
    {
        SceneObject newTarget = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(position, sceneObjectTypeEnumsList: sceneObjectTypeEnums, maxDistance: attackerComponent.attacker.GetStats().maxShootingDistance);
        if (newTarget != null)
        {
            attackerComponent.SetNewTarget(newTarget);
            return newTarget;
        }
        return null;

    }
}