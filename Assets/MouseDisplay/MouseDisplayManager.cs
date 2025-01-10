using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDisplayManager : MonoBehaviour
{
    public static MouseDisplayManager instance;
    public MouseDisplayStateMachine stateMachine;
    public static Action<OnSetSpriteArgs> OnSetNewSprite;
    public static Action OnRemoveDisplay;
    public BaseState<MouseDisplayStateMachine> prevState;
    public bool mouseOverCard;
    public bool highligtSceneObjects;

    public bool displayCellChange;
    public int displaySizeX;
    public int displaySizeY;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("double");
    }
    private void OnEnable()
    {
       // Debug.Log("mouse Over card sub");
        DisplayActions.OnDisplayCell += OnHandleCellchange;
        DisplayActions.OnMouseOverCard += OnMouseOverCard;
        DisplayActions.OnMouseNotOverCard += OnMouseNotOverCard;
        DisplayActions.OnHighligtSceneObject += OnHighlightSceneObject;
    }


    private void OnDisable()
    {
        DisplayActions.OnDisplayCell -= OnHandleCellchange;
        DisplayActions.OnMouseOverCard -= OnMouseOverCard;
        DisplayActions.OnMouseNotOverCard -= OnMouseNotOverCard;
        DisplayActions.OnHighligtSceneObject -= OnHighlightSceneObject;
    }
    private void OnHighlightSceneObject(bool obj)
    {
        highligtSceneObjects = obj;
    }
    private void OnHandleCellchange(OnDisplayCellArgs obj)
    {
        displayCellChange = obj.setDisplay;
        displaySizeX = obj.sizeX;
        displaySizeY = obj.sizeY;
    }

    private void OnMouseNotOverCard()
    {
        mouseOverCard = false;
    }

    private void OnMouseOverCard()
    {
        mouseOverCard = true;
    }
}


public class OnSetSpriteArgs
{
    public Sprite sprite;
    public float size;
}
