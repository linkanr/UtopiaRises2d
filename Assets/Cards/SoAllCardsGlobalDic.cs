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

    /// <summary>
    /// Get a card by its index in the dictionary.
    /// </summary>
    public SoCardBase GetCard(int index)
    {
        if (index < 0 || index >= cardEnumsToCards.Count)
        {
            Debug.LogError($"Index {index} is out of range for cardEnumsToCards.");
            return null;
        }
        return cardEnumsToCards.ElementAt(index).Value;
    }

    /// <summary>
    /// Get a card by its CardNames key.
    /// </summary>
    public SoCardBase GetCard(CardNames cardName)
    {
        if (cardEnumsToCards.TryGetValue(cardName, out SoCardBase card))
        {
            return card;
        }
        Debug.LogError($"Card with name {cardName} not found in cardEnumsToCards.");
        return null;
    }

    /// <summary>
    /// Get all cards with the specified rarity.
    /// </summary>
    public List<SoCardBase> GetCardsByRarity(CardRareEnums rarity)
    {
        return cardEnumsToCards.Values.Where(card => card.rarity == rarity).ToList();
    }

    /// <summary>
    /// Get an enumerator for iterating over the cards.
    /// </summary>
    public IEnumerator<SoCardBase> GetEnumerator()
    {
        foreach (var keyValuePair in cardEnumsToCards)
        {
            yield return keyValuePair.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
