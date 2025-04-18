using System;

public static class GlobalActions
{

    public static Action Dead;
    public static Action<int> OnLifeChange; // This calls with the amount of life left
    public static Action<int> DoBaseDamage; // this triggers life to be lost
    public static Action<int> OnMoneyChange;
    public static Action<ToolTipArgs> ShowTooltip;
    public static Action OnClickStartGame;
    public static Action OnPostBattle;
    public static Action OnBattleSceneLoaded;
    public static Action BattleSceneCompleted; //triggers when All enemies are dead 
    public static Action GoBackToMap; //triggers when all spoils scenes are completed
    public static Action OnEventSceneLoaded;
    public static Action<SoCardBase> OnNewCardAddedToDeck;
    public static Action OnMapSceneEntered;
    public static Action OnMapSceneExited;
    public static Action<MapNode> OnNodeClicked;
    public static Action OnDebugCreateMap;


}

public class ToolTipArgs
{
    public string Tooltip;
    public float time;
}