
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyWave
{
    public EnemyWave(List<EnemyNames> enemies)
    {
        
        List<EnemyNames> eList = new List<EnemyNames>();

        foreach(EnemyNames e in enemies)
        {

      
            eList.Add(e);
        }

        enemyList = eList;
    }
    public List<EnemyNames> enemyList;


    public static List<EnemyWave> CreateWaveList(List<EnemyWave> enemies)
    {
        List<EnemyWave> copiedWaves = new List<EnemyWave>();
        foreach (EnemyWave enemyWave in enemies)
        {
            EnemyWave newWave = new EnemyWave(enemyWave.enemyList);
            copiedWaves.Add(newWave);
        }
        return copiedWaves;
    }

    public bool IsListEmepty { get { if (enemyList.Count == 0) return true; else return false; }  }
}

