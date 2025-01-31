using System;

using UnityEngine;

public class BattleClock: MonoBehaviour
{
    public static BattleClock Instance;
    private float monthValue = 30f;
    private float quaterValue = 90f;
    private float Value = 0;
    public Action<bool> OnClockRunning;

    public float timeValue { get { return Value; } }
    public int intValue { get { return (int) Value; } }
    public bool paused { get; private set; }
    private float timerPingMax = .1f;
    private int timerPingSecond = 0;
    private float timerPing;
    private float interwall = 10f;

    public float interwallTimer = 0f;
    public float deltaValue;

    private void OnEnable()
    {
        TimeActions.OnPause += HandlePauseTrigger;
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Dounle clock");
        }
    }
    private void HandlePauseTrigger(bool obj)
    {
        if (obj) 
        {
            Pause();
            OnClockRunning?.Invoke(false);
        }
        if (!obj)
        {
            OnClockRunning?.Invoke(true);
            UnPause();
        }
    }

    public void Pause()
    {
     
        paused = true;
        deltaValue = 0f;
       
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
            deltaValue = Time.deltaTime;
            timerPing += Time.deltaTime;
            if (timerPing >= timerPingMax)
            {
                TimeActions.GlobalTimeChanged?.Invoke(new BattleSceneTimeArgs { time = timeValue ,deltaTime= timerPingMax });
                interwallTimer += timerPing;
                timerPing = 0f;
                timerPingSecond++;
                if (timerPingSecond >= 10)
                {
                    Debug.Log("trigger second change");
                    TimeActions.OnSecondChange?.Invoke();
                    timerPingSecond = 0;
                }



            }
            if (interwallTimer > interwall)
            {
                Debug.Log("trigger interwall Timer. Interwall timer is " + interwallTimer );

                BattleSceneActions.OnSpawnInterwallDone();
                interwallTimer = 0f;
            }
        }
        else
        {
            
        }
    }
    
}