using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (HealthManager.Instance.health <= 0)

        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


}
