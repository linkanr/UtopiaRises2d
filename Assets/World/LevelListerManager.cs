using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelListerManager : SerializedMonoBehaviour
{
    public static LevelListerManager instance;
    public List<LevelBase> levels;
    private Dictionary<LevelBase, int> levelDictionary;

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
        levelDictionary = new Dictionary<LevelBase, int>();
        foreach (LevelBase level in levels)
        {
            levelDictionary.Add(level, level.levelDiffculty);
        }
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

