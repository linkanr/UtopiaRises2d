using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<SoCardBase> ownedCards;

    public static CardManager Instance;
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
        if (Instance == null)
        {
            Instance = this;
            
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
            AddCard(cardBase);
        }
    }

    private void GetStartingCards()
    {
        AddCard(startingCards);
    }
}
