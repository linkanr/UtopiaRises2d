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
        //Debug.Log("removing target");
        target.RemoveTarget();
        target = null;
        
    }

    public virtual void SetNewTarget(SceneObject _iDamageable)
    {
        Debug.Log("SettingnewTarget");
        target = new Target(_iDamageable as IDamageAble, this);
        OnTargetChanged?.Invoke(target);
    }

    public bool CheckIfTargetIsInDistance()
    {
        if (target == null || attacker == null)
        {
            return false;
        }
            
        if (!target.IsValid())
        {
            return false;
        }
        if (Vector2.Distance(target.transform.position, attacker.transform.position) < attacker.GetStats().maxShootingDistance)
        {
            Debug.Log("target within distance");
            return true;
        }
        return false;
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
