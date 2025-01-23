using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseDisplayStateMachine : BaseStateMachine<MouseDisplayStateMachine>
{

    public Transform spriteGO;
    public SpriteRenderer spriteRenderer;

    protected override void Init()
    {
        spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
        DisplayActions.OnRemoveMouseSprite += ChangeToNoDisplayState;
        DisplayActions.OnSetMouseSprite += ChangeToDisplaySpriteState;
    }
    private void OnDisable()
    {
        DisplayActions.OnRemoveMouseSprite -= ChangeToNoDisplayState;
        DisplayActions.OnSetMouseSprite -= ChangeToDisplaySpriteState;
    }

    private void ChangeToDisplaySpriteState(OnDisplaySpriteArgs onSetSpriteArgs)
    {

        spriteRenderer.sprite = onSetSpriteArgs.sprite;
        spriteGO.transform.localScale = new Vector3 (onSetSpriteArgs.size, onSetSpriteArgs.size, onSetSpriteArgs.size);
        SetState(typeof(SoMouseStateDisplaySprite));
    }

    private void ChangeToNoDisplayState()
    {
        SetState(typeof(SoMouseStateNoDisplay));
    }
    public void TurnOfDisplay()
    {
        spriteGO.gameObject.SetActive(false);
    }
    public void TurnOnDisplay()
    {
        spriteGO.gameObject.SetActive(true);
    }
}
