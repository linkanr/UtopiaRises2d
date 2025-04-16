using System;
using UnityEngine;

public class TimeHealthSystem : HealthSystem
{


    public TimeTickerForIHasLifeSpan timeTicker;

    public override bool HandleDamage(float damage)
    {

        return CheckForDeath();
    }


    private bool CheckForDeath()
    {
        if ((GetHealth() < 1))
        {
            return true;
        }
        return false;
    }
    private void Start()
    {
        timeTicker = gameObject.AddComponent<TimeTickerForIHasLifeSpan>();
        timeTicker.Init(this);
    }




}