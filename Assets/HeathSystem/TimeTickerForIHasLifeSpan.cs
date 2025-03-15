using System;

using UnityEngine;

public class TimeTickerForIHasLifeSpan : MonoBehaviour
{

    TimeHealthHandler timeHealthHandler;
    public Action<float> OnTimeChange;
    

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        TimeActions.OnSecondChange -= TimeTick;
        OnTimeChange -= timeHealthHandler.TakeDamage;
    }


    public void Init(TimeHealthHandler _timeHealthHandler)
    {
        TimeActions.OnSecondChange += TimeTick;
        timeHealthHandler = _timeHealthHandler;
        OnTimeChange += timeHealthHandler.TakeDamage;

    }
    private void TimeTick()
    {
        Debug.Log("TimeTick ");
        OnTimeChange?.Invoke(1f);


    }



}