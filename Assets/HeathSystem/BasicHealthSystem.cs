using System;
using UnityEngine;
using DamageNumbersPro;
[RequireComponent(typeof(SceneObject))]
public class BasicHealthSystem : HealthSystem
{
    protected override bool HandleDamage(int damage)
    {
        if (health < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
