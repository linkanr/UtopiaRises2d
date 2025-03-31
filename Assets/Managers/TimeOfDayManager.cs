using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayManager : MonoBehaviour
{
    public Light sun;
    public float currentAngle;
    public float startanlge;
    public float endangle;
    public float speed;
    public float intensity;
    public Color startColor;
    public Color currentColor;

    public Color midColor;
    private void Start()
    {
        currentAngle = startanlge;
    }
    private void OnEnable()
    {
        TimeActions.GlobalTimeChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeActions.GlobalTimeChanged -= UpdateTime;
    }

    private void UpdateTime(BattleSceneTimeArgs args)
    {
        currentAngle += speed * Time.deltaTime;
        float colorMix = Mathf.Abs(currentAngle) / 90;
        colorMix = Mathf.Pow(colorMix,3);

        currentColor =  Color.Lerp(midColor, startColor, colorMix);
        intensity =  MathF.Pow(((1-(Mathf.Abs(currentAngle) / 90))+.3f),1.3f);
        sun.color = currentColor;
        sun.intensity = intensity;
        sun.transform.rotation = Quaternion.Euler(currentAngle, 0, 0);

    }
}
