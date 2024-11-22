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
    }
    private void OnEnable()
    {
       
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
        foreach (SoCardBase card in CardManager.Instance.cardsList)
        {
            Card newCard =  Card.Create(card, GameSceneRef.instance.drawPile);
            InDrawPileList.Add(newCard);
        }
        ShuffleDrawPile();
    }

    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++) 
        {
            if (InDrawPileList.Count == 0)
            {
                AddDiscardToDrawPile();
                ShuffleDrawPile();

            }
            Card currentCard = InDrawPileList[0];
            currentCard.Dealt();
            InDrawPileList.Remove(currentCard);
            InHandList.Add(currentCard);
            
        }
    }
    private void AddDiscardToDrawPile()
    {
        foreach (Card card in InDiscardPileList)
        {
            InDrawPileList.Add(card);
            card.AddedToDrawPile();
        }
        InDiscardPileList.Clear();
        ShuffleDrawPile();
    }
    private void DiscardCard(Card card)
    {

        card.AddToDiscardPile();
    }
    private void ShuffleDrawPile()
    {
        GeneralUtils.ShuffleList(InDrawPileList);
    }
}
