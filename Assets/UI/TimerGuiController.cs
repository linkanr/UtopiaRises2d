using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;
using System;
using TMPro;
using DG;
using DG.Tweening;


public class TimerGuiController : MonoBehaviour
{
    public RectTransform timerRect;
    public TextMeshProUGUI timeTextNextWaveTime;
    public TextMeshProUGUI timerYear;
    public TextMeshProUGUI timerSeason;
    public TextMeshProUGUI timerDay;


    private void OnEnable()
    {


        BattleSceneManager.instance.OnTimeCountDown += UpdateTimers;
    }


    private void OnDisable()
    {

        BattleSceneManager.instance.OnTimeCountDown -= UpdateTimers;
    }

    private void UpdateTimers()
    {
        timeTextNextWaveTime.text = BattleSceneManager.instance.GetTimeLeft().ToString();
    }
    private void ScaleTimer()
    {
        timerRect.DOPunchScale(new Vector3 (.1f,.1f,.1f),.2f).SetEase(Ease.OutSine);
    }


    // Start is called before the first frame update
    void Start()
    {



    }


}
