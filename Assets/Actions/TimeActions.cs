using System;


    public static class TimeActions
    {

    public static Action<BattleSceneTimeArgs> GlobalTimeChanged;
    public static Action OnSecondChange;
    /// <summary>
    /// Triggered every 0.223 seconds. For ofsync ticks that are not delta dependent
    /// </summary>
    public static Action OnQuaterTick;
    public static Action<bool> OnPause;
    }
public class BattleSceneTimeArgs
{
    public float time;
    public float deltaTime;
}