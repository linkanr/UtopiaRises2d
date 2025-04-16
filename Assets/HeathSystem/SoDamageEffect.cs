using DamageNumbersPro;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DamageEffect/DamageEffect")]
public class SoDamageEffect:ScriptableObject
{
    public DamageNumber damageNumbertTakedamage;
    public DamageNumber damagaInfo;

    public void ShowNumbers(Vector3 pos, int amount, string text ="")
    {
        Debug.Log("ShowNumbers called with value: " + amount + " and text: " + text);
        if (!string.IsNullOrEmpty(text))
        {
            DamageNumber damageInfo = damagaInfo.Spawn(pos, text);
        }
        if (amount != 0)
        {
            DamageNumber damageNumber = damageNumbertTakedamage.Spawn(pos, amount);
        }
       

       
    }


}