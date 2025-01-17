using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IdamagableComponent
{
    public HealthSystem healthSystem  { get; private set; }

    public SceneObject sceneObject { get; set; }

    public bool isDead => healthSystem.health > 0;

    public event EventHandler<IdamageAbleArgs> OnDeath;

    public void Die()
    {
        if (!sceneObject.isDead)
        {
            sceneObject.isDead = true;
            if (OnDeath == null)
            {
                Debug.Log("no subscriber to event");
            }
            else
            {
                Debug.Log("invoking deathevent");
            }
            OnDeath?.Invoke(this, new IdamageAbleArgs { damageable = this });
            sceneObject.DestroySceneObject();
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

    public IdamagableComponent Init(SceneObject _sceneObject)
    {
        sceneObject = _sceneObject;
        healthSystem = _sceneObject.GetComponent<HealthSystem>();
        return this;


    }

    public void TakeDamage(int amount)
    {
        if (sceneObject.isDead)
        {
            return;
        }
        float fdamage =  amount * sceneObject.GetStats().takesDamageMultiplier;
        int idamage = (int)fdamage;
        bool hasDied = healthSystem.TakeDamage(idamage);
        if (hasDied)
        {
            Die();
        }
    }
}