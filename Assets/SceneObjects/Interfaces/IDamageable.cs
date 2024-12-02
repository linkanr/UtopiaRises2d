using System;
using UnityEngine;

public interface IDamageable
{
    public HealthSystem healthSystem { get; }
    public SceneObject sceneObject { get; }
    public iDamageableTypeEnum damageableType {get;}
    public void SetHealthSystem(HealthSystem healthSystem);
    public void TakeDamage(int amount);
    public void Die();
    public bool IsDead();
    public Transform GetTransform();
    public void OnCreated(); // Add to the list of scene objects
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