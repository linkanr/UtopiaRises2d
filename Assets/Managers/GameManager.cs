using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 1;
    private string levelPrefix = "Level";
    private string levelString { get { return levelPrefix + level.ToString(); } }
    public SoEnemyLevelList soEnemyLevelList { get { return Resources.Load(levelString) as SoEnemyLevelList; } }
    public Action LoadNextBattleScene;
    public Action LoadSpoilsSceneAction;
    private void Awake()
    {
        level = 1;
        instance = this;
    }
    private void OnEnable()
    {
        GlobalActions.BattleSceneCompleted += LoadSpoilsScene;
        GlobalActions.SpoilScenesCompleted += LoadNextLevel;

    }

    private void LoadSpoilsScene()
    {
        LoadSpoilsSceneAction();
    }

    private void LoadNextLevel()
    {
        level++;
        LoadNextBattleScene();
    }
}
