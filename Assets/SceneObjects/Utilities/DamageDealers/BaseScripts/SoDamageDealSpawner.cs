using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DamageDealers/Spawner")]
public class SoDamageDealSpawner : DamagerBaseClass
{
    public override int CaclulateDamage()
    {
        return 0;
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