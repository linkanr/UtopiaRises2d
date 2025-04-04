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
    public Action LoadEventScene;
    private void Awake()
    {

        instance = this;
    }
    private void OnEnable()
    {
        GlobalActions.BattleSceneCompleted += LoadSpoilsScene;
        GlobalActions.GoBackToMap += LoadMapScene;
        GlobalActions.OnNodeClicked += LoadNextLevel;

    }
    private void OnDisable()
    {
        GlobalActions.BattleSceneCompleted -= LoadSpoilsScene;
        GlobalActions.GoBackToMap -= LoadMapScene;
        GlobalActions.OnNodeClicked -= LoadNextLevel;

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
        if (mapNode.nodeTypeEnum == MapNodeTypeEnum.randomEvent)
        {
            LoadEventScene();

        }
        else
        {
            currentLevel = mapNode.battleLevelBase;
            LoadNextBattleScene();
        }

    }


}
