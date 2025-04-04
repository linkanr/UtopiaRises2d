using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<SoCardBase> ownedCards;
    public bool intialized = false;    
    public static CardManager instance;
    private SoAllCardsGlobalDic allCards;
    public SoCardList startingCards;

    private void OnEnable()
    {
        GlobalActions.OnNewCardAddedToDeck += AddCard;
    }

    private void OnDisable()
    {
        GlobalActions.OnNewCardAddedToDeck -= AddCard;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.LogError("Duplicate CardManager instance detected!");
        }

        allCards = Resources.Load<SoAllCardsGlobalDic>("cardNames");
    }
    public void AddStartingCardsToDeck()
    {
        GetStartingCards();
        intialized = true;
    }

    public SoCardBase GetRandomCard()
    {
        int randomIndex = Random.Range(0, allCards.length);
        return allCards.GetCard(randomIndex);
    }

    public SoCardBase GetRandomCard(CardRareEnums rarity)
    {
        List<SoCardBase> filteredCards = allCards.GetCardsByRarity(rarity);
        return filteredCards[Random.Range(0, filteredCards.Count)];
    }

    public SoCardBase GetRandomCard(SoCardList cardList)
    {
        int randomIndex = Random.Range(0, cardList.lengt);
        return allCards.GetCard(cardList.list[randomIndex]);
    }

    public void AddCard(SoCardBase cardBase)
    {

        SoCardBase newCard = Instantiate(cardBase);
        ownedCards.Add(newCard);
    }

    public void AddCard(SoCardList cardList)
    {
        foreach (SoCardBase cardBase in cardList)
        {
            Debug.Log("Adding card to deck: " + cardBase.title);
            AddCard(cardBase);
        }
    }

    private void GetStartingCards()
    {
        Debug.Log("Adding starting cards");
        AddCard(startingCards);
    }
    public List<FactionsEnums> GetContainingFactions(int minAmount)
    {
        List<FactionsEnums > result = new List<FactionsEnums>();
        Dictionary<FactionsEnums, int> factionDic = new Dictionary<FactionsEnums, int>();
        foreach (SoCardBase card in ownedCards)
        {
            FactionsEnums faction = card.faction.factionEnum;
            if (!factionDic.ContainsKey(faction))
            {
                factionDic[faction] = 1;
            }
            else
            {
                factionDic[faction]++;
            }

        }
        foreach (var faction in factionDic)
        {
            if (faction.Value >= minAmount)
            {
                result.Add(faction.Key);
            }
        }
        return result;
    }
}
