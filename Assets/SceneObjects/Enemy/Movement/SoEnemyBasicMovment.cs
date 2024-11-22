using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Movement/Enemy/BasicMovment")]
public class SoEnemyBasicMovment : SoEnemySeekSystem//Goes straigt to damagable building and attacks
{
    private void Awake()
    {
        
    }
    public override void Seek(Vector2 pos, TargetableAttacksEnums enemyAttackTypeEnum)
    {
        Transform newTarget = null;
        Transform secondaryTarget = null;

        float distance = Mathf.Infinity;
        float secondDistance = Mathf.Infinity;
        foreach (ITargetableByEnemy itargetableByEnememy in PlayerAssetsManager.Instance.iTargetableByEnememiesList)
        {
            if (itargetableByEnememy.TargatebleEnum()== enemyAttackTypeEnum)// DOES IT MATCH THE SPECIFIC OF 
                //THIS ATTACK TYPE
            {
                
                float newDistance = Vector3.Distance(pos, itargetableByEnememy.GetTransform().position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    newTarget = itargetableByEnememy.GetTransform();
                }

            }
            if (itargetableByEnememy.TargatebleEnum() == TargetableAttacksEnums.Base)//THIS
                //LIST ALWAYS INCLUDES THE BASE
            {
                float newDistance = Vector3.Distance(pos, itargetableByEnememy.GetTransform().position);
                if (newDistance < distance)
                {
                    secondDistance = newDistance;
                    secondaryTarget = itargetableByEnememy.GetTransform();
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
