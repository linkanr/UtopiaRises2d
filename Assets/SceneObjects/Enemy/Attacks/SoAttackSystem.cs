using UnityEngine;

public class SoAttackSystem:ScriptableObject
{
    public Transform prefabForAmmo;
    public float maxRange;
    public int damage;
    public int attackTimerMax;

    public virtual void Attack(Enemy enemy, IDamageable idamageableByEnememy)
    {

    }
}
