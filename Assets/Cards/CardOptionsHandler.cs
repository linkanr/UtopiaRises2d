using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOptionsHandler : MonoBehaviour
{

    const float costRare = 120f;
    const float costUncommon = 10f;

    public static List<CardRareEnums> GetRareEnums(int luck, int cardAmount)
    {
        List<CardRareEnums> returnList = new List<CardRareEnums>();
        for (int i=0; i<cardAmount; i++)
        {
            float roll = Random.Range(0, 350) + luck;
            float rareChance = roll / costRare;
            float uncommonChance = roll / costUncommon;
            Debug.Log("uncommon chance is " + uncommonChance.ToString() + " rare chance " + rareChance.ToString());
            float secondRoll = Random.Range(0, 100);
            Debug.Log("second roll is "+secondRoll.ToString());
            if (secondRoll < rareChance)
            {
                returnList.Add(CardRareEnums.rare);
                continue;

            }
            if (secondRoll < uncommonChance) 
            {
                returnList.Add(CardRareEnums.uncommon);
                continue;
            }
            else
            {
                returnList.Add(CardRareEnums.common);
            }
        }
        return returnList;

    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            foreach (CardRareEnums rareEnums in GetRareEnums(0, 3))
            {
                Debug.Log(rareEnums.ToString());
                SoCardBase soCardBase = CardManager.Instance.GetRandomCard(rareEnums);
                Debug.Log(soCardBase.title);
                
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (CardRareEnums rareEnums in GetRareEnums(150, 3))
            {
                Debug.Log(rareEnums.ToString());
                SoCardBase soCardBase = CardManager.Instance.GetRandomCard(rareEnums);
                Debug.Log(soCardBase.title);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (CardRareEnums rareEnums in GetRareEnums(500, 3))
            {
                Debug.Log(rareEnums.ToString());
                SoCardBase soCardBase = CardManager.Instance.GetRandomCard(rareEnums);
                Debug.Log(soCardBase.title);
            }
        }



    }
    */


}
