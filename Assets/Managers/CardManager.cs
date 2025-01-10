using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<SoCardBase> ownedCards;
    
    public static CardManager Instance;
    SoAllCardsGlobalDic allCards;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("double trouble");
        }
        allCards = Resources.Load("cardNames") as SoAllCardsGlobalDic;



    }

    public SoCardBase GetRandomCard()
    {
        int random = UnityEngine.Random.Range(0, allCards.length - 1);
        return allCards.GetCard(random);
    }
    public SoCardBase GetRandomCard(CardRareEnums rarity)
    {
        List<SoCardBase> returnList = new List<SoCardBase>();
        foreach (SoCardBase soCardBase in allCards)
        {
            if (soCardBase.rarity == rarity)
            {
                returnList.Add(soCardBase);
            }
        }
        int l = returnList.Count;
        int rand = UnityEngine.Random.Range(0,l-1);
        return returnList[rand];

        
    }
    public SoCardBase GetRandomCard(SoCardList cardlist)
    {
        int random = UnityEngine.Random.Range(0, cardlist.lengt -1);
        return  allCards.GetCard(cardlist.list[random]);
    }

    public void AddCard(SoCardBase soCardBase) // regular bases
    {
        SoCardBase newCard =  Instantiate(soCardBase);
        
        ownedCards.Add(newCard);
    }

    public void AddCard(SoCardList soCardList) // uses the custom list class
    {
        foreach (SoCardBase soCardBase in soCardList)
        {
            SoCardBase newCard = Instantiate(soCardBase);
            ownedCards.Add(newCard);
        }
    }

    internal void GetStartingCards()
    {
        AddCard(Resources.Load("startingCards") as SoCardList);
    }

}
