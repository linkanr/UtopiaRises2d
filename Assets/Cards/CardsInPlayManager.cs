using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class CardsInPlayManager : MonoBehaviour
{
    public static CardsInPlayManager instance;

    public List<Card> InHandList = new List<Card>();
    public List<Card> InDrawPileList = new List<Card>();
    public List<Card> InDiscardPileList = new List<Card>();
    public List<Card> InExhausedPileList = new List<Card>();
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
        BattleSceneActions.OnDrawCard += DrawCards;
        BattleSceneActions.OnLiveStatsStarting += DiscardAllCards;
    }


    private void OnDisable()
    {

        BattleSceneActions.OnInitializeScene -= InitializeDrawPile;
        BattleSceneActions.OnDrawCard -= DrawCards;
        BattleSceneActions.OnLiveStatsStarting -= DiscardAllCards;
    }

    private void DiscardAllCards()
    {
        List<Card> tempList = new List<Card>();
        foreach (Card card in InHandList)
        {
            tempList.Add(card);
        }
        foreach (Card card in tempList)
        {
            DiscardCardInHand(card);
        }   
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
            Card newCard = CardFactory.Create(card,Card.CardMode.playable,GameSceneRef.instance.drawPile);
            InDrawPileList.Add(newCard);
        }
        ShuffleDrawPile();
    }

    private void DrawCards(int amount)
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
    public Card DrawCard()
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
        return currentCard;
    }
    private void AddToHandFromDraw(Card currentCard)
    {
        Debug.Log($"Adding card to hand: {currentCard.cardBase.title}");

        // Remove from draw pile and add to hand
        InDrawPileList.Remove(currentCard);
        InHandList.Add(currentCard);

        // Ensure cardAnimations is initialized
        if (currentCard.cardAnimations == null)
        {
            Debug.LogError($"CardAnimations is null for card: {currentCard.cardBase.title}");
            currentCard.Initialize(currentCard.cardBase, Card.CardMode.playable);
        }

        // Set initial scale to 0 and parent the card
       
        currentCard.transform.SetParent(GameSceneRef.instance.inHandPile, worldPositionStays: false);
        currentCard.cardAnimations.SetScale(0f);
        // Immediately update layout after re-parenting
        LayoutRebuilder.ForceRebuildLayoutImmediate(GameSceneRef.instance.inHandPile);
        Canvas.ForceUpdateCanvases();

        // Animate the card into view and set it to playable
        currentCard.cardState = CardStateEnum.availible; // Set to a clickable state
        currentCard.cardAnimations.AnimateScale(1f, currentCard,.3f);
    }




    // Helper to ensure layout is updated at the next frame



    private void AddDiscardToDrawPile()
    {
        foreach (Card card in InDiscardPileList)
        {
            InDrawPileList.Add(card);

            card.cardState = CardStateEnum.inDrawPile;
            card.cardAnimations.SetScale(0f);
            card.transform.SetParent(GameSceneRef.instance.drawPile);
            //Debug.Log("adding card to draw pile");
        }
        InDiscardPileList.Clear();
        //Debug.Log("discard added to drawpile");

    }
    public void DiscardCardInHand(Card card)
    {
        //Debug.Log("moving to discard");
        if (card.cardCostModifier != null)
        {
            if (!card.cardCostModifier.permanent)
            {
                card.cardCostModifier = null;
            }
        }

        InHandList.Remove(card);
        InDiscardPileList.Add(card);
        card.cardState = CardStateEnum.inDiscardPile;
        card.transform.SetParent(GameSceneRef.instance.discardPile);
    }
    public void ExhausedCardInHand(Card card)
    {
        if (card.cardCostModifier != null)
        {
            card.cardCostModifier = null;
        }
            //Debug.Log("moving to discard");
        InHandList.Remove(card);
        InExhausedPileList.Add(card);
        card.cardState = CardStateEnum.exhausted;
        card.transform.SetParent(GameSceneRef.instance.exhusedPile);
    }
    private void ShuffleDrawPile()
    {
        GeneralUtils.ShuffleList(InDrawPileList);
    }
    private void OnDestroy()
    {
        Debug.Log("destroying card in play manager");
    }
}