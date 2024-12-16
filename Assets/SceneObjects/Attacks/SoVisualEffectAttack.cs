
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/VisualEffect")]
public class SoVisualEffectAttack : SoAttackSystem

{

	public override void Attack(ICanAttack attacker, Target idamageable)
	{

		idamageable.damagable.TakeDamage(damage);
		visualEffect.Play();

	}
}