using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalActions
{
    public static Action<int> TakeDamage;
    public static Action Dead;
    public static Action<int> OnLifeChange;
    public static Action<int> OnMoneyChange;
    public static Action<ToolTipArgs>  Tooltip;
    public static Action StartGame;
}

public class ToolTipArgs
{
    public string Tooltip;
    public float time;
}