using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;

public class DamageNumbersManager : SerializedMonoBehaviour
{
    public static DamageNumbersManager instance;

    [Title("Effect Prefabs by Type")]
    [DictionaryDrawerSettings(KeyLabel = "Effect Type", ValueLabel = "Effect Prefab")]
    public Dictionary<damageEffectEnum, SoDamageEffect> effectMap = new();

    private void OnEnable()
    {

        BattleSceneActions.OnSceneObjectTakesDamage += HandleDamgageDelt;
    }



    private void OnDisable()
    {

        BattleSceneActions.OnSceneObjectTakesDamage -= HandleDamgageDelt;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowNumber(Vector3 pos, int value, damageEffectEnum effectType, string label = "")
    {
        Debug.Log("ShowNumber called with value: " + value + " and effectType: " + effectType);
        var effect = effectMap[effectType];

        effect.ShowNumbers(pos, value, label);
    }
    private void HandleDamgageDelt(OnDamageArgs args)
    {
        if (args == null) 
            Debug.LogError("Damage args is null");
        if (args.damageAmount == 0)
            return;
        if (args.damageAmount < 0)
        {
            ShowNumber(args.defender.transform.position, (int)-args.damageAmount, damageEffectEnum.heal);
        }
        else
        {
            ShowNumber(args.defender.transform.position, (int)args.damageAmount, damageEffectEnum.takeDamage);
        }


    }
    public void OnGlobalModifierAdded(GlobalVariableModifier mod, Vector3 pos, int amount)
    {
        if (mod is BasicGlobalVariableModifier basic)
        {

               
               
                switch (basic.varType)
                {
                    case PlayerGlobalVarTypeEnum.DamageModifier:
                        ShowNumber(pos,0, damageEffectEnum.damageBoost, "buff");
                        break;

                    case PlayerGlobalVarTypeEnum.RangeModifier:
                        ShowNumber(pos, 0, damageEffectEnum.maxRange, "range");
                        break;

                }

            


        }
    }

}


public enum damageEffectEnum
{
        takeDamage,
        damageBoost,
        heal,   
        maxRange,

}