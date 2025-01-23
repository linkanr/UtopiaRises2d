using UnityEngine;

public static class CardFactory
{
    public static Card Create(SoCardBase cardData, Card.CardMode mode, Transform parent)
    {
        GameObject cardObject = GameObject.Instantiate(Resources.Load<GameObject>("Card"), parent);
        Card card = cardObject.GetComponent<Card>();
        card.Initialize(cardData, mode);
        return card;
    }
}
