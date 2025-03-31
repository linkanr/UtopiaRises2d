using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Levels/BaseLevel")]
public class LevelBase:ScriptableObject
{
    public List<SoEnemyBaseInformation> soEnemyBaseEnemyLists;
    public int levelDiffculty;
    public Texture2D map;
    public int luck;
}