using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOptionsHandler : MonoBehaviour
{

    float costRare = 120f;
    float costUncommon = 15f;
    public List<CardNames> cardNamesList = new List<CardNames>();
    SoCardGlobalDic SoCardGlobalDic;
    private void Awake()
    {
        SoCardGlobalDic = Resources.Load("CardNames") as SoCardGlobalDic;
        foreach(KeyValuePair<CardNames, SoCardBase> keyValuePair in SoCardGlobalDic.CardEnumsToCards)
        {
           cardNamesList.Add(keyValuePair.Key);
        }
    }
    private List<RareEnums> GetRareEnums(int luck, int cardAmount)
    {
        List<RareEnums> returnList = new List<RareEnums>();
        for (int i=0; i<cardAmount; i++)
        {
            float roll = Random.Range(0, 250) + luck;
            float rareChance = roll / costRare;
            float uncommonChance = roll / costUncommon;
            Debug.Log("uncommon chance is " + uncommonChance.ToString() + " rare chance " + rareChance.ToString());
            float secondRoll = Random.Range(0, 100);
            Debug.Log("second roll is "+secondRoll.ToString());
            if (secondRoll < rareChance)
            {
                returnList.Add(RareEnums.rare);
                continue;

            }
            if (secondRoll < uncommonChance) 
            {
                returnList.Add(RareEnums.uncommon);
                continue;
            }
            else
            {
                returnList.Add(RareEnums.common);
            }
        }
        return returnList;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            foreach (RareEnums rareEnums in GetRareEnums(0, 3))
            {
                Debug.Log(rareEnums.ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (RareEnums rareEnums in GetRareEnums(150, 3))
            {
                Debug.Log(rareEnums.ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (RareEnums rareEnums in GetRareEnums(500, 3))
            {
                Debug.Log(rareEnums.ToString());
            }
        }



    }



}
