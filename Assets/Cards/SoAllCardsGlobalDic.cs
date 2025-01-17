using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/CardNames")]
public class SoAllCardsGlobalDic : SerializedScriptableObject, IEnumerable<SoCardBase>
{
    public Dictionary<CardNames, SoCardBase> cardEnumsToCards;
    public int length => cardEnumsToCards.Count;

    public SoCardBase GetCard(int index)
    {
        return cardEnumsToCards.ElementAt(index).Value;
    }
    public SoCardBase GetCard(CardNames cardName)
    {
        return cardEnumsToCards[cardName];
    }
    public IEnumerator<SoCardBase> GetEnumerator()
    {
        foreach (KeyValuePair<CardNames,SoCardBase> keyValuePair  in cardEnumsToCards)
        {
            yield return keyValuePair.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}




