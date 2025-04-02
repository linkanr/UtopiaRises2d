using UnityEngine;

public class DamageEffectInstansiator: MonoBehaviour
{
    private SoDamageEffect soDamageEffect;
    public void Init(SoDamageEffect _soDamageEffect)
    {
        soDamageEffect = _soDamageEffect;
        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamaged += soDamageEffect.TakeDamage;

    }
}