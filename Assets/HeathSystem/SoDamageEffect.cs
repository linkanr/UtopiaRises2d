using DamageNumbersPro;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DamageEffect/DamageEffect")]
public class SoDamageEffect:ScriptableObject
{
    public DamageNumber damageNumbertTakedamage;
    public DamageNumber damagaInfo;

    public void TakeDamage(int amount, Transform sendingItem)
    {
        damageNumbertTakedamage.Spawn(sendingItem.position, amount, sendingItem);
    }
    public void DamageText(string text, Transform sendingItem)
    {
        damagaInfo.Spawn(sendingItem.position, text, sendingItem);
    }

}