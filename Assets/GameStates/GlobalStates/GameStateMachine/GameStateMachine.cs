using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : BaseStateMachine<GameStateMachine>
{
    protected override void Init()
    {
        GameManager.instance.LoadNextBattleScene += LoadBattleScene;
        GameManager.instance.LoadSpoilsSceneAction += LoadSpoilsScene;
        GameManager.instance.LoadMapSceneAction += LoadMapScene;
        GameManager.instance.LoadEventScene += LoadEventScene;
    }


    private void OnDisable()
    {
        GameManager.instance.LoadNextBattleScene -= LoadBattleScene;
        GameManager.instance.LoadSpoilsSceneAction -= LoadSpoilsScene;
        GameManager.instance.LoadMapSceneAction -= LoadMapScene;
        GameManager.instance.LoadEventScene -= LoadEventScene;
    }
    private void LoadMapScene()
    {
        SetState(typeof(SoLoadMapScene));
    }

    private void LoadBattleScene()
    {
        SetState(typeof(SoLoadBattleScene));
    }
    private void LoadSpoilsScene()
    {
        SetState(typeof(SoLoadSpoilsScene));
    }
    private void LoadEventScene()
    {
        SetState(typeof(SoLoadEventScene));
    }

}
