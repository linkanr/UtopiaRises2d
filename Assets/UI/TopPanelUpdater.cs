using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanelUpdater : MonoBehaviour
{
    public TextMeshProUGUI healthGUI;
    public TextMeshProUGUI moneyGUI;
    public TextMeshProUGUI followersGUI;
    public TextMeshProUGUI influenceGUI;

    private void OnEnable()
    {
        GlobalActions.OnLifeChange += OnLifeChange;
        GlobalActions.OnMoneyChange += OnMoneyChange;
        BattleSceneActions.OnFollowerCountChanged += OnFollowerChanged;
        BattleSceneActions.OnInfluenceChanged += OnInfluenceChanged;
        
    }

    private void OnInfluenceChanged(int obj)
    {
        influenceGUI.text = obj.ToString(); 
    }

    private void OnFollowerChanged(int obj)
    {
        followersGUI.text = obj.ToString();
    }

    private void OnMoneyChange(int obj)
    {
        moneyGUI.text = obj.ToString();
    }

    private void OnLifeChange(int obj)
    {
        healthGUI.text = obj.ToString();
    }
}
