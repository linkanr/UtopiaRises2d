using UnityEngine;
[System.Serializable]
public class SoDamagerForEnemies : DamagerBaseClass
{


    public override float CalculateAttackRange(Stats statsOveride= null)
    {
        return attackRange;
    }
    public override int CaclulateDamage(Stats statsOveride = null)
    {

        return CalculateDamageImplementation(baseDamage);
    }

    public override int CalculateDamageImplementation(int _baseDamage)
    {
        return _baseDamage;
    }

    public override float CalculateReloadTime()
    {
        return reloadTime;
    }

    public override void InitImplemantation(SceneObject _sceneObject)
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
