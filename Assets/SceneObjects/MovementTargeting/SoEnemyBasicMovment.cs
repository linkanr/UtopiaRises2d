using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Movement/Enemy/BasicMovment")]
public class SoEnemyBasicMovment : NonStaticSeekSystem//Goes straigt to damagable building and attacks
{
    private void Awake()
    {
        
    }
    public override void Seek(Vector2 pos, List<iDamageableTypeEnum> iDamageableTypeEnum, TargetPriorityEnum targetPriorityEnum  )
    {
        Transform newTarget = null;
        Transform secondaryTarget = null;

        float distance = Mathf.Infinity;
;
        foreach (IDamageable idamagable in SceneObjectManager.Instance.iDamagablesInScene)
        {
            if (iDamageableTypeEnum.Contains(idamagable.damageableType) )// DOES IT MATCH THE SPECIFIC OF 
                //THIS ATTACK TYPE
            {
                
                float newDistance = Vector3.Distance(pos, idamagable.GetTransform().position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    newTarget = idamagable.GetTransform();
                }

            }


        }
        if (newTarget != null)//PRIMARY TARGET FOUND
        {
            OnNewTarget(newTarget);
        }
        else//NO PRIMARY GO FOR BASE(OR OTHER SECONDERY)
        {
            OnNewTarget(secondaryTarget);
        }
        
        
    }


}
