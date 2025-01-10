using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : BaseStateMachine<GameStateMachine>
{
    protected override void Init()
    {
        GameManager.instance.LoadNextBattleScene += LoadNextScene;
    }
    private void OnDisable()
    {
        GameManager.instance.LoadNextBattleScene -= LoadNextScene;
    }
    private void LoadNextScene()
    {
        SetState(typeof(SoLoadBattleScene));
    }
}
