using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 1;

    public LevelBase currentLevel;
    public Action LoadNextBattleScene;
    public Action LoadSpoilsSceneAction;
    public Action LoadMapSceneAction;
    private void Awake()
    {

        instance = this;
    }
    private void OnEnable()
    {
        GlobalActions.BattleSceneCompleted += LoadSpoilsScene;
        GlobalActions.SpoilScenesCompleted += LoadMapScene;
        GlobalActions.OnNodeCleared += LoadNextLevel;

    }
    private void OnDisable()
    {
        GlobalActions.BattleSceneCompleted -= LoadSpoilsScene;
        GlobalActions.SpoilScenesCompleted -= LoadMapScene;
        GlobalActions.OnNodeCleared -= LoadNextLevel;
    }

    private void LoadMapScene()
    {
        LoadMapSceneAction();
    }

    private void LoadSpoilsScene()
    {
        LoadSpoilsSceneAction();
    }

    private void LoadNextLevel(MapNode mapNode)
    {
        currentLevel = mapNode.levelBase;
        LoadNextBattleScene();
    }


}
