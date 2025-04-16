using System;
using UnityEngine;

public abstract class StatsModifier : IDisposable
{
    public bool markedForRemoval { get; private set; }
    public event Action<StatsModifier> OnDispose = delegate { };
    private readonly Timer timer;
    public PickupTypes pickupTypes { get; protected set; }

    protected StatsModifier(float duration, PickupTypes pickupTypes)
    {
        if (duration <= 0) return;

        timer = new CountdownTimer(duration);
        timer.OnTimerStop += () => markedForRemoval = true;
        timer.OnTimerStop += Dispose;
        timer.Start();
        this.pickupTypes = pickupTypes;
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
        timer.Dispose();
        OnDispose.Invoke(this);

    }

    public abstract void Handle(object sender, Query query);
}


public class BasicStatModifier : StatsModifier
{
    public readonly StatsInfoTypeEnum type;
    public readonly Func<float,float> operation;
    public BasicStatModifier(StatsInfoTypeEnum type, float _duration, Func<float, float> operation, PickupTypes pickupTypes) : base(_duration, pickupTypes)
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
