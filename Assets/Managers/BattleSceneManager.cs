using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public Action OnTimeCountDown;
    public static BattleSceneManager instance;
    public BattleSceneStateMachine stateMachine;
    public PlayerGlobalVariables playerGlobalVariables;
    public float playTime = 10f;
    public float spawntime = 2f;
    public float timer = 0f;

    private void OnEnable()
    {
        BattleSceneActions.OnEmemyDefeated += CheckLevelClear;
        TimeActions.GlobalTimeChanged += ChangeTimer;
        
    }
    private void OnDisable()
    {
        BattleSceneActions.OnEmemyDefeated -= CheckLevelClear;
        TimeActions.GlobalTimeChanged -= ChangeTimer;
    }

    private void ChangeTimer(BattleSceneTimeArgs args)
    {
        
        if (stateMachine.activeState is SoBattleSceneStateSpawningEnemies)
        {
            timer += args.deltaTime;
            if (timer >= spawntime)
            {
                stateMachine.SetState(typeof(SoBattleScenePlaying));
                timer = 0;
            }
        }
        else if (stateMachine.activeState is SoBattleScenePlaying)
        {
            OnTimeCountDown?.Invoke();
            timer += args.deltaTime;
            if (timer >= playTime)
            {
                stateMachine.SetState(typeof(SoBattleSceneStatePlayCards));
                timer = 0;
            }
        }
    }

    private void CheckLevelClear()
    {
        if (EnemyManager.Instance.CheckSpawnCount() == 0)
        {
            TimeActions.OnPause(true);
            GlobalActions.BattleSceneCompleted();
        }
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

    public  int GetTimeLeft()
    {

        if (stateMachine.activeState is SoBattleScenePlaying)
        {
            return (int)(playTime - timer);
        }
        else
        {
            return 0;
        }
    }
}
