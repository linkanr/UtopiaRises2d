using System;

public abstract class GlobalVariableModifier : IDisposable
{
    public bool MarkedForRemoval { get; private set; }

    public event Action<BasicGlobalVariableModifier> OnDispose = delegate { };

    private CountdownTimer timer;

    protected GlobalVariableModifier(ModifierLifetime lifetime, float duration = 0f)
    {
        if (lifetime == ModifierLifetime.Timed && duration > 0f)
        {
            timer = new CountdownTimer(duration);
            timer.OnTimerStop += () => MarkedForRemoval = true;
            timer.OnTimerStop += Dispose;
            timer.Start();
        }
    }

    public float RemainingTime => timer?.Time ?? 0f;

    public void Dispose()
    {
        timer?.Dispose();
        OnDispose(this as BasicGlobalVariableModifier);
    }

    public abstract void Handle(object sender, PlayerGlobalQuery query);
}
