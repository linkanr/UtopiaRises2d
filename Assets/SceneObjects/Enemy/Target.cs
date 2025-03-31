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
    public Target(SceneObject _damageable, TargeterBaseClass attacker)
    {
        if (_damageable == null)
        {
            Debug.LogError("Target constructor received a null IDamageAble!");
            return;
        }
        if (_damageable.healthSystem == null)
        {
            Debug.LogError("IDamageAble's idamageableComponent is null!");
            return;
        }

        damagable = _damageable;
        transform = damagable.transform;

        if (damagable != null && attacker != null)
        {
            damagable.healthSystem.OnKilled += HandleDeath;
        }

        void HandleDeath(object sender,  OnSceneObjectDestroyedArgs onSceneObjectDestroyedArgs)
        {
            if (attacker != null && attacker.target != null)  // Prevent duplicate calls
            {
                attacker.RemoveTarget();
            }

            // Unsubscribe immediately to prevent multiple calls
            if (damagable?.healthSystem != null)
            {
                damagable.healthSystem.OnKilled -= HandleDeath;
            }
        }
    }






    public SceneObject damagable { get; set; }
    public Transform transform
    {
        get { return damagable != null ? damagable.transform : null; }
        private set { }
    }
    public bool isDead { get { return damagable.isDead;} }
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
        if (isDead)
        {
            Debug.Log("target dead");
            return false;
        }
        return true;
    }
    public void Set(SceneObject _damageable, Transform _transform)
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