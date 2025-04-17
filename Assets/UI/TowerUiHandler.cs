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
    public int init = 0;
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
        BattleSceneActions.OnGlobalModifersChanged += OnModifierAdded;
        UpdateUI();
    }



    private void OnDisable()
    {
        BattleSceneActions.OnSceneObjectCreated -= UpdateOnSceneObject;
        BattleSceneActions.OnSceneObjectKilled -= UpdateOnSceneObject;
        BattleSceneActions.OnGlobalModifersChanged -= OnModifierAdded;
    }
    private void OnModifierAdded(GlobalVariableModifier modifier)
    {


        UpdateUI();
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
        damageText.text =  stats.damageAmount().ToString();
    }
}

