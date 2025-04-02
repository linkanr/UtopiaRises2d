using DamageNumbersPro;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DamageEffect/DamageEffect")]
public class SoDamageEffect:ScriptableObject
{
    public DamageNumber damageNumbertTakedamage;
    public DamageNumber damagaInfo;

    public void TakeDamage(object sender, OnDamageArgs onDamageArgs)
    {
        damageNumbertTakedamage.Spawn(onDamageArgs.defender.transform.position, onDamageArgs.damageAmount, onDamageArgs.defender.transform);
    }
    public void DamageText(string text, Transform sendingItem)
    {
        damagaInfo.Spawn(sendingItem.position, text, sendingItem);
    }

}