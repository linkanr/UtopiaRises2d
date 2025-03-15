using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Seeks and attacks 
/// </summary>
public abstract class TargeterBaseClass : MonoBehaviour
{
    public abstract SoSeekSystemBase GetSeeker();
    public SoAttackSystem soAttackSystem { get; set; }
    public SceneObject attacker { get; set; }
    public Target target { get; set; }

    public float lookForNewTargetTime { get; set; }
    public float attackTimer { get; set; }

    public List<SceneObjectTypeEnum> possibleTargetTypes { get; set; }
  

    public  Action <Target> OnTargetChanged;

    public void RemoveTarget()
    {
        if (target == null)
        {
            Debug.Log("RemoveTarget() called, but target is already null. Skipping.");
            return;
        }

        if (target.damagable?.iDamageableComponent != null)
        {
            Debug.Log($"Removing target: {target.damagable.iDamageableComponent.sceneObject.GetStats().name}");
            target.damagable.iDamageableComponent.OnDeath -= (sender, e) => RemoveTarget();
        }

        target.RemoveTarget();
        target = null;
    }




    public virtual void SetNewTarget(SceneObject _iDamageable)
    {
        if (_iDamageable == null)
        {
            Debug.LogError("SetNewTarget was called with a null SceneObject!");
            return;
        }

        IDamageAble damageableTarget = _iDamageable as IDamageAble;
        if (damageableTarget == null)
        {
            Debug.LogError($"SetNewTarget failed: {_iDamageable.name} is not an IDamageAble!");
            return;
        }

        target = new Target(damageableTarget, this);
        OnTargetChanged?.Invoke(target);
    }




    public virtual void Seek()
    {
        GetSeeker().Seek(attacker.transform.position, possibleTargetTypes, this);    
    }

    public void Attack()
    {
        soAttackSystem.Attack(attacker);
    }

    public abstract void Initialize(SceneObject sceneObject, SoSeekSystemBase SeekSystem, List<SceneObjectTypeEnum> possibleTargetTypes, SoAttackSystem attackSystem, Mover mover = null);
}
