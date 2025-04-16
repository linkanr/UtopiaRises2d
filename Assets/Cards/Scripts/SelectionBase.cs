using UnityEngine;

public class SelectionBase
{
    public Card card { get; private set; }

    public SelectionBase(Card card)
    {
        this.card = card;
    }

    public SoCardBase GetCardBase()
    {
        return card.cardBase;
    }

    public void Select()
    {
        card.Select();
    }
}
