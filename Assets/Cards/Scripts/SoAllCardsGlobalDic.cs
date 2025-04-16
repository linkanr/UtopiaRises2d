// SoAllCardsGlobalDic.cs
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/CardDatabase")]
public class SoAllCardsGlobalDic : SerializedScriptableObject, IEnumerable<SoCardBase>
{
    [ReadOnly]
    public Dictionary<CardNames, SoCardBase> cardEnumsToCards = new();

    public int length => cardEnumsToCards.Count;

    public SoCardBase GetCard(int index)
    {
        if (index < 0 || index >= cardEnumsToCards.Count)
        {
            Debug.LogError($"Index {index} is out of range for cardEnumsToCards.");
            return null;
        }
        return cardEnumsToCards.ElementAt(index).Value;
    }

    public SoCardBase GetCard(CardNames cardName)
    {
        if (cardEnumsToCards.TryGetValue(cardName, out SoCardBase card))
        {
            return card;
        }
        Debug.LogError($"Card with name {cardName} not found in cardEnumsToCards.");
        return null;
    }

    public List<SoCardBase> GetCardsByRarity(CardRareEnums rarity)
    {
        return cardEnumsToCards.Values.Where(card => card.rarity == rarity).ToList();
    }

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

#if UNITY_EDITOR
    public void ClearAndAddAllCards(List<SoCardBase> allCards)
    {
        cardEnumsToCards.Clear();

        foreach (var card in allCards)
        {
            string enumName = new string(card.name.Where(char.IsLetterOrDigit).ToArray());
            if (string.IsNullOrWhiteSpace(enumName))
            {
                Debug.LogWarning($"Skipping unnamed or invalid card asset: {card.name}");
                continue;
            }
            if (char.IsDigit(enumName.First()))
                enumName = "_" + enumName;

            if (!System.Enum.TryParse(enumName, ignoreCase: true, out CardNames parsedEnum))
            {
                Debug.LogWarning($"Card '{card.name}' could not be matched to enum '{enumName}'");
                continue;
            }

            if (!cardEnumsToCards.ContainsKey(parsedEnum))
            {
                cardEnumsToCards.Add(parsedEnum, card);
            }
            else
            {
                Debug.LogWarning($"Duplicate card enum '{parsedEnum}' – skipping.");
            }
        }

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif
}