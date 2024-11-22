using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/CardList")]
public class SoCardList : ScriptableObject, IEnumerable<SoCardBase>
{
    public List<CardNames> list;

    // Enumerator to yield SoCardBase objects instead of CardNames
    public  IEnumerator<SoCardBase> GetEnumerator()
    {
        SoCardGlobalDic soCardGlobalDic = Resources.Load("cardNames") as SoCardGlobalDic;

        foreach (var cardName in list)
        {
            yield return getBase(cardName, soCardGlobalDic); // Convert CardNames to SoCardBase
        }
    }


    // Non-generic enumerator implementation for compatibility
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Static method for retrieving SoCardBase from a CardNames
    public static SoCardBase getBase(CardNames cardName, SoCardGlobalDic soCardGlobalDic)
    {
        
        return soCardGlobalDic.CardEnumsToCards[cardName];
    }
}