using System;
using UnityEngine;

public class PhysicalHealthSystem : HealthSystem

{
    public override bool HandleDamage(float damage)
    {
        if (GetHealth() < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }












}