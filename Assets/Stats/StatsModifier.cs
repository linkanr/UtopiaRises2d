using System;
using UnityEngine;

public abstract class StatsModifier : IDisposable
{
    public bool markedForRemoval { get; private set; }
    public event Action<StatsModifier> OnDispose = delegate { };
    private readonly Timer timer;

    protected StatsModifier(float duration)
    {
        if (duration <= 0) return;

        timer = new CountdownTimer(duration);
        timer.OnTimerStop += () => markedForRemoval = true;
        timer.OnTimerStop += Dispose;
        timer.Start();
    }

    // New method to prolong the duration
    public void ProlongDuration(float additionalTime)
    {
        if (additionalTime > 0 && timer != null)
        {
            timer.AddTime(additionalTime); // Assuming CountdownTimer supports AddTime
            Debug.Log($"Effect prolonged by {additionalTime} seconds.");
        }
    }

    public void Dispose()
    {
        OnDispose.Invoke(this);
    }

    public abstract void Handle(object sender, Query query);
}


public class BasicStatModifier : StatsModifier
{
    readonly StatsInfoTypeEnum type;
    readonly Func<float,float> operation;
    public BasicStatModifier(StatsInfoTypeEnum type, float _duration, Func<float, float> operation): base(_duration)
    {
        this.type = type;
        this.operation = operation;
    }

    public override void Handle(object sender, Query query)
    {
        if (query.statsType == type)
        {
            query.value = operation(query.value);
        }
    }
}
