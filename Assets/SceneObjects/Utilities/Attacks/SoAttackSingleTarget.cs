using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/BasicAttack")]
public class SoAttackSingleTarget : SoAttackSystem

{
    public Transform prefabForAmmo;
    public override void Attack(SceneObject attacker)
    {
        IcanAttack icanAttack = attacker as IcanAttack;


        //Debug.Log("attacking attacker is " + attacker.attacker.GetStats().GetString(StatsInfoTypeEnum.name) + "defender is " + defender.transform.gameObject.GetComponent<SceneObject>().GetStats().GetString(StatsInfoTypeEnum.name));
        if (prefabForAmmo != null)
        {
            Transform ammo = Instantiate(prefabForAmmo, attacker.transform);
            ammo.GetComponent<IIsAttackInstanciator>()?.Trigger(icanAttack.targeter, icanAttack.targeter.target);
        }

        if (attacker.GetStats().fireEffect != null)
        {
            attacker.GetStats().fireEffect.Play();

        }


        icanAttack.targeter.target.damagable.healthSystem.TakeDamage(attacker.GetStats().damageAmount,attacker);

    }
}