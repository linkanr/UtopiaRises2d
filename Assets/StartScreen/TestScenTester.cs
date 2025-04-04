using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TestScenTester : MonoBehaviour
{
    // References to GUI elements
    public TextMeshProUGUI cardNameDisplay;
    public Button rollButton;
    public TMP_InputField luckInput;
    public TMP_InputField cardAmountInput;

    void Start()
    {
        if (rollButton != null)
        {
            rollButton.onClick.AddListener(OnRollButtonPressed);
        }
    }

    // Called when the roll button is pressed
    void OnRollButtonPressed()
    {
        cardNameDisplay.text = string.Empty;
        if (CardManager.instance == null)
        {
            Debug.LogError("CardManager instance is null!");
            return;
        }

        int luckValue = ParseLuckInput();
        if (luckValue == -1)
        {
            Debug.LogError("Invalid luck input");
            return;
        }

        int cardAmount = ParseCardAmountInput();
        if (cardAmount <= 0)
        {
            Debug.LogError("Invalid card amount input");
            return;
        }

        // Determine card rarities based on luck
        List<CardRareEnums> rarities = CardOptionsHandler.GetRareEnums(luckValue, cardAmount);

        foreach (CardRareEnums rarity in rarities)
        {
            SoCardBase newCard = CardManager.instance.GetRandomCard(rarity);

            // Update GUI with the new card's name and rarity
            if (newCard != null && cardNameDisplay != null)
            {
                cardNameDisplay.text += $"{newCard.title} ({rarity})\n";
            }
            else
            {
                Debug.LogWarning("No card retrieved or cardNameDisplay is null");
            }
        }
    }

    // Helper method to parse the luck input field
    private int ParseLuckInput()
    {
        if (luckInput == null || string.IsNullOrEmpty(luckInput.text))
        {
            return -1; // Invalid input
        }

        if (int.TryParse(luckInput.text, out int luckValue))
        {
            return luckValue;
        }

        return -1; // Invalid input
    }

    // Helper method to parse the card amount input field
    private int ParseCardAmountInput()
    {
        if (cardAmountInput == null || string.IsNullOrEmpty(cardAmountInput.text))
        {
            return -1; // Invalid input
        }

        if (int.TryParse(cardAmountInput.text, out int cardAmount))
        {
            return cardAmount;
        }

        return -1; // Invalid input
    }
}
