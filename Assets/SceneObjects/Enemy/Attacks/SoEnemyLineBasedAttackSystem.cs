using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/Enemy/BasicAttack")]
public class SoEnemyLineBasedAttackSystem : SoAttackSystem

{
     
    public override void Attack(Enemy enemy, IDamageable idamageable)
    {
        
        idamageable.TakeDamage(damage);
        Transform ammo = Instantiate(prefabForAmmo, enemy.transform);
        ammo.GetComponent<IIsAttackInstanciator>().Trigger(enemy,idamageable.GetTransform());
        
    }
}