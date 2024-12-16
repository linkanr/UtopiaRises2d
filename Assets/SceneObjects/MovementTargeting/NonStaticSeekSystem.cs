using Sirenix.OdinInspector.Editor.Drawers;
using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class NonStaticSeekSystem:ScriptableObject, Iseeker
{
    public float speed;
    
    public float lookForNewTargetTime;
    public Action<IDamageable> OnNewTarget;
    public Action OnReachedTarget;

 


    public void  ReachedTarget()
    {
        OnReachedTarget();
}
    public virtual void Seek(Vector2 position, List<iDamageableTypeEnum> IdamageableEnum)// This is a overload to be able to pass 2 args

    {
        Seek(position, IdamageableEnum,TargetPriorityEnum.notRelevant);
    }

    public abstract void Seek(Vector2 position, List<iDamageableTypeEnum> IdamageableEnum, TargetPriorityEnum targetPriorityEnum);
 
}