using System;
using UnityEngine;

public abstract class PhysicalHealthSystem : HealthSysten

{

    public int health;
    public int maxHealth;
    public SoDamageEffect damageEffect;
    public event EventHandler OnDamaged;




    private void Start()
    {
        
    }
    public virtual float GetHealthAmountNormalized()
    {
        return (float)health / (float)maxHealth;
    }

    public virtual void SetInitialHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
        health = maxHealth;

        damageEffect = GetComponent<SceneObject>().GetStats().soDamageEffect;
    }


    public virtual bool TakeDamage(int damageAmount)
    {
        if (damageEffect != null)
        {
            damageEffect.TakeDamage(damageAmount, GetTransformPosition());
        }
            
        health -= damageAmount;
        OnDamaged?.Invoke(this, new EventArgs { });
        return HandleDamage(damageAmount);
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