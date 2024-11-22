using Sirenix.OdinInspector.Editor.Drawers;
using System;
using UnityEngine;


public class SoEnemySeekSystem:ScriptableObject, Iseeker
{
    public float speed;
    
    public float lookForNewTargetTime;
    public Action<Transform> OnNewTarget;
    public Action OnReachedTarget;

 
    public virtual void Seek(Vector2 position, TargetableAttacksEnums enemyAttackTypeEnum)
    {
        
        
    }

    public void  ReachedTarget()
    {
        OnReachedTarget();
}
}