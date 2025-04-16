using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUiHandler : MonoBehaviour
{
    public TimeHealthSystem healthSystem;
    public Stats stats;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI damageText;
    public void Init(SceneObject sceneObject)
    {
        if (sceneObject.healthSystem == null)
        {
            Debug.LogError("damageAble.iDamageableComponent is NULL!");
            return;
        }
        sceneObject.healthSystem.OnKilled += DestroyUiPanel;
        healthSystem = sceneObject.healthSystem as TimeHealthSystem;
        healthSystem.OnDamaged += OnDamaged;
        stats = sceneObject.GetStats();
        if (stats == null)
        {
            Debug.LogError("Stats is NULL!");
            return;
        }

        stats.OnStatsChanged += UpdateUI;
        BattleSceneActions.OnSceneObjectCreated += UpdateOnSceneObject;   
        BattleSceneActions.OnSceneObjectKilled += UpdateOnSceneObject;
        BattleSceneActions.OnGlobalModifierAdded += OnModifierAdded;
        UpdateUI();
    }



    private void OnDisable()
    {
        BattleSceneActions.OnSceneObjectCreated -= UpdateOnSceneObject;
        BattleSceneActions.OnSceneObjectKilled -= UpdateOnSceneObject;
        BattleSceneActions.OnGlobalModifierAdded -= OnModifierAdded;
    }
    private void OnModifierAdded(GlobalVariableModifier modifier)
    {
        BasicGlobalVariableModifier basicModifier = modifier as BasicGlobalVariableModifier;
        if (basicModifier == null)
        {
            Debug.LogError("modifier is not a BasicGlobalVariableModifier");
            return;
        }

        string label = GetGlobalModifierDescription(basicModifier);
        DamageNumbersManager.instance?.ShowNumber(transform.position, 0, MapEffectType(basicModifier.varType), label);
        UpdateUI();
    }
    private string GetGlobalModifierDescription(BasicGlobalVariableModifier mod)
    {
        string symbol = mod.modType == GlobalModificationType.Multiply ? "x" : "+";
        string value = mod.amount.ToString("0.##");

        switch (mod.varType)
        {
            case PlayerGlobalVarTypeEnum.DamageModifier:
                return $"{symbol}{value} damage";
            case PlayerGlobalVarTypeEnum.RangeModifier:
                return $"{symbol}{value} range";
            case PlayerGlobalVarTypeEnum.ExtraHeal:
                return $"{symbol}{value} heal";
            case PlayerGlobalVarTypeEnum.ExtraLifetime:
                return $"{symbol}{value} time";
            default:
                return $"{symbol}{value}";
        }
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
    private void DestroyUiPanel(object sender, OnSceneObjectDestroyedArgs e)
    {
        Destroy(gameObject);
    }

    private void UpdateOnSceneObject(SceneObject sceneObject)
    {
        UpdateUI();
    }
    private void UpdateOnSceneObject(OnSceneObjectDestroyedArgs args)
    {
        UpdateUI();
    }
    private void OnDamaged(object sender, OnDamageArgs e)
    {
        
        UpdateUI();
    }
    public void UpdateUI()
    {
        Debug.Log("UpdateUI");
        int time = healthSystem.GetHealth();    
        timeText.text = time.ToString();
        damageText.text =  stats.damageAmount.ToString();
    }
}

