using UnityEngine;

public static class CardFactory
{
    public static Card Create(SoCardBase soCardBase)
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("Card") as GameObject);
        gameObject.transform.SetParent(GameSceneRef.instance.drawPile);

        Card card = gameObject.GetComponent<Card>();
        Initialize(card, soCardBase);
        return card;
    }

    private static void Initialize(Card card, SoCardBase soCardBase)
    {
        card.cardState = CardStateEnum.inDrawPile;
        card.cardBase = soCardBase;
        card.titleText.text = soCardBase.title;
        card.cost.text = soCardBase.influenceCost.ToString();
        card.descriptionText.text = soCardBase.description;
        card.factionColorImage.color = soCardBase.faction.color;
        card.backgroundImage.sprite = soCardBase.image;
        BattleSceneActions.OnStartSpawning += card.MoveFromHandToDiscardList;
    }
}