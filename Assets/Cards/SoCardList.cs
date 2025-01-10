using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/CardList")]
public class SoCardList : ScriptableObject, IEnumerable<SoCardBase>
{
    public List<CardNames> list;
    public int lengt=> list.Count;


    // Enumerator to yield SoCardBase objects instead of CardNames
    public  IEnumerator<SoCardBase> GetEnumerator()
    {
        SoAllCardsGlobalDic soCardGlobalDic = Resources.Load("cardNames") as SoAllCardsGlobalDic;

        foreach (var cardName in list)
        {
            yield return soCardGlobalDic.GetCard(cardName);
        }
    }


    // Non-generic enumerator implementation for compatibility
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Static method for retrieving SoCardBase from a CardNames

}