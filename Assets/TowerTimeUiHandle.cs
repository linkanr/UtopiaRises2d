using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerTimeUiHandle : MonoBehaviour
{
    public TimeHealthSystem healthSystem;
    public Stats stats;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI damageText;
    public void Init(SceneObject sceneObject)
    {


        IDamageAble damageAble = sceneObject as IDamageAble;


        if (damageAble == null)
        {
            Debug.LogError("sceneObject is NOT an IDamageAble!");
            return;
        }

        if (damageAble.iDamageableComponent == null)
        {
            Debug.LogError("damageAble.iDamageableComponent is NULL!");
            return;
        }

        TimeHealthHandler timeHealthHandler = damageAble.iDamageableComponent as TimeHealthHandler;


        if (timeHealthHandler == null)
        {
            Debug.LogError("iDamageableComponent is NOT a TimeHealthHandler!");
            return;
        }

        healthSystem = timeHealthHandler.healthSystem;

        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem is NULL!");
            return;
        }

        healthSystem.OnLifeChanged += OnDamaged;

        stats = sceneObject.GetStats();


        if (stats == null)
        {
            Debug.LogError("Stats is NULL!");
            return;
        }

        stats.OnStatsChanged += UpdateUI;
        BattleSceneActions.OnSceneObejctCreated += UpdateOnSceneObject;   
        BattleSceneActions.OnSceneObjectDestroyed += UpdateOnSceneObject;
        UpdateUI();
    }

    private void UpdateOnSceneObject(SceneObject @object)
    {
        UpdateUI();
    }

    private void OnDamaged(object sender, EventArgs e)
    {
        
        UpdateUI();
    }
    public void UpdateUI()
    {
        int time = (int)healthSystem.timeToLive;    
        timeText.text = time.ToString();
        damageText.text = stats.damageAmount.ToString();
    }
}

