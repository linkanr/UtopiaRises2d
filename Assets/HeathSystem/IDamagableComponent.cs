using System;
using UnityEngine;

public interface IDamagableComponent
{

    public abstract void TakeDamage(float amount);
    /// <summary>
    /// On death has listners that trigger on death effects such as SpriteChange and targeting systems
    /// </summary>
    /// 
    public event EventHandler<IdamageAbleArgs> OnDeath;
    public abstract void Init(SceneObject sceneObject);
    public abstract void Die();
    public abstract Transform GetTransform();
    public abstract SceneObject sceneObject { get; set; }
    public abstract bool isDead { get; }

}