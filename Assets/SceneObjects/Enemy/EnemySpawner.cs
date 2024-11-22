using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Diagnostics;

public class EnemySpawner : MonoBehaviour
{
    public Transform prefab;
    public List<EnemyWave> enemyWaves;
    public Transform SpawnPos;
    private float timer;
    private float timerMax = .45f;

    private float actualTimer;
    public bool spawn = false;

    private void Start()
    {
        actualTimer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {  if (spawn)
        {
            timer += Time.deltaTime;
            if (timer > actualTimer)
            {
                EnemyEnumToGO gi = Resources.Load("EnemyGo") as EnemyEnumToGO ;
                SoEnemyObject soEnemyObject = Instantiate( gi.EnemyEnumToName[enemyWaves[0].enemyList[0]]);
                Enemy.Create(soEnemyObject, SpawnPos.position + WorldSpaceUtils.GetRandomDirection() * 5f * Mathf.Pow(UnityEngine.Random.Range(0f, 1f), .3f), Quaternion.identity);

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
