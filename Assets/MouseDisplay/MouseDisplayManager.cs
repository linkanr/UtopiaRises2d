using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDisplayManager : MonoBehaviour
{
    public MouseDisplayStateMachine stateMachine;
    public static Action<OnSetSpriteArgs> OnSetNewSprite;
    public static Action OnRemoveDisplay;
}

public class OnSetSpriteArgs
{
    public Sprite sprite;
    public float size;
}
