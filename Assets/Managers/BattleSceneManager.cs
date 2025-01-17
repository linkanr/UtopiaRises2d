using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance;
    public BattleSceneStateMachine stateMachine;
    private void OnEnable()
    {
        BattleSceneActions.OnEnemyBaseDestroyed += LevelClear;
    }
    private void OnDisable()
    {
        BattleSceneActions.OnEnemyBaseDestroyed -= LevelClear;
    }
    private void LevelClear()
    {
        TimeActions.OnPause(true);
        GlobalActions.BattleSceneCompleted();
    }

    private void Awake()
    {
        Instance = this;
        stateMachine = GetComponent<BattleSceneStateMachine>();
    }
    private void Update()
    {
 
    }

}
