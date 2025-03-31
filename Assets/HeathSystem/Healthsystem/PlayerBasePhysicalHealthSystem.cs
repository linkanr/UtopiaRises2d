using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneObject))]
public class PlayerBasePhysicalHealthSystem : PhysicalHealthSystem
{
    private void Start()
    {
        Init(HealthManager.Instance.health, sceneObject);
    }
    public override bool HandleDamage(float damage)
    {
        int iDamage = (int)damage;
        BattleSceneActions.OnBaseDamaged(iDamage);

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
