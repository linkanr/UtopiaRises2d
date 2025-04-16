using UnityEngine;
[System.Serializable]
public class SoDamageDealNoDamage : DamagerBaseClass
{




    public override int CalculateDamageImplementation(int _baseDamage)
    {
        return 0;
    }

    public override float CalculateReloadTime()
    {
        return reloadTime;
    }

    public override void InitImplemantation()
    {
    }
    public override DamagerBaseClass Clone()
    {
        return new SoDamageDealNoDamage
        {
            baseDamage = this.baseDamage,
            reloadTime = this.reloadTime,
            attackRange = this.attackRange
        };
    }

}