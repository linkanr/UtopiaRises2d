using UnityEngine;
[CreateAssetMenu(menuName = "Cards/Negotiations")]
public class CardNegotiations : SoCardBase
{
    public override bool ActualEffect(Vector3 position, out string failuerReason)
    {
        failuerReason = "";
        Card newCard = CardsInPlayManager.instance.DrawCard();
        if (newCard.cardBase.faction.politicalAlignment.ideolgicalAlignment == IdeolgicalAlignment.MiddleCentrist)
        {
            newCard.cardCostModifier = new CardCostModifier(newCard, (originalCost) => 0, false);
        }
        else
        {
            failuerReason = "Card is not Centrist so no cost reduction";
        }
        return true;
    }
}