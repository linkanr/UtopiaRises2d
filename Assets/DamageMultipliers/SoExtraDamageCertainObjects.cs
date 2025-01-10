using System;
using UnityEngine;
[CreateAssetMenu(menuName = "DamageModifiers/ExtraDamageObjects")]
public class SoExtraDamageCertainObjects : DamageMultiplier
{
    public ExtraDamageAgainstEnum extraDamageAgainst = ExtraDamageAgainstEnum.none;
    public int extraDamageAmount = 0;
    public int extraDamageMulti = 1;
    public SceneObjectTypeEnum sceneObjectExtraDamage;
    public Faction factionExtraDamage;

    public override int GetExtraDamageAmount(int damage, SceneObject target)
    {
        switch (extraDamageAgainst)
        {
            case ExtraDamageAgainstEnum.none:
                return damage;

            case ExtraDamageAgainstEnum.faction:
                if (target.GetStats().faction == factionExtraDamage)
                {
                    Debug.Log("found faction to do extra damage against");
                    return DoExtraDamage(damage);
                }
                return damage;
            case ExtraDamageAgainstEnum.sceneObjectType:
                if (target.GetStats().sceneObjectType == sceneObjectExtraDamage)
                {
                    Debug.Log("found object to do extra damage against");
                    return DoExtraDamage(damage);
                }
                return damage;
        }
        return damage;
    }

    private int DoExtraDamage(int damage)
    {
        damage *= extraDamageMulti;
        damage += extraDamageAmount;
        return damage;
    }
}
