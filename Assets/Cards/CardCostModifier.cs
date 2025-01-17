using TMPro;
using UnityEngine;

public class CardCostModifier
{
    public int modifiedCost { get; private set; }
    public bool permanent;
    public delegate int ModifyCostHandler(int originalCost);

    // Constructor takes the Card instance and applies the modification
    public CardCostModifier(Card card, ModifyCostHandler modifyCostHandler, bool permanent =false)
    {
        if (card == null || card.cardBase == null || card.cost == null)
        {
            UnityEngine.Debug.LogError("Card, CardBase, or cost field is null.");
            return;
        }

        // Get the base cost from SoCardBase
        int baseCost = card.cardBase.influenceCost;

        // Apply the modification logic
        modifiedCost = modifyCostHandler?.Invoke(baseCost) ?? baseCost;

        // Update the UI with the modified cost
        card.cost.text = modifiedCost.ToString();
        card.cost.color = Color.green;
        this.permanent = permanent;
    }
}