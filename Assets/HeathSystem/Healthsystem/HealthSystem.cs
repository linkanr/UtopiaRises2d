using Sirenix.OdinInspector.Editor.Drawers;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class HealthSystem:MonoBehaviour
{

    private int health;
    public int maxHealth;
    protected SceneObject sceneObject;


    public event EventHandler<OnDamageArgs> OnDamaged;
    public event EventHandler<OnSceneObjectDestroyedArgs> OnKilled;
    public abstract bool HandleDamage(float damage);
    public virtual float GetHealthAmountNormalized()
    {
        return (float)health / (float)maxHealth;
    }
    public void Heal(int amount, bool toMaxHealthOnly = false)
    {
        if(toMaxHealthOnly)
        {
            health = Mathf.Min(maxHealth, health+amount) ;
        }
        else
        {
            health += amount;
        }

        var healArgs = new OnDamageArgs { attacker = null, defender = sceneObject, damageAmount = -amount };
        BattleSceneActions.OnSceneObjectTakesDamage?.Invoke(new OnDamageArgs { attacker = null, defender = sceneObject, damageAmount = -amount });
        OnDamaged?.Invoke(this, healArgs);


    }
    public bool sceneobjectIsDead =>sceneObject.isDead;
    public virtual void Die(SceneObject attacker)
    {

        sceneObject.OnSceneObjectDestroyedBase();
        OnKilled?.Invoke(this, new OnSceneObjectDestroyedArgs { victim = sceneObject,killer = attacker });
        BattleSceneActions.OnSceneObjectKilled?.Invoke(new OnSceneObjectDestroyedArgs { killer=attacker,victim=sceneObject});
    }

    public virtual void Init(int _maxHealth, SceneObject _sceneObject)
    {
        //Debug.Log("HealthSystem Init");
        if (_sceneObject == null)
        {
            Debug.LogError("[HealthSystem.Init] sceneObject passed in was null!");
            return;
        }
   
        maxHealth = _maxHealth;
        health = maxHealth;
        sceneObject = _sceneObject;

    }
    public virtual bool TakeDamage(int damageAmount, SceneObject attacker)
    {
        if (sceneObject == null)
        {
            Debug.LogError("[TakeDamage] Failed: sceneObject is null.");
            return false;
        }

        var stats = sceneObject.GetStats();
        if (stats == null)
        {
            Debug.LogError("[TakeDamage] Failed: sceneObject.GetStats() returned null.");
            return false;
        }

        float fdamage = damageAmount * stats.takesDamageMultiplier;
        health -= damageAmount;

        try
        {
            OnDamaged?.Invoke(this, new OnDamageArgs { attacker = attacker, defender = sceneObject, damageAmount = damageAmount });
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[TakeDamage] Error invoking OnDamaged: {e.Message}");
        }

        try
        {
            BattleSceneActions.OnSceneObjectTakesDamage?.Invoke(new OnDamageArgs { attacker = attacker, defender = sceneObject, damageAmount = damageAmount });
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[TakeDamage] Error invoking BattleSceneActions.OnSceneObjectTakesDamage: {e.Message}");
        }

        bool hasDied = GetHealth() < 1;
        if (hasDied)
        {
            Die(attacker);
        }

        return HandleDamage(damageAmount);
    }


    public int GetHealth()
    {
        return health;
    }

}

public class OnDamageArgs
{
   public SceneObject attacker;
    public SceneObject defender;
    public int damageAmount;
}