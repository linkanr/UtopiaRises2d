using UnityEngine;
[System.Serializable]
public class SoDamageDealGetDamage : DamagerBaseClass
{



    public override int CalculateDamageImplementation(int _baseDamage)
    {
        return _baseDamage;
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
        return new SoDamageDealGetDamage
        {
            baseDamage = this.baseDamage,
            reloadTime = this.reloadTime,
            attackRange = this.attackRange
        };
    }

}
