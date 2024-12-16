using System;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{

    public int health;
    public int maxHealth;
    public SoDamageEffect damageEffect;
    public event EventHandler OnDamaged;




    public float GetHealthAmountNormalized()
    {
        return (float)health / (float)maxHealth;
    }

    public void SetInitialHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
        health = maxHealth;
    }


    public virtual bool TakeDamage(int damageAmount)
    {
        
        damageEffect.TakeDamage(damageAmount, GetTransformPosition());
        health -= damageAmount;
        OnDamaged?.Invoke(this, new EventArgs { });
        return HandleDamage(damageAmount);
    }

    protected abstract bool HandleDamage(int damage);


    public Transform GetTransformPosition()
    {
        return transform;
    }

    public int GetHealth()
    {
        return health;
    }
}