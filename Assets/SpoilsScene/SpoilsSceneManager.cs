using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spoils scene, including UI elements and card selection.
/// </summary>
public class SpoilsSceneManager : MonoBehaviour
{
    public RectTransform canvas;
    public RectTransform buttonParent;
    public Card selectedCard;
    private SpoilsUiPanel spoilsUiPanel;

    private void OnEnable()
    {
        SelectCardsActions.OnCardSelected += SetSelectedCard;
    }

    private void OnDisable()
    {
        SelectCardsActions.OnCardSelected -= SetSelectedCard;
    }

    private void Start()
    {
        InitializeUI();
        InitializeCards();
    }

    /// <summary>
    /// Initializes the UI elements for the spoils scene.
    /// </summary>
    private void InitializeUI()
    {
        // Add "Move on" button
        ButtonWithDelegate.CreateThis(() => LoadNextScene(), buttonParent, "Move on");

        // Create the spoils UI panel
        spoilsUiPanel = SpoilsUiPanel.Create(canvas);

        // Instantiate the mouse display manager
        Instantiate(Resources.Load("mouseDisplayManager") as GameObject, transform);
    }

    /// <summary>
    /// Initializes the cards to be displayed in the spoils scene.
    /// </summary>
    private void InitializeCards()
    {
        // Generate random rare card enums
        List<CardRareEnums> rateEnums = CardOptionsHandler.GetRareEnums(GameManager.instance.currentLevel.luck, 3);

        // Create cards based on rarity
        foreach (CardRareEnums rareEnums in rateEnums)
        {
            SoCardBase soCardBase = CardManager.Instance.GetRandomCard(rareEnums);
            CardFactory.Create(soCardBase, Card.CardMode.selectable, spoilsUiPanel.cardParent);
        }
    }

    /// <summary>
    /// Sets the selected card when a card is clicked.
    /// </summary>
    /// <param name="selectionBase">The selected card wrapped in a SelectionBase.</param>
    private void SetSelectedCard(SelectionBase selectionBase)
    {
        if (selectionBase.card.mode == Card.CardMode.selectable)
        {
            selectedCard = selectionBase.card;
        }
    }

    /// <summary>
    /// Loads the next scene if a card is selected.
    /// </summary>
    private void LoadNextScene()
    {
        if (selectedCard != null)
        {
            SoCardBase soCardBase = selectedCard.cardBase;

            // Add the selected card to the deck and trigger global actions
            GlobalActions.OnNewCardAddedToDeck(soCardBase);
            GlobalActions.SpoilScenesCompleted();
        }
        else
        {
            Debug.LogWarning("No card selected! Cannot proceed to the next scene.");
        }
    }
}
