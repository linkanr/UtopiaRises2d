using UnityEngine;
using UnityEngine.VFX;

public class SoAttackSystem:ScriptableObject
{
    public Sprite displayRangeSprite;
    public VisualEffect visualEffect;
    public virtual void Attack(TargeterBaseClass attacker, Target defender, int damage)
    {

    }
}
