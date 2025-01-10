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
    private float timerPing;
    public float interwall;
    public float interwallTimer = 0f;
    public float deltaValue;

    private void OnEnable()
    {
        BattleSceneActions.OnPause += HandlePauseTrigger;
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
                //Debug.Log("time ping");
                BattleSceneActions.GlobalTimeChanged?.Invoke(new BattleSceneTimeArgs { time = timeValue ,deltaTime= timerPingMax });
                timerPing = 0f;
                interwallTimer += timerPingMax;

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