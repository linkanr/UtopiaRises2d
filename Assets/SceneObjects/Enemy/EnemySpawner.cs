using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Diagnostics;

public class EnemySpawner : MonoBehaviour
{
    public Action OnEnemySpawned;
    public Action OnSpawnerTimeChange;
    public List<EnemyWave> enemyWaves;
    public List<EnemyWave> origWaves;
    public EnemyBase enemyBase;
    public Vector3 spawnPos;
    private float timeToNextSpawnTimer;
    private float spawnTimerMax;
    private bool allSpawned = false;
    public bool currentWaveDone = false;
    private bool isLockedForSpawning = false;
    public bool spawningEnabledNotPaused = false;

    EnemyEnumToGO enemyEnumToGameObjectList;

    private void OnEnable()
    {
        TimeActions.GlobalTimeChanged += ChangeTimer;
    }

    private void ChangeTimer(BattleSceneTimeArgs args)
    {
        if (!spawningEnabledNotPaused)
        {
            return;
        }
        timeToNextSpawnTimer += args.deltaTime;



    }

    private void Start()
    {
        enemyEnumToGameObjectList = Resources.Load("EnemyGo") as EnemyEnumToGO;
    
        

    }
    public void Init(List<EnemyWave> _enemyWaves, Vector3 pos, EnemyBase enemyBase)
    {
        enemyWaves = new List<EnemyWave>();
        Debug.Log("init spawner enemy waves count " + _enemyWaves.Count);
        enemyWaves.AddRange(_enemyWaves);

        origWaves = EnemyWave.CreateWaveList(_enemyWaves);
        spawnPos = pos;
        enemyBase.Onkilled += DestroySpawner;
    }
    private void DestroySpawner()
    {
        StartCoroutine(Destroy());
    }
    public IEnumerator Destroy()
    {
        if (isLockedForSpawning)
        {
            yield return new WaitForEndOfFrame();
        }
        enemyWaves.Clear();
        EnemyManager.Instance.RemoveSpawener(this);
        Destroy(gameObject);
    }
    public int GetTimeLeft()
    {
        return BattleSceneManager.instance.GetTimeLeft();
    }
    // Update is called once per frame
    void Update()
    {  
        if (allSpawned)
        {
            return;
        }
        if (currentWaveDone)
        {
            return;
        }

        if (!spawningEnabledNotPaused)
        {
            return;
        }

            if (timeToNextSpawnTimer > spawnTimerMax)
            {
                isLockedForSpawning = true;
                timeToNextSpawnTimer = 0;
                SpawnEnemy();
                OnEnemySpawned?.Invoke();
            }
           
        



    }

    private float CaluculateNextWaveAmount()
    {
        Dictionary<Sprite, int> enemyAmount = GetEnemieInNextWave();
        int amount = 0;
        if (enemyAmount.Count == 0)
        {
            return 0;
        }
        foreach (KeyValuePair<Sprite, int> enemy in enemyAmount)
        {
            amount += enemy.Value;
        }
        return amount;
    }

    private void SpawnEnemy()
    {
        SoEnemyObject soEnemyObject = Instantiate(enemyEnumToGameObjectList.EnemyEnumToName[enemyWaves[0].enemyList[0]]);

        Vector3 position = spawnPos + WorldSpaceUtils.GetRandomDirection(.5f, 2f, 0f) * 1f * Mathf.Pow(UnityEngine.Random.Range(0f, 1f), .3f);
        position.z = 0f;
        position.x += 1f;
        EnemyCreator.CreateEnemy(soEnemyObject, position);


        ReduceList();
        isLockedForSpawning = false;
    }

    private void ReduceList()
    {
        
        if (enemyWaves[0].enemyList.Count == 1)
        {
          

            if (enemyWaves.Count == 1)
            {
                enemyWaves.RemoveAt(0);
                if (enemyBase.permanent)
                {
                    enemyWaves.Clear();
                    
                    enemyWaves.AddRange(EnemyWave.CreateWaveList(origWaves));
                }
                else
                {

                    allSpawned = true;
                    enemyBase.healthSystem.Die(null);
                }


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


    private void CurrentWaveDone()
    {
        currentWaveDone = true;
        Debug.Log("end of list");
        enemyWaves.RemoveAt(0);
    }

    internal Dictionary<Sprite,int> GetEnemieInNextWave()
    {
        if (enemyWaves.Count == 0)
        {
            return new Dictionary<Sprite, int>();
        }
        EnemyWave enemyWave = enemyWaves[0];
        Dictionary<Sprite, int> enemyList = new Dictionary<Sprite, int>();
        foreach (EnemyNames enemyName in enemyWave.enemyList)
        {
            if (enemyList.ContainsKey(enemyEnumToGameObjectList.EnemyEnumToName[enemyName].sprite))
            {
                enemyList[enemyEnumToGameObjectList.EnemyEnumToName[enemyName].sprite]++;
            }
            else
            {
                enemyList.Add(enemyEnumToGameObjectList.EnemyEnumToName[enemyName].sprite, 1);
            }
        }
        return enemyList;
    }
    public List<EnemyNames> GetAllEnemies()
    {
        List<EnemyNames> enemies = new List<EnemyNames>();
        foreach (EnemyWave wave in enemyWaves)
        {
            enemies.AddRange(wave.enemyList);
        }
        return enemies;
    }

    internal void CaluclateSpawnTime()
    {
        spawnTimerMax = 1f/ CaluculateNextWaveAmount();
    }
}
