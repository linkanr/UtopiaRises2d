using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelListerManager : SerializedMonoBehaviour
{
    public static LevelListerManager instance;
    private List<LevelBase> levels;
    private Dictionary<LevelBase, int> levelDictionary;
    private Dictionary<LevelBase, string> levelByName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is more than one LevelListerManager in the scene");
            Destroy(this);
        }
        levels = new List<LevelBase>();
        LevelBase[] loadedLevels = Resources.LoadAll<LevelBase>("LevelBases");
        foreach (LevelBase level in loadedLevels)
        {
            if (level != null)
            {
                levels.Add(level);
            }
        }
        levelDictionary = new Dictionary<LevelBase, int>();
        foreach (LevelBase level in levels)
        {
            levelDictionary.Add(level, level.levelDiffculty);
        }
        levelByName = new Dictionary<LevelBase, string>();
        foreach (LevelBase level in levels)
        {
            if (level != null && !string.IsNullOrEmpty(level.levelName))
            {
                levelByName.Add(level, level.levelName);
            }
        }
    }
    public LevelBase GetLevelByName(string name)
    {
        foreach (KeyValuePair<LevelBase, string> kvp in levelByName)
        {
            if (kvp.Value == name)
            {
                return kvp.Key;
            }
        }
        Debug.LogError("Level not found: " + name);
        return null;
    }
    public List<LevelBase> GetBaseMaps(int difficulty)
    {
        List<LevelBase> levelBases = new List<LevelBase>();
        foreach (KeyValuePair<LevelBase, int> kvp in levelDictionary)
        {
            if (kvp.Value == difficulty)
            {
                levelBases.Add(kvp.Key);
            }
        }
        return levelBases;


    }
    public LevelBase GetRandomLevel(int difficulty)
    {
        Debug.Log("Getting random level" + difficulty);


        List<LevelBase> levelBases = GetBaseMaps(difficulty);
        return levelBases[Random.Range(0, levelBases.Count)];
    }
}

