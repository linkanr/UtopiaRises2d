using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OnClickObject : MonoBehaviour
{
    public Transform CircleReach;
    public RectTransform leftPanel;
    public RectTransform rightPanel;
    public TextMeshProUGUI simpleText;
    private Transform tracingObject;
    public static OnClickObject Create(Stats clickInfo)
    {
        OnClickObject onClickObject = Instantiate (Resources.Load("ClickRefObject")).GetComponent<OnClickObject>();
        onClickObject.Init(clickInfo);
        return onClickObject;
    }

    private void Init(Stats clickInfo)
    {
        tracingObject = clickInfo.GetTransform(StatsInfoTypeEnum.objectToFollow);
        foreach (KeyValuePair<StatsInfoTypeEnum, object> kvp in clickInfo.statsInfoDic)
        {
            switch (kvp.Key)
            {
                case StatsInfoTypeEnum.SoEnemyAttackSystem:
                    HandleAttackSystem(kvp);

                    break;


                case StatsInfoTypeEnum.description:
                case StatsInfoTypeEnum.name:
                case StatsInfoTypeEnum.faction:
                case StatsInfoTypeEnum.health:


                    GenerateStrings(kvp);
                    break;
            }




        }
    }

    private void HandleAttackSystem(KeyValuePair<StatsInfoTypeEnum, object> kvp)
    {
        SoAttackSystem attackSystem = kvp.Value as SoAttackSystem;
        float size = attackSystem.maxRange * 2f;
        CircleReach.localScale = new Vector3(size, size, size);
        TextMeshProUGUI damageText = Instantiate(simpleText, leftPanel);
        TextMeshProUGUI damagenumber = Instantiate(simpleText, rightPanel);
        TextMeshProUGUI timeAttacktext = Instantiate(simpleText, leftPanel);
        TextMeshProUGUI timeAttacknumber = Instantiate(simpleText, rightPanel);
        damageText.text = "damage";
        timeAttacktext.text = "reload time";
        damagenumber.text = attackSystem.damage.ToString();
        timeAttacknumber.text = attackSystem.attackTimerMax.ToString();
    }

    private void GenerateStrings(KeyValuePair<StatsInfoTypeEnum, object> kvp)
    {
        TextMeshProUGUI leftText = Instantiate(simpleText, leftPanel);
        TextMeshProUGUI rightText = Instantiate(simpleText, rightPanel);
        
        leftText.text = kvp.Key.ToString();
        if (kvp.Key == StatsInfoTypeEnum.reloadTime)
        {
            leftText.text = "Shots/S";
            float shots = 1f/((float) kvp.Value);
            rightText.text = shots.ToString();
        }
        if (kvp.Value.GetType() == typeof(int))
        {
            rightText.text = kvp.Value.ToString();
        }
        if (kvp.Value.GetType() == typeof(string))
        {
            rightText.text = (string)kvp.Value;
        }
        if (kvp.Value.GetType() == typeof(TimeStruct))
        {
            TimeStruct text = (TimeStruct)kvp.Value;
            rightText.text = text.GetString(true);


        }
        if (kvp.Value.GetType() == typeof(Faction))
        {

            Faction factionText = (Faction)kvp.Value;
            rightText.text = factionText.factionEnum.ToString();
        }
       
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
        if (tracingObject != null) 
        {
            CircleReach.transform.position = tracingObject.position;
        }
    }
}
