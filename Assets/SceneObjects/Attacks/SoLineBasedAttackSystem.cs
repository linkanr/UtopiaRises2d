using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/BasicLineAttack")]
public class SoLineBasedAttackSystem : SoAttackSystem

{
     
    public override void Attack(SceneObject attacker, IDamageable defender)
    {
        
        defender.TakeDamage(damage);
        Transform ammo = Instantiate(prefabForAmmo, attacker.transform);
        ammo.GetComponent<IIsAttackInstanciator>().Trigger(attacker,defender);
        
    }
}