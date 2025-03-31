using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUiBattle : MonoBehaviour
{
    private void OnEnable()
    {
        BattleSceneActions.OnInitializeScene += Animate;
    }

    private void Animate()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        BattleSceneActions.OnInitializeScene -= Animate;
    }
}
