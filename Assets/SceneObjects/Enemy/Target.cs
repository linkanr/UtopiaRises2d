using Pathfinding;
using System;
using System.Data;
using UnityEngine;
/// <summary>
/// Target Has a damagable and a transform . Its constructior set the ICanAttack so that it looses its target when the damagagable dies
/// IsValid is used for null and alive check. 
/// </summary>
public class Target
{
    /// <summary>
    /// the constructor takes both target and attacker and sets up a event call that clears the target on death
    /// </summary>
    /// <param name="the target, a scene object with idamageable"></param>
    /// <param name="attacker"></param>
    public Target (IDamageAble _damageable, TargeterBaseClass attacker) 
    {
        damagable = _damageable;
        //Debug.Log("creating new target attacker is " + attacker.attacker.GetStats().name + " defender is " + _damageable.idamageableComponent.sceneObject.GetStats().name);
        damagable.idamageableComponent.OnDeath += (sender, e) => attacker.RemoveTarget();
        transform = damagable.idamageableComponent.GetTransform();
    }

    public IDamageAble damagable { get; set; } 
    public Transform transform { get { return damagable.idamageableComponent.GetTransform(); } private set { } }
    public bool isDead { get { return damagable.idamageableComponent.sceneObject.isDead;} }
    /// <summary>
    /// IsValid checks if it both have a transform and life above zero
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        if (transform == null)
        {
            Debug.Log("transform is null");
            return false;
        }
        if (!isDead)
        {
            return true;
        }
        Debug.Log("target dead");
        return false;
    }
    public void Set(IDamageAble _damageable, Transform _transform)
    {
        damagable = _damageable;
        transform = _transform;
        
    }
    public void RemoveTarget()
    {
        damagable = null;
        transform=null;
       
    }


}