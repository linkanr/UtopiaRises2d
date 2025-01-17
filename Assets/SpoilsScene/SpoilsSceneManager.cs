using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spoils scene, including UI elements and card selection.
/// </summary>
public class SpoilsSceneManager : MonoBehaviour
{
    public RectTransform canvas;
    public RectTransform buttonParent;
    public SelectionCards selectedCard;
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
        ButtonWithDelegate.CreateThis(() => LoadNextScene(), buttonParent, "Move on");
        spoilsUiPanel = SpoilsUiPanel.Create(canvas);
        Instantiate(Resources.Load("mouseDisplayManager") as GameObject, transform);
    }

    /// <summary>
    /// Initializes the cards to be displayed in the spoils scene.
    /// </summary>
    private void InitializeCards()
    {
        List<CardRareEnums> rateEnums = CardOptionsHandler.GetRareEnums(GameManager.instance.soEnemyLevelList.luck, 3);
        CardManager.Instance.GetRandomCard(CardRareEnums.rare);
        foreach (CardRareEnums rareEnums in rateEnums)
        {
            SoCardBase soCardBase = CardManager.Instance.GetRandomCard(rareEnums);
            SelectionCards.CreateDisplayCard(soCardBase, spoilsUiPanel.cardParent);
        }
    }

    /// <summary>
    /// Sets the selected card.
    /// </summary>
    /// <param name="cards">The selected card.</param>
    private void SetSelectedCard(SelectionCards cards)
    {
        selectedCard = cards;
    }

    /// <summary>
    /// Loads the next scene if a card is selected.
    /// </summary>
    private void LoadNextScene()
    {
        if (selectedCard != null)
        {
            SoCardBase soCardBase = selectedCard.cardBase;
            GlobalActions.OnNewCardAddedToDeck(soCardBase);
            GlobalActions.SpoilScenesCompleted();
        }
    }
}
