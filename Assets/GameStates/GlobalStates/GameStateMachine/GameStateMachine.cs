using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : BaseStateMachine<GameStateMachine>
{
    protected override void Init()
    {
        GameManager.instance.LoadNextBattleScene += LoadNextScene;
        GameManager.instance.LoadSpoilsSceneAction += LoadSpoilsScene;
    }
    private void OnDisable()
    {
        GameManager.instance.LoadNextBattleScene -= LoadNextScene;
        GameManager.instance.LoadSpoilsSceneAction -= LoadSpoilsScene;
    }
    private void LoadNextScene()
    {
        SetState(typeof(SoLoadBattleScene));
    }
    private void LoadSpoilsScene()
    {
        SetState(typeof(SoLoadSpoilsScene));
    }
}
