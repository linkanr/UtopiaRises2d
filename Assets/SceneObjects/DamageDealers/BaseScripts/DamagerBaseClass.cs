using UnityEngine;

public abstract class DamagerBaseClass:ScriptableObject
{

    public int baseDamage;
    public float reloadTime;
    public float attackRange;

    public abstract int CaclulateDamage();

    public abstract float CalculateReloadTime();

    public abstract float CalculateAttackRange();



  
}
