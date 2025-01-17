using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Levels/EnemyWaveList")]
public class SoEnemyLevelList : ScriptableObject
{
    public SoEnemyBase SoEnemyBase;
    public Vector3 basePosition;
    public List<EnemyWave> enemyWaveList;
    public int luck;

}
