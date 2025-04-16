using System;

public abstract class GlobalVariableModifier : IDisposable
{
    public ModifierLifetime Lifetime { get; private set; }
    public bool MarkedForRemoval { get; private set; }
    public event Action<GlobalVariableModifier> OnDispose = delegate { };

    private CountdownTimer timer;

    protected GlobalVariableModifier(ModifierLifetime lifetime, float duration = 0f)
    {
        Lifetime = lifetime;

        if (Lifetime == ModifierLifetime.Timed && duration > 0f)
        {
            timer = new CountdownTimer(duration);
            timer.OnTimerStop += () => MarkedForRemoval = true;
            timer.OnTimerStop += Dispose;
            timer.Start();
        }
    }

    public void Dispose()
    {
        timer?.Dispose();
        OnDispose(this);
    }

    public abstract void Handle(object sender, PlayerGlobalQuery query);
}
