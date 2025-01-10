using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneStateMachine : BaseStateMachine<BattleSceneStateMachine>
{
    protected override void Init()
    {
        
    }
    private void OnEnable()
    {
        GlobalActions.OnBattleSceneLoaded += StartStateMachine;
    }
    private void OnDisable()
    {
        GlobalActions.OnBattleSceneLoaded -= StartStateMachine;
    }

    private void StartStateMachine()
    {
        SetState(typeof(SoBattleScenePreScene));
    }
}