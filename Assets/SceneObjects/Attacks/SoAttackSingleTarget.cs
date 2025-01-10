using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/BasicAttack")]
public class SoAttackSingleTarget : SoAttackSystem

{
    public Transform prefabForAmmo;
    public override void Attack(TargeterBaseClass attacker, Target defender, int damage)
    {

        //Debug.Log("attacking attacker is " + attacker.attacker.GetStats().GetString(StatsInfoTypeEnum.name) + "defender is " + defender.transform.gameObject.GetComponent<SceneObject>().GetStats().GetString(StatsInfoTypeEnum.name));
        if (prefabForAmmo != null)
        {
            Transform ammo = Instantiate(prefabForAmmo, attacker.attacker.transform);
            ammo.GetComponent<IIsAttackInstanciator>()?.Trigger(attacker, defender);
        }

        visualEffect.Play();
        defender.damagable.idamageableComponent.TakeDamage(damage);

    }
}