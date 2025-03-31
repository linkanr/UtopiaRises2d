
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Movement/Enemy/SearchAndMove")]
public class SoSeekSytemForEnemies : SoSeekSystemBase//Goes straigt to damagable building and attacks
{


    public override SceneObject Seek(Vector3 position, List<SceneObjectTypeEnum> sceneObjectTypeEnums, TargeterBaseClass attackerComponent, IMoverComponent moverComponent = null)
    {
        Debug.Log("Seek method called with position: " + position);
        position.z = 0;
        TargeterForEnemies movingTargeter = attackerComponent as TargeterForEnemies;

        if (moverComponent == null)
        {
            Debug.LogError("MoverComponent is null.");
            return null;
        }

        if (movingTargeter == null)
        {
            Debug.LogError("AttackerComponent is not a TargeterForEnemies.");
            return null;
        }

        List<SceneObject> potentialDamagebles = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(
            position, sceneObjectTypeEnumsList: sceneObjectTypeEnums, onlyDamageables: true);

        Debug.Log("Found " + potentialDamagebles.Count + " potential damageables.");

        if (potentialDamagebles.Count == 0)
        {
            Debug.LogWarning("No damageable objects found.");
            return null;
        }

        Vector3[] positions = new Vector3[potentialDamagebles.Count];

        for (int i = 0; i < potentialDamagebles.Count; i++)
        {
            SceneObject damageable = potentialDamagebles[i];

            if (damageable == null)
            {
                Debug.LogWarning("Damageable object at index " + i + " is null.");
                continue;
            }

            if (damageable.transform != null)
            {
                positions[i] = damageable.transform.position;
                Debug.Log("Position [" + i + "]: " + positions[i]);
            }
            else
            {
                Debug.LogWarning("Damageable object at index " + i + " has a null transform.");
            }
        }

        if (positions.Length == 0)
        {
            Debug.LogWarning("No valid positions found for potential damageables.");
            return null;
        }

        Debug.Log("Calling StartMultiTargetPath with " + positions.Length + " positions.");

        moverComponent.seeker.StartMultiTargetPath(position, positions, false, movingTargeter.SetNewTarget);

        return null;
    }



}
