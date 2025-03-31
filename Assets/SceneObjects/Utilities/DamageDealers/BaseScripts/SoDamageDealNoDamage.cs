using UnityEngine;
[System.Serializable]
public class SoDamageDealNoDamage : DamagerBaseClass
{


    public override float CalculateAttackRange()
    {
        return attackRange;
    }

    public override int CalculateDamageImplementation(int _baseDamage)
    {
        return 0;
    }

    public override float CalculateReloadTime()
    {
        return reloadTime;
    }
}