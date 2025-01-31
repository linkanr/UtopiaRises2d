using System;
using UnityEngine;
/// <summary>
/// Adds functionality to be able to take damage, The corresponding SO needs to have health
/// </summary>
public interface IdamagableComponent
{
    public HealthSystem healthSystem { get; }
    public SceneObject sceneObject { get; }
    public bool isDead { get; }

    public IdamagableComponent Init(SceneObject sceneObject);
    /// <summary>
    /// This should be initialized with a get component in the awake
    /// </summary>
    /// <param name="healthSystem"></param>

    public void TakeDamage(int amount);
    /// <summary>
    /// Die must call Destroy scene object
    /// </summary>
    public void Die();

    /// <summary>
    /// a check to see if life is less than 1
    /// </summary>
    /// <returns></returns>

    public Transform GetTransform();

/// <summary>
/// On death has listners that trigger on death effects such as SpriteChange and targeting systems
/// </summary>
    public event EventHandler<IdamageAbleArgs> OnDeath;

    
}

public class IdamageAbleArgs
{
    public IdamagableComponent damageable;

}
[System.Serializable]
public enum SceneObjectTypeEnum
{
    playerbuilding,
    follower,
    enemy,
    enemyBase,
    playerBase,
    enviromentObject,
    playerConstructionBase,
    all,
    
}