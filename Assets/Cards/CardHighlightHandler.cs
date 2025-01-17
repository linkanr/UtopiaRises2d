public class CardHighlightHandler
{
    private readonly Card card;

    public CardHighlightHandler(Card card)
    {
        this.card = card;
    }

    public void HighlightCard()
    {
        DisplayActions.OnMouseOverCard();

        if (card.cardState != CardStateEnum.lockedForSelection && !card.isSelected)
        {
            card.cardState = CardStateEnum.mousedOver;
            card.cardAnimations.AnimateScale(card.cardAnimations.mouseOverScale, card);
        }
    }

    public void ResetCardHighlight()
    {
        DisplayActions.OnMouseNotOverCard();

        if (card.cardState != CardStateEnum.lockedForSelection && !card.isSelected)
        {
            card.cardState = CardStateEnum.availible;
            card.cardAnimations.ScaleResetAndRelease(card);
        }
    }
}
