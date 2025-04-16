using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/AttackAndDie")]
public class SoAttackAndDie : SoAttackSystem

{

    public override void Attack(SceneObject attacker)
    {
        if (attacker == null)
        {
            Debug.LogError("Attacker is null");
            return;
        }
        IcanAttack icanAttack = attacker as IcanAttack;

        if (!icanAttack.targeter.target.IsValid())
        {
            Debug.LogError("Target is not valid");
            return;
        }
        if (icanAttack.targeter.target.damagable == null)
        {
            Debug.LogError($"Target object exists, but damagable component is null for {attacker.name}.");
            return;
        }

        icanAttack.targeter.target.damagable.healthSystem.TakeDamage(attacker.GetStats().damageAmount, attacker);
        if (attacker.healthSystem == null)
        {
            Debug.LogError("Attacker health system is null");
            return;
        }
            attacker.KillSceneObject();

    }
}