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
    private float spawnTimer;
    private float spawnTimerMax = .5f;
    private float timerMax = 10f;

    private float actualTimer;
    public bool spawn = false;
    public bool timerReachedSpawn = false;
    EnemyEnumToGO enemyEnumToGameObjectList;

    private void OnEnable()
    {
        TimeActions.GlobalTimeChanged += ChangeTimer;
    }

    private void ChangeTimer(BattleSceneTimeArgs args)
    {
        timer += args.deltaTime;
        spawnTimer += args.deltaTime;
    }

    private void Start()
    {
        enemyEnumToGameObjectList = Resources.Load("EnemyGo") as EnemyEnumToGO;
        timer = 5f;
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (spawn && !timerReachedSpawn)
        {
            if (timer > timerMax)
            {
                timerReachedSpawn = true;
                timer = 0;

            }
        }
        if (timerReachedSpawn)
        {
            if (spawnTimer > spawnTimerMax)
            {
                spawnTimer = 0;
                SpawnEnemy();
            }
           
        }



    }

    private void SpawnEnemy()
    {
        SoEnemyObject soEnemyObject = Instantiate(enemyEnumToGameObjectList.EnemyEnumToName[enemyWaves[0].enemyList[0]]);

        Vector3 position = SpawnPos.position + WorldSpaceUtils.GetRandomDirection(.5f, 2f, 1f) * 1f * Mathf.Pow(UnityEngine.Random.Range(0f, 1f), .3f);

        EnemyCreator.CreateEnemy(soEnemyObject, position);


        ReduceList();
    }

    private void ReduceList()
    {
        
        if (enemyWaves[0].enemyList.Count == 1)
        {
            
            
            if (enemyWaves.Count == 1)
            {
                AllWavesDone();
            }
            else
            {
                CurrentWaveDone();
            }


        }
        else
        {
            enemyWaves[0].enemyList.RemoveAt(0);
        }
    }

    private void AllWavesDone()
    {
        timerReachedSpawn = false;


        

    }

    private void CurrentWaveDone()
    {
        timerReachedSpawn = false;
        //Debug.Log("end of list");
        enemyWaves.RemoveAt(0);
    }
}
