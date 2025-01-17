using UnityEngine;

public class CardSelectionHandler
{
    private readonly Card card;

    public CardSelectionHandler(Card card)
    {
        this.card = card;
    }

    public bool IsCardClickable()
    {
        Debug.Log("IsCardClickable");
        return card.cardState != CardStateEnum.lockedForSelection &&
               card.cardState != CardStateEnum.resetting &&
               card.cardState != CardStateEnum.inDisplayMenu;
    }

    public void LockCardForSelection()
    {
        if (card.cardBase is IHasClickEffect effect)
        {
            MouseDisplayManager.OnRemoveDisplay();
            MouseDisplayManager.OnSetNewSprite(new OnSetSpriteArgs { sprite = effect.GetSprite(), size = effect.GetSpriteSize() });
        }

        if (card.cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.damagable)
        {
            DisplayActions.OnHighligtSceneObject(true);
        }

        card.isSelected = true;
        card.cardState = CardStateEnum.lockedForSelection;

        card.cardAnimations.AnimateScale(card.cardAnimations.clickScale, card);
        HighlightPlayableArea();
    }

    private void HighlightPlayableArea()
    {
        int sizeX = 1;
        int sizeY = 1;

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

    public void HandleSelectionUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down");
            ProcessSelectionClick();
        }
    }

    private void ProcessSelectionClick()
    {
        ClickableType clickableType = WorldSpaceUtils.CheckClickableType();

        if (MouseDisplayManager.instance.mouseOverCard)
        {
            ResetCardSelection();
            return;
        }

        switch (card.cardBase.cardCanBePlayedOnEnum)
        {
            case CardCanBePlayedOnEnum.instantClick:
                if (clickableType != ClickableType.card)
                {
                    Debug.Log("instant click");
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
                    Debug.Log("empty ground");
                    PlayCard();
                }
                else
                {
                    ResetCardSelection();
                }
                break;
        }
    }

    public void ResetCardSelection()
    {
        MouseDisplayManager.OnRemoveDisplay();
        card.cardState = CardStateEnum.availible;
        card.isSelected = false;
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false));
        card.cardAnimations.ScaleResetAndRelease(card);
    }

    private void PlayCard()
    {
        Debug.Log("play card");
        if (card.cardBase.Effect(WorldSpaceUtils.GetMouseWorldPosition(), out string result, card.cardCostModifier))
        {
            Debug.Log("play card success");
            FinalizeSuccessfulPlay();
        }
        else
        {
            GlobalActions.Tooltip(new ToolTipArgs { time = 3f, Tooltip = result });
        }
    }

    private void FinalizeSuccessfulPlay()
    {
        Debug.Log("finalize play");
        card.cardCostModifier = null;
        card.cardState = CardStateEnum.availible;
        card.isSelected = false;
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false));
        DisplayActions.OnHighligtSceneObject(false);
        MouseDisplayManager.OnRemoveDisplay();

        if (card.cardBase.cardPlayTypeEnum == CardPlayTypeEnum.exhuast)
        {
            CardsInPlayManager.instance.ExhausedCardInHand(card);
        }
        else
        {
            CardsInPlayManager.instance.DiscardCardInHand(card);
            card.cardState = CardStateEnum.inDiscardPile;
        }
    }
}
