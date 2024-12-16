using System;
using UnityEngine;

public interface IDamageable
{
    public HealthSystem healthSystem { get; }
    public SceneObject sceneObject { get; }
    public iDamageableTypeEnum damageableType {get;}

    /// <summary>
    /// This should be initialized with a get component in the awake
    /// </summary>
    /// <param name="healthSystem"></param>
    public void SetHealthSystem(HealthSystem healthSystem);
    public void TakeDamage(int amount);
    /// <summary>
    /// Die must call Destroy scene object
    /// </summary>
    public void Die();

    /// <summary>
    /// a check to see if life is less than 1
    /// </summary>
    /// <returns></returns>
    public bool IsDead();
    public Transform GetTransform();
    /// <summary>
    /// This adds the class to the list of damagables In the SceneObjectManager this should call public static Action<IDamageable> OnDamagableCreated
    /// </summary>
    public void OnCreated(); 
/// <summary>
/// On death has listners that trigger on death effects such as SpriteChange and targeting systems
/// </summary>
    public event EventHandler<IdamageAbleArgs> OnDeath;

    
}

public class IdamageAbleArgs
{
    public IDamageable damageable;

}
[System.Serializable]
public enum iDamageableTypeEnum
{
    playerbuilding,
    follower,
    enemy,
    enemyBase,
    playerBase
}