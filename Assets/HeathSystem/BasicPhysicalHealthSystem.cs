using System;
using UnityEngine;
using DamageNumbersPro;
[RequireComponent(typeof(SceneObject))]
public class BasicPhysicalHealthSystem : PhysicalHealthSystem
{
    public override bool HandleDamage(float damage)
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
