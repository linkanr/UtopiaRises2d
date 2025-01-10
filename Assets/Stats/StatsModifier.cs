using System;

public abstract class StatsModifier:IDisposable
{
    public bool markedForRemoval {  get; private set; }
    public event Action<StatsModifier>OnDispose = delegate { };
    readonly Timer timer;
    protected StatsModifier(float _duration)
    {
        if (_duration <= 0) return;

        timer = new CountdownTimer(_duration);
        timer.OnTimerStop += () => markedForRemoval = true;
        timer.OnTimerStop += Dispose;
        timer.Start();
    }
    public void Dispose()
    {

        OnDispose.Invoke(obj:this);
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
