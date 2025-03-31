using UnityEngine;
[System.Serializable]
public abstract class DamagerBaseClass
{

    public int baseDamage;
    public float reloadTime;
    public float attackRange;

    public int CaclulateDamage()
    {
        return CalculateDamageImplementation( PlayerGlobalsManager.instance.playerGlobalVariables.GetDamage(baseDamage));
    }

    public abstract float CalculateReloadTime();

    public abstract float CalculateAttackRange();
    public abstract int CalculateDamageImplementation(int _baseDamage);





}
