using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/BasicAttack")]
public class SoAttackSingleTarget : SoAttackSystem

{
    public ShotEffectTypeEnum shotEffectType;
    public override void Attack(SceneObject attacker)
    {
        if (attacker is IcanAttack icanAttack)
        {
            var targeter = icanAttack.targeter;
            Target target = targeter?.target;
            var damagable = target?.damagable;
            var healthSystem = damagable?.healthSystem;

            if (target == null || !target.IsValid())
                return;

            Vector3? targetPosition = null;

            try
            {
                targetPosition = target.targetTransform?.position;
            }
            catch
            {
                Debug.LogWarning("Failed to access target position safely.");
                return;
            }

            if (targetPosition == null)
            {
                Debug.LogWarning("Target position was null.");
                return;
            }

            healthSystem.TakeDamage(attacker.GetStats().damageAmount, attacker);

         //   Debug.Log(shotEffectType.ToString());
           // Debug.Log(attacker.transform.position);
            //Debug.Log(targetPosition.Value);

            ShotEffectManager.Instance.PlayShotEffect(
                shotEffectType,
                attacker.transform.position,
                targetPosition.Value
            );
        }
    }

}