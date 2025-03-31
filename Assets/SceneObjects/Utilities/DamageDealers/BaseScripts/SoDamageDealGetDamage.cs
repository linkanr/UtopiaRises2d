using UnityEngine;
[System.Serializable]
public class SoDamageDealGetDamage : DamagerBaseClass
{


    public override float CalculateAttackRange()
    {
        return attackRange;
    }

    public override int CalculateDamageImplementation(int _baseDamage)
    {
        return _baseDamage;
    }

    public override float CalculateReloadTime()
    {
        return reloadTime;
    }
}
