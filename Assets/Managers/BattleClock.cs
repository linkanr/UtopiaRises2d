using System;
using UnityEngine;

public class BattleClock: MonoBehaviour
{
    private float monthValue = 30f;
    private float quaterValue = 90f;
    private float Value = 0;

    public float timeValue { get { return Value; } }
    public int intValue { get { return (int) Value; } }
    public bool paused { get; private set; }
    private float timerPingMax = .1f;
    private float timerPing;

    private void OnEnable()
    {
        BattleSceneActions.OnPause += HandlePauseTrigger;
    }

    private void HandlePauseTrigger(bool obj)
    {
        if (obj) 
        {
            Pause();
        }
        if (!obj)
        {
            UnPause();
        }
    }

    public void Pause()
    {
        paused = true;
    }
    public void UnPause()
    {
        paused=false;
    }
    public void SetStartValue(float _value)
    {
        Value = _value;
    }


    private void Update()
    {
        if (!paused)
        {
            Value += Time.deltaTime;
            timerPing += Time.deltaTime;
            if (timerPing >= timerPingMax)
            {
                //Debug.Log("time ping");
                BattleSceneActions.GlobalTimeChanged?.Invoke(new BattleSceneTimeArgs { time = timeValue ,deltaTime= timerPingMax });
                timerPing = 0f;

            }
        }
    }
    
}