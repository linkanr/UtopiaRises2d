using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private List<EnemySpawner> spawners;
    public EnemyLooker looker;
    public Transform enemySpawnerParent;
    bool initialized = false;
    List<Vector3> basePositions;



    public LevelBase currentLevel;// this should be set by game manger
    public List<Enemy> spawnedEnemiesList;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("double trouble");
        }
        basePositions = new List<Vector3>();
        for (int i = 0; i < 6; i++)
        {
            basePositions.Add(new Vector3(5, 5 + i*5, 0));
        }

    }
    public int CheckSpawnCount()
    {
        return spawners.Count;
    }
    public IEnumerator Init()
    {
        if (initialized)
        {
            yield return null;
        }
        initialized = true;
        spawners = new List<EnemySpawner>();
        currentLevel = GameManager.instance.currentLevel;
        Debug.Log(currentLevel);


        looker = new EnemyLooker(this);
        int i = 0;
        List<Vector3> actualBasePos = GeneralUtils.GetRandomFromList(basePositions, currentLevel.soEnemyBaseEnemyLists.Count);
        
        foreach ( SoEnemyBaseInformation soEnemyBaseInformation in GameManager.instance.currentLevel.soEnemyBaseEnemyLists)
        {
            GameObject spawnerObject = new GameObject();
            spawnerObject.transform.parent = enemySpawnerParent;
            spawnerObject.name = "EnemySpawner" + i;
            EnemySpawner spawner = spawnerObject.AddComponent<EnemySpawner>();
            List<EnemyWave> enemyWavesList = EnemyWave.CreateWaveList(soEnemyBaseInformation.enemyWaveList);

            Debug.Log("creating enemy base " + enemyWavesList.Count + " is in enemy list");
            
            EnemyBase enemyBase = EnemyBase.Create(actualBasePos[i], soEnemyBaseInformation.SoEnemyBase);

            spawner.enemyBase = enemyBase;
            enemyBase.spawner = spawner;
            spawner.Init(enemyWavesList, enemyBase.transform.position, enemyBase);
            Debug.Log("spawner " + i + " is created and it has " + spawner.GetAllEnemies() + " enemies");
            spawners.Add(spawner);  
            i++;
        }
        yield return null;

    }
    public void RemoveSpawener(EnemySpawner enemySpawner)
    {
        spawners.Remove(enemySpawner);
    }
    private void Update()
    {
        if (!initialized)
        {
            return;
        }
        if(EnemyCount().totalEnemies == 0)
        {
            Debug.Log("Level Cleared");
            BattleSceneActions.OnEmemyDefeated();
        }
    }


    private void OnEnable()
    {
  
        BattleSceneActions.OnSpawningStarting += RemoveLineViz;
        BattleSceneActions.OnSpawningInterwallEnding += AddLineViz;

    }

    private void AddLineViz()
    {
        foreach (Enemy enemy in spawnedEnemiesList)
        {
            enemy.aIPathVisualizer.ActivateLine(true);
        }
    }

    private void RemoveLineViz()
    {
        foreach (Enemy enemy in spawnedEnemiesList)
        {
            enemy.aIPathVisualizer.ActivateLine(false);
        }
    }

    private void OnDisable()
    {
     
 
        BattleSceneActions.OnSpawningStarting -= RemoveLineViz;
        BattleSceneActions.OnSpawningInterwallEnding -= AddLineViz;
    }





    public void SetSpawning(bool onOff)
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.spawningEnabledNotPaused = onOff;
            spawner.currentWaveDone = false;
            spawner.CaluclateSpawnTime();  
        }
           
    }


    public EnemyCounter EnemyCount()
    {
        int spawnedEnemies = spawnedEnemiesList.Count;
        int enemiesInCurrentWave = 0;
        int enemiesInNextWaves = 0;

        // Check if spawners list exists
        if (spawners == null || spawners.Count == 0)
        {

            return new EnemyCounter
            {
                spawnedEnemies = spawnedEnemies,
                enemiesInCurrentWave = 0,
                enemiesInNextWaves = 0
            };
        }

        // Loop through all spawners
        foreach (EnemySpawner spawner in spawners)
        {
            if (spawner.enemyWaves == null || spawner.enemyWaves.Count == 0)
                continue; // Skip if spawner has no waves

            // Current wave enemies
            if (spawner.enemyWaves[0].enemyList != null)
            {
                enemiesInCurrentWave += spawner.enemyWaves[0].enemyList.Count;
            }

            // Enemies in next waves
            for (int i = 1; i < spawner.enemyWaves.Count; i++)
            {
                if (spawner.enemyWaves[i].enemyList != null)
                {
                    enemiesInNextWaves += spawner.enemyWaves[i].enemyList.Count;
                }
            }
        }

        return new EnemyCounter
        {
            spawnedEnemies = spawnedEnemies,
            enemiesInCurrentWave = enemiesInCurrentWave,
            enemiesInNextWaves = enemiesInNextWaves
        };
    }



}

public struct EnemyCounter
{
    public int spawnedEnemies;
    public int enemiesInCurrentWave;
    public int enemiesInNextWaves;
    public int totalEnemies => spawnedEnemies + enemiesInCurrentWave + enemiesInNextWaves;
}