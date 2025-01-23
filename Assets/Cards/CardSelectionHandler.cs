using UnityEngine;

public class CardSelectionHandler
{
    private readonly Card card;

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

        HighlightPlayableArea();
    }

    /// <summary>
    /// Highlights the playable area based on the card's playability.
    /// </summary>
    private void HighlightPlayableArea()
    {
        int sizeX = 1;
        int sizeY = 1;

        // Adjust the playable area for cards with instantiation size
        if (card.cardBase is SoCardInstanciate soCardInstanciate)
        {
            sizeX = soCardInstanciate.sizeX;
            sizeY = soCardInstanciate.sizeY;
        }

        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(
            card.cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.emptyGround,
            sizeX, sizeY
        ));
    }

    /// <summary>
    /// Handles mouse click updates for card selection.
    /// </summary>
    public void HandleSelectionUpdate()
    {
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
        ClickableType clickableType = WorldSpaceUtils.CheckClickableType();

        switch (card.cardBase.cardCanBePlayedOnEnum)
        {
            case CardCanBePlayedOnEnum.instantClick:
                if (clickableType != ClickableType.card)
                {
                    PlayCard();
                }
                else
                {
                    ResetCardSelection();
                }
                break;

            case CardCanBePlayedOnEnum.damagable:
                if (clickableType == ClickableType.SceneObject)
                {
                    PlayCard();
                }
                else
                {
                    ResetCardSelection();
                }
                break;

            case CardCanBePlayedOnEnum.emptyGround:
                if (clickableType != ClickableType.card)
                {
                    PlayCard();
                }
                else
                {
                    ResetCardSelection();
                }
                break;

            default:
                Debug.LogError($"Unhandled CardCanBePlayedOnEnum value: {card.cardBase.cardCanBePlayedOnEnum}");
                ResetCardSelection();
                break;
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
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false));

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
            GlobalActions.Tooltip(new ToolTipArgs { time = 3f, Tooltip = result });
        }
    }

    /// <summary>
    /// Finalizes the play process for the card.
    /// </summary>
    private void FinalizeSuccessfulPlay()
    {
        // Clear cost modifier and reset card state
        card.cardCostModifier = null;
        card.cardState = CardStateEnum.inDiscardPile;
        card.isSelected = false;

        // Reset display actions
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false));
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnRemoveMouseSprite?.Invoke();

        // Move card to the appropriate pile
        if (card.cardBase.cardPlayTypeEnum == CardPlayTypeEnum.exhuast)
        {
            CardsInPlayManager.instance.ExhausedCardInHand(card);
        }
        else
        {
            CardsInPlayManager.instance.DiscardCardInHand(card);
        }
    }
}
