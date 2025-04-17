using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelectionHandler
{
    private readonly Card card;
    private Color naColor = Color.red;
    private Color AvilibleColor = Color.green;

    public CardSelectionHandler(Card card)
    {
        this.card = card;
    }

    /// <summary>
    /// Determines if the card is currently clickable.
    /// </summary>
    public bool IsCardClickable()
    {
        bool isClickable = (card.cardState == CardStateEnum.availible || card.cardState == CardStateEnum.mousedOver) &&
                           card.mode == Card.CardMode.playable;

        return isClickable;
    }

    /// <summary>
    /// Locks the card for selection and updates its state.
    /// </summary>
    public void LockCardForSelection()
    {
        DisplayActions.OnRemoveMouseSprite?.Invoke();
        // Update mouse display for cards with a click effect
        if (card.cardBase is IHasClickEffect effect)
        {
            DisplayActions.OnRemoveMouseSprite?.Invoke();
            DisplayActions.OnSetMouseSprite?.Invoke(new OnDisplaySpriteArgs(effect.GetSprite(), effect.GetSpriteSize()));
        }

        // Highlight scene objects if the card can be played on them
        if (card.cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.damagable)
        {
            DisplayActions.OnHighligtSceneObject(true);
        }

        // Lock the card for selection
        card.isSelected = true;
        card.cardState = CardStateEnum.lockedForSelection;

        // Animate card to visually indicate selection
        card.cardAnimations.AnimateScale(card.cardAnimations.clickScale, card);

        
    }

    /// <summary>
    /// Highlights the playable area based on the card's playability.
    /// </summary>
    private void HighlightPlayableArea()
    {
        int sizeX = 1;
        int sizeY = 1;
        bool activateDisplay = true;

        if (card.cardBase is SoCardInstanciate soCardInstanciate)
        {
            sizeX = soCardInstanciate.sizeX;
            sizeY = soCardInstanciate.sizeY;
        }

        if (card.cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.instantClick)
            activateDisplay = false;

        Cell activeCell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();
        if (activeCell == null)
            return;

        Color displayColor = CellCardColoringUtil.Evaluate(activeCell, card, AvilibleColor, naColor);
        SetDisplayHandle(displayColor, activateDisplay, sizeX, sizeY);
    }
    private void SetDisplayHandle(Color displayColor, bool active, int sizeX, int sizeY)
    {
        //Debug.Log("SetDisplayHandle to active: " + active);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(
        active,displayColor,
        sizeX, sizeY));
    }

          /// <summary>
    /// Handles mouse click updates for card selection.
    /// </summary>
    public void HandleSelectionUpdate()
    {
        if (card.mode == Card.CardMode.playable)
            HighlightPlayableArea(); 
        if (Input.GetMouseButtonDown(0) && !MouseDisplayManager.instance.mouseOverCard)
        {
            ProcessSelectionClick();
        }
    }

    /// <summary>
    /// Processes the selection click logic.
    /// </summary>
    private void ProcessSelectionClick()
    {
        if (card.mode != Card.CardMode.playable)
            return;

        var clickType = WorldSpaceUtils.CheckClickableType();
        var cell = GridCellManager.instance.gridConstrution.GetCurrecntCellByMouse();

        if (CardUtils.IsCardPlayableOnCell(card, clickType, cell))
        {
            PlayCard();
        }
        else
        {
            ResetCardSelection();
        }
    }

    /// <summary>
    /// Resets the card selection and updates the state.
    /// </summary>
    public void ResetCardSelection()
    {
        // Remove mouse display
        DisplayActions.OnRemoveMouseSprite?.Invoke();

        // Reset card state
        card.cardState = CardStateEnum.availible;
        card.isSelected = false;

        // Reset scene and UI highlights
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false, naColor));

        // Reset card animation
        card.cardAnimations.ScaleResetAndRelease(card);
    }

    /// <summary>
    /// Handles the play logic for the selected card.
    /// </summary>
    private void PlayCard()
    {
        if (card.cardBase.Effect(WorldSpaceUtils.GetMouseWorldPosition(), out string result, card.cardCostModifier))
        {
            FinalizeSuccessfulPlay();
        }
        else
        {
            GlobalActions.ShowTooltip(new ToolTipArgs { time = 3f, Tooltip = result });
        }
    }

    /// <summary>
    /// Finalizes the play process for the card.
    /// </summary>
    private void FinalizeSuccessfulPlay()
    {
        card.cardState = CardStateEnum.inDiscardPile;
        card.isSelected = false;

        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false, naColor));
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnRemoveMouseSprite?.Invoke();

        Action onComplete = () =>
        {
            if (card.cardBase.cardPlayTypeEnum == CardPlayTypeEnum.exhuast)
            {
                CardsInPlayManager.instance.ExhausedCardInHand(card);
            }
            else
            {
                CardsInPlayManager.instance.DiscardCardInHand(card);
            }
        };

        if (card.cardBase.cardPlayTypeEnum == CardPlayTypeEnum.exhuast)
        {
            card.cardAnimations.ExhaustCardAnimation(onComplete);
        }
        else
        {
            card.cardAnimations.PlayCardAnimation(onComplete);
        }
    }


}
