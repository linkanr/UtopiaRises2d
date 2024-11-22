using System;
using UnityEngine;

public interface IDamageable
{
    public HealthSystem healthSystem { get; }
    public void SetHealthSystem(HealthSystem healthSystem);
    public void TakeDamage(int amount);
    public void Die();
    public bool IsDead();
    public Transform GetTransform();
/// <summary>
/// On death has listners that trigger on death effects such as SpriteChange and targeting systems
/// </summary>
    public event EventHandler<OnDeathArgs> OnDeath;



}

public class OnDeathArgs
{
    public IDamageable damageable;
}