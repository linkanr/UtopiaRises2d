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
    public TextMeshProUGUI timerYear;
    public TextMeshProUGUI timerSeason;
    public TextMeshProUGUI timerDay;


    private void OnEnable()
    {
        TimeActions.GlobalTimeChanged += UpdateTimers;
        TimeActions.OnSecondChange += ScaleTimer;
    }


    private void OnDisable()
    {
        TimeActions.GlobalTimeChanged -= UpdateTimers;
        TimeActions.OnSecondChange -= ScaleTimer;
    }

    private void UpdateTimers(BattleSceneTimeArgs args)
    {
        List<string> list = TimeCalc.TimeToString(TimeCalc.TimeToTimeStruct(args.time));
        timerYear.text = list[0];
        timerSeason.text = list[1];
        timerDay.text = list[2];
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
