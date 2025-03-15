using System;
using UnityEngine;

public class TimeHealthHandler : MonoBehaviour, IDamagableTimeComponent
{
    public SceneObject sceneObject { get ; set; }

    public TimeHealthSystem healthSystem { get; set; }
    public TimeTickerForIHasLifeSpan timeTicker { get; set; }
    public bool isDead => healthSystem.timeToLive <= 0;

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
            
            sceneObject.OnSceneObjectDestroyedBase();

        }
    }

    public Transform GetTransform()
    {
        return sceneObject.transform;
    }

    public void Init(SceneObject _sceneObject)
    {
        sceneObject = _sceneObject;
        healthSystem = _sceneObject.GetComponent<TimeHealthSystem>();
        healthSystem.timeTicker = gameObject.AddComponent<TimeTickerForIHasLifeSpan>();
        healthSystem.timeTicker.Init(this);
        healthSystem.SetInitialHealth(sceneObject.GetStats().lifeTime);
        

    }

    public void TakeDamage(float amount)
    {
        //Debug.Log("Taking damage");
        if (healthSystem.HandleDamage(amount))
        {
            Die();
        };
    }
}