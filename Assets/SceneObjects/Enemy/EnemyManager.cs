using System.Collections.Generic;

using UnityEngine;
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
    }
    private void Start()
    {

        soEnemyLevelList = GameManager.instance.soEnemyLevelList;
        Debug.Log(soEnemyLevelList);

        spawner = GetComponent<EnemySpawner>();
        looker = new EnemyLooker(this);
        
        
        spawner.enemyWaves = CreateInstancesOfTheEnemies(); 
        spawner.soEnemyBase = soEnemyLevelList.SoEnemyBase;
        
        
    }
    private void Update()
    {
        if(EnemyCount().totalEnemies == 0)
        {
            Debug.Log("Level Cleared");
            BattleSceneActions.OnEmemyDefeated();
        }
    }


    private void OnEnable()
    {
        BattleSceneActions.OnInitializeScene += SpawEnemyBase;
      
    }

    private void OnDisable()
    {
     
        BattleSceneActions.OnInitializeScene -= SpawEnemyBase;
    }

    private void SpawEnemyBase()
    {
        EnemyBase.Create(soEnemyLevelList.basePosition, soEnemyLevelList.SoEnemyBase);
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
    public EnemyCounter EnemyCount()
    {
        int spawnedEnemies = spawnedEnemiesList.Count;
        int enemiesInCurrentWave = 0;
        if (spawner.enemyWaves.Count > 0)
        {
            if (spawner.enemyWaves[0].enemyList.Count > 0)
            {
                enemiesInCurrentWave = spawner.enemyWaves[0].enemyList.Count;
            }
            else
            {
                enemiesInCurrentWave = 0;
            }
               
        }
        else
        {
            enemiesInCurrentWave = 0;
        }

        int enemiesInNextWaves = 0;
        for (int i = 0; i < spawner.enemyWaves.Count; i++)
        {
            enemiesInNextWaves += spawner.enemyWaves[i].enemyList.Count;
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