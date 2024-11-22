using UnityEngine;

public class SoEnemyAttackSystem:ScriptableObject
{
    public Transform prefabForAmmo;
    public float maxRange;
    public int damage;
    public int attackTimerMax;
    public TargetableAttacksEnums enemyAttackType;
    public virtual void Attack(Enemy enemy, IDamageable idamageableByEnememy)
    {

    }
}
