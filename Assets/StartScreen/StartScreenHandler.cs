using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenHandler : MonoBehaviour
{
    public List<SoCardList> StartingLayouts;
    public int StartingLayoutIndex;
    public RectTransform canvas;
    void Start()
    {
        ButtonWithDelegate.CreateThis(()=>StartGame(),canvas, "Start Game");
    }
    public void SetIndex(int index)
    {
        StartingLayoutIndex = index;
    }
    private void StartGame()
    {
        CardManager.instance.startingCards = StartingLayouts[StartingLayoutIndex];
        CardManager.instance.AddStartingCardsToDeck();
        GlobalActions.OnClickStartGame();
    }

    
}
