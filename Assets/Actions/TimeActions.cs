using System;


    public static class TimeActions
    {

    public static Action<BattleSceneTimeArgs> GlobalTimeChanged;
    public static Action OnSecondChange;
    public static Action<bool> OnPause;
    }
public class BattleSceneTimeArgs
{
    public float time;
    public float deltaTime;
}