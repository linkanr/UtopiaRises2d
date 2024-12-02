using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static ShootingBuilding;
[RequireComponent(typeof(EnemySpawner))]
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private EnemySpawner spawner;
    public EnemyLooker looker;

     
    public SoEnemyLevelList soEnemyLevelList;// this should be set by game manger
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
        spawner = GetComponent<EnemySpawner>();
        looker = new EnemyLooker(this);
        
        
        spawner.enemyWaves = CreateInstancesOfTheEnemies(); 
        spawner.soEnemyBase = soEnemyLevelList.SoEnemyBase;
        
        
    }


    private void OnEnable()
    {
        BattleSceneActions.OnInitializeScene += SpawEnemyBase;
        BattleSceneActions.OnAllEnemiesSpawned += RestartSpawning;
    }

    private void OnDisable()
    {
        BattleSceneActions.OnAllEnemiesSpawned -= RestartSpawning;
        BattleSceneActions.OnInitializeScene -= SpawEnemyBase;
    }

    private void SpawEnemyBase()
    {
        EnemyBase.Create(soEnemyLevelList.basePosition, soEnemyLevelList.SoEnemyBase);
    }

    private void RestartSpawning()
    {
        spawner.enemyWaves = CreateInstancesOfTheEnemies();
    }

    public void SetSpawning(bool onOff)
    {
        spawner.spawn = onOff;
    }

    public List<EnemyWave> CreateInstancesOfTheEnemies()
    {
        List<EnemyWave> enemyWaveList = new List<EnemyWave>();
        foreach (EnemyWave enemyWave in soEnemyLevelList.enemyWaveList)
        {
            EnemyWave newWave = new EnemyWave(enemyWave.enemyList, enemyWave.timer);
            enemyWaveList.Add(newWave);
        }
        return enemyWaveList;
    }

    
}
