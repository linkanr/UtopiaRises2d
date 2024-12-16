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
    /// <param name="_damageable"></param>
    /// <param name="attacker"></param>
    public Target (IDamageable _damageable, ICanAttack attacker) 
    {
        Damagable = _damageable;
        if (!Damagable.IsDead())
        {
            Damagable.OnDeath += (sender, e) => attacker.target = null;

            transform = Damagable.GetTransform();
        }
        

        
    }
    private IDamageable Damagable;
    public IDamageable damagable { get  { return Damagable; } } 
    public Transform transform { get { return Damagable.GetTransform(); } private set { } }
    public bool isDead { get { return Damagable.IsDead();} }
    /// <summary>
    /// IsValid checks if it both have a transform and life above zero
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        if (!isDead && transform != null)
        {
            return true;
        }
        return false;
    }
    



}