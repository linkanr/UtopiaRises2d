using System;
using UnityEngine;

/// <summary>
/// Handles the logic of taking damage a break out of the sceneObject class
/// The health system hanldes the actual health and is called from this when damage is taken
/// </summary>
public class PhysicalHealthHandler : MonoBehaviour, IdamagablePhysicalComponent
{
    public PhysicalHealthSystem healthSystem  { get; private set; }

    public SceneObject sceneObject { get; set; }

    public bool isDead => healthSystem.health > 0;

    public event EventHandler<IdamageAbleArgs> OnDeath;
    public void Die()
    {
        if (!sceneObject.isDead)
        {
            sceneObject.isDead = true;

            Debug.Log($"{sceneObject.GetStats().name} is dying. Notifying subscribers...");

            if (OnDeath != null)
            {
                OnDeath.Invoke(this, new IdamageAbleArgs { damageable = this });
            }
            else
            {
                Debug.Log("No subscribers to OnDeath event.");
            }

            healthSystem.health = 0;
            sceneObject.OnSceneObjectDestroyedBase();
        }
    }



    public Transform GetTransform()
    {
        if (!sceneObject.isDead)
        {
            return sceneObject.transform;
        }
        else
            return null;
    }

    public void Init(SceneObject _sceneObject)
    {
        sceneObject = _sceneObject;
        healthSystem = _sceneObject.GetComponent<PhysicalHealthSystem>();
        healthSystem.SetInitialHealth(sceneObject.GetStats().health);

     


    }


    public void TakeDamage(float amount)
    {
        if (sceneObject.isDead)
        {
            return;
        }
        BattleSceneActions.OnSceneObjectTakesPhysicalDamage?.Invoke(sceneObject);
        float fdamage =  amount * sceneObject.GetStats().takesDamageMultiplier;
        int idamage = (int)fdamage;
        bool hasDied = healthSystem.TakeDamage(idamage);
        if (hasDied)
        {
            Die();
        }
    }


}