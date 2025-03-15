using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager instance;
    public BattleSceneStateMachine stateMachine;
    public PlayerGlobalVariables playerGlobalVariables;
    private void OnEnable()
    {
        BattleSceneActions.OnEmemyDefeated += LevelClear;
    }
    private void OnDisable()
    {
        BattleSceneActions.OnEmemyDefeated -= LevelClear;
    }
    private void LevelClear()
    {
        TimeActions.OnPause(true);
        GlobalActions.BattleSceneCompleted();
    }

    private void Awake()
    {
        instance = this;
        stateMachine = GetComponent<BattleSceneStateMachine>();
        playerGlobalVariables = new PlayerGlobalVariables();
    }
    private void Update()
    {
 
    }

}
