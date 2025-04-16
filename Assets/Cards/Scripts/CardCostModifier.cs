using System;
using TMPro;
using UnityEngine;

public class CardCostModifier
{
    public int modifiedCost { get; private set; }
    public bool permanent;
    public delegate int ModifyCostHandler(int originalCost);
    private Card card;  


    // Constructor takes the Card instance and applies the modification
    public CardCostModifier(Card _card, ModifyCostHandler modifyCostHandler, bool permanent =false)
    {
        if (_card == null || _card.cardBase == null || _card.costText == null)
        {
            UnityEngine.Debug.LogError("Card, CardBase, or cost field is null.");
            return;
        }
        
        // Get the base cost from SoCardBase
        int baseCost = _card.cardBase.influenceCost;

        // Apply the modification logic
        modifiedCost = modifyCostHandler?.Invoke(baseCost) ?? baseCost;

        // Update the UI with the modified cost
        _card.costText.text = modifiedCost.ToString();
        _card.costText.color = Color.green;
        card = _card;
        _card.cardCostModifier = this;
        this.permanent = permanent;
        Debug.Log("Cost modifier applied to" + _card.cardBase.title +  " card has modifier " + _card.cardCostModifier.modifiedCost.ToString());
    }

    internal void Remove()
    {
        Debug.Log("Removing cost modifier");
        card.costText.text = card.cardBase.influenceCost.ToString();
        card.costText.color = Color.white;
    }
}