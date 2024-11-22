
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyWave
{
    public EnemyWave(List<EnemyNames> enemies, float _timer)
    {
        
        List<EnemyNames> eList = new List<EnemyNames>();

        foreach(EnemyNames e in enemies)
        {

      
            eList.Add(e);
        }
        timer = _timer;
        enemyList = eList;
    }
    public List<EnemyNames> enemyList;
    public float timer;
    
    
    public bool IsListEmepty { get { if (enemyList.Count == 0) return true; else return false; }  }
}