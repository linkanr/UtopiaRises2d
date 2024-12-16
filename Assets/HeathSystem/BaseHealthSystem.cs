using System;
using UnityEngine;

[RequireComponent(typeof(SceneObject))]
public class BaseHealthSystem : HealthSystem
{
    private void Start()
    {
        SetInitialHealth(HealthManager.Instance.health);
    }
    protected override bool HandleDamage(int damage)
    {
        BattleSceneActions.OnBaseDamaged(damage);
        if (health != HealthManager.Instance.health)
        {
            Debug.LogWarning("health is not matching");
        }
        if (health <= 0)

        {

            return true;
        }
        else
        {
            return false;
        }
        
    }


}
