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
    public string levelString { get { return levelPrefix + level.ToString(); } }
    public Action LoadNextBattleScene;
    private void Awake()
    {
        level = 1;
        instance = this;
    }
    private void OnEnable()
    {
        GlobalActions.BattleSceneCompleted += LoadNextLevel;
    }

    private void LoadNextLevel()
    {
        level++;
        LoadNextBattleScene();
    }
}
