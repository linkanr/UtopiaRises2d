using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class CardsInPlayManager : MonoBehaviour
{
    public static CardsInPlayManager instance;
    public List<Card> InHandList = new List<Card>();
    public List<Card> InDrawPileList = new List<Card>();
    public List<Card> InDiscardPileList = new List<Card>();

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("");
        }
    }
    private void OnEnable()
    {
        Debug.Log("subbing");
        BattleSceneActions.OnInitializeScene += InitializeDrawPile;
        BattleSceneActions.OnDrawCard += DrawCard;
    }
    private void OnDisable()
    {
      
        BattleSceneActions.OnInitializeScene -= InitializeDrawPile;
        BattleSceneActions.OnDrawCard -= DrawCard;
    }

    public bool CheckForLockedCard()
    {
        foreach (Card card in InHandList)
        {
            if (card.cardState == CardStateEnum.lockedForSelection)
            {
                return true;
            }

        }
        return false;
    }



    private void InitializeDrawPile()
    {
        foreach (SoCardBase card in CardManager.Instance.ownedCards)
        {
            Card newCard =  Card.Create(card);
            InDrawPileList.Add(newCard);
        }
        ShuffleDrawPile();
    }

    private void DrawCard(int amount)
    {
        //Debug.Log("drawing " + amount + " cards");
        for (int i = 0; i < amount; i++)
        {
            if (InDrawPileList.Count == 0)
            {
                Debug.Log("reshuffle");
                AddDiscardToDrawPile();
                ShuffleDrawPile();
                //Debug.Log("amount after reshuffle " + InDrawPileList.Count);
                

            }

            Card currentCard = InDrawPileList[0];

            AddToHandFromDraw(currentCard);

        }
    }

    private void AddToHandFromDraw(Card currentCard)
    {
        InDrawPileList.Remove(currentCard);
        InHandList.Add(currentCard);
        currentCard.AddToHand();
    }

    private void AddDiscardToDrawPile()
    {
        foreach (Card card in InDiscardPileList)
        {
            InDrawPileList.Add(card);
            
            card.AddedToDrawPile();
            //Debug.Log("adding card to draw pile");
        }
        InDiscardPileList.Clear();
        //Debug.Log("discard added to drawpile");
       
    }
    public void DiscardCardInHand(Card card)
    {
        //Debug.Log("moving to discard");
        InHandList.Remove(card);
        InDiscardPileList.Add(card);
        card.MoveLeftOversToDiscard();
    }
    private void ShuffleDrawPile()
    {
        GeneralUtils.ShuffleList(InDrawPileList);
    }
}
