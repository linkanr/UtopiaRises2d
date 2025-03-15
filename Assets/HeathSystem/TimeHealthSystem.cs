using System;
using UnityEngine;

public class TimeHealthSystem : HealthSysten
{

    public float timeToLive;
    public float maxTimeToLive;
    public TimeTickerForIHasLifeSpan timeTicker;
    public event EventHandler OnLifeChanged;
    public override bool HandleDamage(float damage)
    {
        TakeDamage(damage);
        OnLifeChanged?.Invoke(this, EventArgs.Empty);
        return CheckForDeath();
    }

    private bool CheckForDeath()
    {
        if ((timeToLive < 1))
        {
            return true;
        }
        return false;
    }


    private void TakeDamage(float amount)
    {
        Debug.Log("Taking damage " + amount + " time to live is " + timeToLive);
        timeToLive -= amount;
        Debug.Log("Time to live is now " + timeToLive);
    }
    public void SetInitialHealth(float _maxTimeToLive)
    {
        maxTimeToLive = _maxTimeToLive;
        timeToLive = maxTimeToLive;
        OnLifeChanged?.Invoke(this, EventArgs.Empty);
    }
}