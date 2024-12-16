using System;

public static class GlobalActions
{

    public static Action Dead;
    public static Action<int> OnLifeChange;
    public static Action<int> OnMoneyChange;
    public static Action<ToolTipArgs> Tooltip;
    public static Action OnClickStartGame;
    public static Action OnPostBattle;
    public static Action OnBattleSceneLoaded; 
    public static Action BattleSceneCompleted; //triggers when All enemies are dead 
}

public class ToolTipArgs
{
    public string Tooltip;
    public float time;
}