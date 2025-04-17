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
        BattleSceneActions.OnGlobalModifersChanged += HandleGlobalModifier;
    }


    private void OnDisable()
    {

        BattleSceneActions.OnSceneObjectTakesDamage -= HandleDamgageDelt;
        BattleSceneActions.OnGlobalModifersChanged -= HandleGlobalModifier;
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
    private void HandleGlobalModifier(GlobalVariableModifier mod)
    {
        if (mod is not BasicGlobalVariableModifier basic) return;

        string label = GetGlobalModifierDescription(basic);
        var effect = MapEffectType(basic.varType);
        var targets = SceneObjectUtils.GetObjectsAffectedByGlobalModifier(basic);

        foreach (var obj in targets)
        {
            var stats = obj.GetStats();
            if (stats == null) continue;

            ShowNumber(stats.sceneObjectTransform.position, 0, effect, label);
        }
    }
    private string GetGlobalModifierDescription(BasicGlobalVariableModifier mod)
    {
        string symbol = mod.modType == GlobalModificationType.Multiply ? "x" : "+";
        string value = mod.amount.ToString("0.##");

        return mod.varType switch
        {
            PlayerGlobalVarTypeEnum.DamageModifier => $"{symbol}{value} damage",
            PlayerGlobalVarTypeEnum.RangeModifier => $"{symbol}{value} range",
            PlayerGlobalVarTypeEnum.ExtraHeal => $"{symbol}{value} heal",
            PlayerGlobalVarTypeEnum.ExtraLifetime => $"{symbol}{value} time",
            _ => $"{symbol}{value}"
        };
    }

    private damageEffectEnum MapEffectType(PlayerGlobalVarTypeEnum type)
    {
        return type switch
        {
            PlayerGlobalVarTypeEnum.DamageModifier => damageEffectEnum.damageBoost,
            PlayerGlobalVarTypeEnum.RangeModifier => damageEffectEnum.maxRange,
            PlayerGlobalVarTypeEnum.ExtraHeal => damageEffectEnum.heal,
            PlayerGlobalVarTypeEnum.ExtraLifetime => damageEffectEnum.maxRange,
            _ => damageEffectEnum.damageBoost
        };
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