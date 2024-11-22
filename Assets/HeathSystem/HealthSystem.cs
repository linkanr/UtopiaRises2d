using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
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


    public bool TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        OnDamaged?.Invoke(this, new EventArgs { });
        damageEffect.TakeDamage(damageAmount, GetTransformPosition());
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Transform GetTransformPosition()
    {
        return transform;
    }

    public int GetHealth()
    {
        return health;
    }
}