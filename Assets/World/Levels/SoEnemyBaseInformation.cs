using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Levels/EnemyWaveList")]
public class SoEnemyBaseInformation : ScriptableObject
{
    public SoEnemyBase SoEnemyBase;

    public List<EnemyWave> enemyWaveList;
    public int luck;
    
}
