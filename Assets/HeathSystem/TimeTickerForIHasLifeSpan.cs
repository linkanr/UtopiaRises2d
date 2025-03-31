using System;

using UnityEngine;

public class TimeTickerForIHasLifeSpan : MonoBehaviour
{

    TimeHealthSystem timeHealthHandler;

    

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        TimeActions.OnSecondChange -= TimeTick;

    }


    public void Init(TimeHealthSystem _timeHealthHandler)
    {
        TimeActions.OnSecondChange += TimeTick;
        timeHealthHandler = _timeHealthHandler;


    }
    private void TimeTick()
    {
        timeHealthHandler.TakeDamage(1,null);


    }



}