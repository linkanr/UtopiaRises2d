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
     //   Debug.Log("subbing");
        BattleSceneActions.OnInitializeScene += InitializeDrawPile;
        BattleSceneActions.OnDrawCard += DrawCards;
        BattleSceneActions.OnSpawningStarting += DiscardAllCards;
    }


    private void OnDisable()
    {

        BattleSceneActions.OnInitializeScene -= InitializeDrawPile;
        BattleSceneActions.OnDrawCard -= DrawCards;
        BattleSceneActions.OnSpawningStarting -= DiscardAllCards;
    }

    private void DiscardAllCards()
    {
        StartCoroutine(DiscardCardsOneByOne());
    }

    private IEnumerator DiscardCardsOneByOne()
    {
        List<Card> cardsToDiscard = new List<Card>(InHandList);
        cardsToDiscard.Reverse(); // Reverse the list to discard from the last card to the first
        foreach (Card card in cardsToDiscard)
        {
            // Lock state so player can’t click it
            card.cardState = CardStateEnum.lockedForAnimation;

            // Delay all discard logic until animation finishes
            card.cardAnimations.DiscardAnimation(() =>
            {
                InHandList.Remove(card);
                InDiscardPileList.Add(card);
                card.cardState = CardStateEnum.inDiscardPile;

                // Only reparent *after* animation completes
                card.transform.SetParent(GameSceneRef.instance.discardPile);
                card.transform.localRotation = Quaternion.identity;
            });

            yield return new WaitForSeconds(0.2f);
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
       // Debug.Log("initializing draw pile");
        foreach (SoCardBase card in CardManager.instance.ownedCards)
        {
            Card newCard = CardFactory.Create(card,Card.CardMode.playable,GameSceneRef.instance.drawPile);
            InDrawPileList.Add(newCard);
        }
        ShuffleDrawPile();
    }

public void DrawCards(int amount)
{
    StartCoroutine(DrawCardsRoutine(amount));
}

private IEnumerator DrawCardsRoutine(int amount)
{
    for (int i = 0; i < amount; i++)
    {
        if (InDrawPileList.Count == 0)
        {
            AddDiscardToDrawPile();
            ShuffleDrawPile();
        }

        Card currentCard = InDrawPileList[0];
        AddToHandFromDraw(currentCard);

        yield return new WaitForSeconds(0.15f); // tweak delay to taste
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
        InDrawPileList.Remove(currentCard);
        InHandList.Add(currentCard);

        if (currentCard.cardAnimations == null)
        {
            Debug.LogError($"CardAnimations is null for card: {currentCard.cardBase.title}");
            currentCard.Initialize(currentCard.cardBase, Card.CardMode.playable);
        }

        currentCard.transform.SetParent(GameSceneRef.instance.inHandPile, worldPositionStays: false);
        currentCard.cardAnimations.SetScale(0f);

        CanvasGroup cg = currentCard.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1f;

        LayoutRebuilder.ForceRebuildLayoutImmediate(GameSceneRef.instance.inHandPile);
        Canvas.ForceUpdateCanvases();

        currentCard.cardState = CardStateEnum.availible;
        currentCard.cardAnimations.AnimateScale(1f, currentCard, 0.3f);
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
        Debug.Log("moving to discard");
        if (card.cardCostModifier != null)
        {
            Debug.Log("removing cost modifier");
            if (!card.cardCostModifier.permanent)
            {
                Debug.Log("not permanent");
                card.cardCostModifier.Remove();
                card.cardCostModifier = null;
            }
            else
            {
                Debug.Log("permanent");
            }
        }
        else
        {
            Debug.Log("no cost modifier");
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
   //     Debug.Log("destroying card in play manager");
    }
}