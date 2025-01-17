using System;


public abstract class Timer
{
    protected float initialTime;
    public float Time { get; set; }
    public bool IsRunning { get; protected set; }

    public float Progress => Time / initialTime;

    public Action OnTimerStart = delegate { };
    public Action OnTimerStop = delegate { };

    protected Timer(float value)
    {
        initialTime = value;
        IsRunning = false;
    }

    public void Start()
    {
        Time = initialTime;
        if (!IsRunning)
        {
            IsRunning = true;
            TimeActions.GlobalTimeChanged += Tick;
            OnTimerStart.Invoke();
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            TimeActions.GlobalTimeChanged -= Tick;
            IsRunning = false;
            OnTimerStop.Invoke();
        }
    }

    public void Resume() => IsRunning = true;
    public void Pause() => IsRunning = false;

    public abstract void Tick(BattleSceneTimeArgs timeArgs);

    internal void AddTime(float additionalTime)
    {
        Time += additionalTime;
    }
}

public class CountdownTimer : Timer
{
    public CountdownTimer(float value) : base(value) { }

    public override void Tick(BattleSceneTimeArgs timeArgs)
    {
        if (IsRunning && Time > 0)
        {
            Time -= timeArgs.deltaTime;
        }

        if (IsRunning && Time <= 0)
        {
            Stop();
        }
    }

    public bool IsFinished => Time <= 0;

    public void Reset() => Time = initialTime;

    public void Reset(float newTime)
    {
        initialTime = newTime;
        Reset();
    }
}

public class StopwatchTimer : Timer
{
    public StopwatchTimer() : base(0) { }

    public override void Tick(BattleSceneTimeArgs timeArgs)
    {
        if (IsRunning)
        {
            Time += timeArgs.deltaTime;
        }
    }

    public void Reset() => Time = 0;

    public float GetTime() => Time;
}