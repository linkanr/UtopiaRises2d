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
        OnClickObject onClickObject = Instantiate(Resources.Load("ClickRefObject")).GetComponent<OnClickObject>();
        onClickObject.Init(clickInfo);
        return onClickObject;
    }

    private void Init(Stats clickInfo)
    {
        tracingObject = clickInfo.sceneObjectTransform;
        foreach (KeyValuePair<StatsInfoTypeEnum, object> kvp in clickInfo.statsInfoDic)
        {
            switch (kvp.Key)
            {
                case StatsInfoTypeEnum.onClickDisplaySprite:
                    float size = clickInfo.maxShootingDistance * 2f;
                    CircleReach.localScale = new Vector3(size, size, size);
                    break;
                case StatsInfoTypeEnum.damager:
                    AddDamagerStats(clickInfo);
                    break;
                case StatsInfoTypeEnum.description:
                case StatsInfoTypeEnum.name:
                case StatsInfoTypeEnum.Faction:
                case StatsInfoTypeEnum.health:
                case StatsInfoTypeEnum.speed:
                    GenerateStrings(kvp);
                    break;
            }
        }
    }

    private void AddDamagerStats(Stats clickInfo)
    {
        TextMeshProUGUI leftText = Instantiate(simpleText, leftPanel);
        TextMeshProUGUI rightText = Instantiate(simpleText, rightPanel);

        leftText.text = "Damage";
        rightText.text = clickInfo.damageAmount.ToString();

        leftText = Instantiate(simpleText, leftPanel);
        rightText = Instantiate(simpleText, rightPanel);

        leftText.text = "Reload Time";
        rightText.text = clickInfo.reloadTime.ToString();

        leftText = Instantiate(simpleText, leftPanel);
        rightText = Instantiate(simpleText, rightPanel);

        leftText.text = "Attack Range";
        rightText.text = clickInfo.maxShootingDistance.ToString();
    }

    private void GenerateStrings(KeyValuePair<StatsInfoTypeEnum, object> kvp)
    {
        TextMeshProUGUI leftText = Instantiate(simpleText, leftPanel);
        TextMeshProUGUI rightText = Instantiate(simpleText, rightPanel);

        leftText.text = kvp.Key.ToString();

        if (kvp.Value.GetType() == typeof(int))
        {
            rightText.text = kvp.Value.ToString();
        }
        if (kvp.Value.GetType() == typeof(float))
        {
            float value = (float)kvp.Value;
            int ivalue = (int)value;
            rightText.text = ivalue.ToString();
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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
        if (tracingObject != null)
        {
            CircleReach.transform.position = tracingObject.position;
        }
    }
}
