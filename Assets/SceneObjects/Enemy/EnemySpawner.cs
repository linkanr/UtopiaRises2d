using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Action OnEnemySpawned;
    public List<EnemyWave> enemyWaves;
    public List<EnemyWave> origWaves;
    public EnemyBase enemyBase;
    public Vector3 spawnPos;

    EnemyEnumToGO enemyEnumToGameObjectList;


    private void OnEnable()
    {
        BattleSceneActions.OnSpawningStarting += SpawnNextWave;
    }

    private void OnDisable()
    {
        BattleSceneActions.OnSpawningStarting -= SpawnNextWave;
    }

    private void Start()
    {
        enemyEnumToGameObjectList = Resources.Load("EnemyGo") as EnemyEnumToGO;
    }

    public void Init(List<EnemyWave> _enemyWaves, Vector3 pos, EnemyBase _enemyBase)
    {
        enemyWaves = EnemyWave.CreateWaveList(_enemyWaves);
        origWaves = EnemyWave.CreateWaveList(_enemyWaves);
        spawnPos = pos;
        enemyBase = _enemyBase;
        enemyBase.Onkilled += StartDestruction;
    }
    private void StartDestruction()
    {
        StartCoroutine(DestroySpawner());
    }
    private void SpawnNextWave()
    {
        if (enemyWaves.Count == 0)
            return;
        Debug.Log("Spawning wave");
        StartCoroutine(SpawnWaveCoroutine(enemyWaves[0]));
    }

    private IEnumerator SpawnWaveCoroutine(EnemyWave wave)
    {
        foreach (EnemyNames enemyName in wave.enemyList)
        {
            SoEnemyObject enemySO = Instantiate(enemyEnumToGameObjectList.EnemyEnumToName[enemyName]);
            Vector3 position = spawnPos + WorldSpaceUtils.GetRandomDirection(.5f, 2f, 0f);
            position.z = 0f;
            EnemyCreator.CreateEnemy(enemySO, position);
            

            yield return new WaitForSeconds(0.3f); // <-- adjustable spawn interval
        }

        enemyWaves.RemoveAt(0);
        OnEnemySpawned?.Invoke();

        if (enemyWaves.Count == 0)
        {
            if (enemyBase.permanent)
            {
                enemyWaves = EnemyWave.CreateWaveList(origWaves);
            }
            else
            {
                enemyBase.KillSceneObject();
                StartCoroutine(DestroySpawner());
            }
        }
    }

    private IEnumerator DestroySpawner()
    {
        yield return new WaitForEndOfFrame();
        EnemyManager.Instance.RemoveSpawener(this);
        Destroy(gameObject);
    }

    // -------------------
    // Info Methods Below
    // -------------------

    public Dictionary<Sprite, int> GetEnemieInNextWave()
    {
        EnemyWave enemyWave;
        if (enemyWaves.Count == 0 && !enemyBase.permanent)
        {
            return new Dictionary<Sprite, int>();
        }
        if (enemyWaves.Count == 0 && enemyBase.permanent)
        {
            enemyWave = origWaves[0];
        }
        else
        {
            enemyWave = enemyWaves[0];
        }

        Dictionary<Sprite, int> enemyList = new Dictionary<Sprite, int>();

        foreach (EnemyNames enemyName in enemyWave.enemyList)
        {
            var sprite = enemyEnumToGameObjectList.EnemyEnumToName[enemyName].sprite;

            if (enemyList.ContainsKey(sprite))
                enemyList[sprite]++;
            else
                enemyList.Add(sprite, 1);
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


}
