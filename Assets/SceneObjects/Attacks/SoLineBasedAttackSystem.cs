using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Attacks/BasicLineAttack")]
public class SoLineBasedAttackSystem : SoAttackSystem

{
     
    public override void Attack(ICanAttack attacker, Target defender)
    {

        Debug.Log("attacking attacker is " + attacker.attacker.GetStats().GetString(StatsInfoTypeEnum.name) + "defender is " + defender.transform.gameObject.GetComponent<SceneObject>().GetStats().GetString(StatsInfoTypeEnum.name));
        Transform ammo = Instantiate(prefabForAmmo, attacker.attacker.transform);
        ammo.GetComponent<IIsAttackInstanciator>().Trigger(attacker,defender);
        defender.damagable.TakeDamage(damage);

    }
}