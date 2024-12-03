
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/VisualEffect")]
public class SoVisualEffectAttack : SoAttackSystem

{

	public override void Attack(SceneObject attacker, IDamageable idamageable)
	{

		idamageable.TakeDamage(damage);
		visualEffect.Play();

	}
}