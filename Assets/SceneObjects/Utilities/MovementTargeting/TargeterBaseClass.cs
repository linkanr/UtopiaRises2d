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
        //    Debug.Log("RemoveTarget() called, but target is already null. Skipping.");
            return;
        }

        if (target.damagable?.healthSystem != null)
        {
           // Debug.Log($"Removing target: {target.damagable.GetStats().name}");
            target.damagable.healthSystem.OnKilled -= (sender, e) => RemoveTarget();
        }

        target.RemoveTarget();
        target = null;
    }




    public virtual void SetNewTarget(SceneObject _iDamageable)
    {
        if (_iDamageable == null)
        {
      //      Debug.LogError("SetNewTarget was called with a null SceneObject!");
            return;
        }



        target = new Target(_iDamageable, this);
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

    private void Update()
    {
        if (target != null)
        {
            if (target.IsValid())
            {
                float angle = WorldSpaceUtils.GetAngleFromVector(attacker.transform.position - target.damagable.transform.position);
                if (attacker.objectAnimator != null)
                {
                    attacker.objectAnimator.SetAngle(angle);
                }

            }      
        }

    }

    public abstract void Initialize(SceneObject sceneObject, SoSeekSystemBase SeekSystem, List<SceneObjectTypeEnum> possibleTargetTypes, SoAttackSystem attackSystem, Mover mover = null);
}
