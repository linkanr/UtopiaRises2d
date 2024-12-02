using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenHandler : MonoBehaviour
{
    public RectTransform canvas;
    void Start()
    {
        ButtonWithDelegate.CreateThis(()=>StartGame(),canvas, "Start Game");
    }

    private void StartGame()
    {
        GlobalActions.OnClickStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
