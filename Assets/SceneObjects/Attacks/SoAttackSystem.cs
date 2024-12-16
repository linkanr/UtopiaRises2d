using UnityEngine;
using UnityEngine.VFX;

public class SoAttackSystem:ScriptableObject
{
    public Transform prefabForAmmo;
    public float maxRange;
    public int damage;
    public int attackTimerMax;
    public VisualEffect visualEffect;

    public virtual void Attack(ICanAttack attacker, Target defender)
    {

    }
}
