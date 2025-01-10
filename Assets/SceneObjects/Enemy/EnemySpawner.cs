using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Diagnostics;

public class EnemySpawner : MonoBehaviour
{

    public List<EnemyWave> enemyWaves;
    public SoEnemyBase soEnemyBase;
    public Transform SpawnPos;
    private float timer;
    private float timerMax = .45f;

    private float actualTimer;
    public bool spawn = false;
    EnemyEnumToGO enemyEnumToGameObjectList;
    private void Start()
    {
        enemyEnumToGameObjectList = Resources.Load("EnemyGo") as EnemyEnumToGO;
        actualTimer = timerMax;
        
    }

    // Update is called once per frame
    void Update()
    {  if (spawn)
        {
            timer += BattleClock.Instance.deltaValue;
            if (timer > actualTimer)
            {
                
                SoEnemyObject soEnemyObject = Instantiate( enemyEnumToGameObjectList.EnemyEnumToName[enemyWaves[0].enemyList[0]]);
             
                Vector3 position = SpawnPos.position + WorldSpaceUtils.GetRandomDirection(5f,1f,1f) * 5f * Mathf.Pow(UnityEngine.Random.Range(0f, 1f), .3f);

                EnemyCreator.CreateEnemy(soEnemyObject,position);

                timer = 0;
                actualTimer = timerMax;
                ReduceList();
            }
        }

    }

    private void ReduceList()
    {
        
        if (enemyWaves[0].enemyList.Count == 1)
        {
            
            
            if (enemyWaves.Count == 1)
            {
                enemyWaves.RemoveAt(0);
                Debug.Log("end of all lists");
                BattleSceneActions.OnAllEnemiesSpawned();
            }
            else
            {
                actualTimer += enemyWaves[1].timer;
                Debug.Log("end of list");
                enemyWaves.RemoveAt(0);
            }
            

        }
        else
        {
            enemyWaves[0].enemyList.RemoveAt(0);
        }
    }
}
