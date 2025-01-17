using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IClickableObject
{
    public CardAnimations cardAnimations;
    public SoCardBase cardBase;
    public CardStateEnum cardState;
    public bool isSelected = false;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI cost;
    public Image factionColorImage;
    public Image backgroundImage;
    public CardCostModifier cardCostModifier;

    private CardSelectionHandler selectionHandler;
    private CardHighlightHandler highlightHandler;

    private void Awake()
    {
        InitializeCardAnimations();
        InitializeHandlers();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void MoveFromHandToDiscardList()
    {
        CardsInPlayManager.instance.DiscardCardInHand(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectionHandler.IsCardClickable())
        {
            selectionHandler.LockCardForSelection();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightHandler.HighlightCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightHandler.ResetCardHighlight();
    }

    private void Update()
    {
        if (cardState == CardStateEnum.lockedForSelection)
        {
            selectionHandler.HandleSelectionUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectionHandler.ResetCardSelection();
        }
    }

    private void InitializeCardAnimations()
    {
        cardAnimations = new CardAnimations(
            GetComponent<LayoutElement>(),

            GetComponent<RectTransform>(),
            Ease.InOutSine,
        GetComponentInParent<RectTransform>()

        ); 
    }

    private void InitializeHandlers()
    {
        selectionHandler = new CardSelectionHandler(this);
        highlightHandler = new CardHighlightHandler(this);
    }

    private void UnsubscribeEvents()
    {
        BattleSceneActions.OnStartSpawning -= MoveFromHandToDiscardList;
    }

    public ClickableType GetClickableType()
    {
        return ClickableType.card;
    }
}
