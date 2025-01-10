using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
[CreateAssetMenu(menuName = "ScriptableObjects/Movement/Enemy/SearchAndMove")]
public class SoSeekSytemForEnemies : SoSeekSystemBase//Goes straigt to damagable building and attacks
{


    public override SceneObject Seek(Vector3 position, List<SceneObjectTypeEnum> sceneObjectTypeEnums, TargeterBaseClass attackerComponent, SeekStyle seekStyle = SeekStyle.findclosest, IMoverComponent moverComponent = null)
    {

        TargeterForEnemies movingTargeter = attackerComponent as TargeterForEnemies;

        if (moverComponent == null)
        {
            Debug.LogError("MoverComponent is null or not a MovingTargeter.");
            return null;
        }

        List<SceneObject> potentialDamagebles = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(position, sceneObjectTypeEnumsList: sceneObjectTypeEnums, onlyDamageables:true);
        Vector3[] positions = new Vector3[potentialDamagebles.Count];
        int i = 0;
        foreach (SceneObject damageable in potentialDamagebles)
        {
            if (damageable.transform != null)
            {
                positions[i] = damageable.transform.position;
            }
            else
            {
                Debug.LogWarning("transform is null"); 
            }
            Debug.Log("i is" + i + " pos is " + positions[i].ToString());
            i++;
            
        }

        if (positions.Length == 0)
        {
            Debug.LogWarning("No valid positions found for potential damageables.");
            return null;
        }
        if (positions.Length > 0)
        {
            moverComponent.seeker.StartMultiTargetPath(position, positions, false, movingTargeter.SetNewTarget);
        }
        return null;
        










    }


}
