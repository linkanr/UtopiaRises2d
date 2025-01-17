using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DamageDealers/BasicDamage")]
public class SoDamageDealGetDamage : DamagerBaseClass
{
    public override int CaclulateDamage()
    {
        return baseDamage;
    }

    public override float CalculateAttackRange()
    {
        return attackRange;
    }

    public override float CalculateReloadTime()
    {
        return reloadTime;
    }
}
