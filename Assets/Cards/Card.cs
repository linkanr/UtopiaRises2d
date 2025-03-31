using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IClickableObject
{
    public enum CardMode { playable, selectable, inspectable }

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] public TextMeshProUGUI costText;
    [SerializeField] public TextMeshProUGUI damageText;
    [SerializeField] public TextMeshProUGUI reachText;
    [SerializeField] public TextMeshProUGUI lifeText;
    [SerializeField] private Image factionColorImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject outline;

    public CardAnimations cardAnimations;
    public CardSelectionHandler selectionHandler;

    public CardCostModifier cardCostModifier;
    public SelectionBase selectionBase;

    public SoCardBase cardBase { get; private set; }
    public CardStateEnum cardState;
    public bool isSelected;
    public CardMode mode;

    public static Card currentlySelectedCard; // Track the currently selected card

    public void Initialize(SoCardBase cardBase, CardMode mode)
    {
        selectionHandler = new CardSelectionHandler(this);

        outline.SetActive(false);
        selectionBase = new SelectionBase(this);
        this.cardBase = cardBase;
        this.mode = mode;

        cardAnimations = new CardAnimations(
            GetComponent<LayoutElement>(),
            GetComponent<RectTransform>(),
            Ease.InOutSine,
            GameSceneRef.instance.inHandPile
        );

        isSelected = false;
        cardState = mode == CardMode.selectable ? CardStateEnum.inDisplayMenu : CardStateEnum.availible;

        switch (cardBase.cardType)
        {
            case CardType.summonTower:
                SoCardInstanciate soCardInstanciate = (SoCardInstanciate)cardBase;
                Stats stats = soCardInstanciate.prefab.GetStats();
                damageText.text = stats.damageAmount.ToString();
                reachText.text = stats.maxRange.ToString();
                lifeText.text = stats.lifeTime.ToString();
                break;
            case CardType.areaDamage:
                
                CardDamageArea damageArea = (CardDamageArea)cardBase;
                damageText.text = damageArea.damage.ToString();
                reachText.text = damageArea.diameter.ToString();

                break;
            default:
                damageText.gameObject.SetActive(false);
                reachText.gameObject.SetActive(false);
                lifeText.gameObject.SetActive(false);
                break;
        }



        GetComponentInChildren<CardSetBg>().Init(cardBase.cardType);
        titleText.text = cardBase.title;
        descriptionText.text = cardBase.description;
        costText.text = cardBase.influenceCost.ToString();
        factionColorImage.color = cardBase.faction.color;
        backgroundImage.sprite = cardBase.image;

        outline.SetActive(false);
    }

    private void Update()
    {
        if (cardState == CardStateEnum.lockedForSelection)
        {
            selectionHandler.HandleSelectionUpdate();
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                selectionHandler.ResetCardSelection();
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle playable cards
        if (mode == CardMode.playable && selectionHandler.IsCardClickable())
        {
            // Deselect the currently selected card (if any)
            if (currentlySelectedCard != null && currentlySelectedCard != this)
            {
                currentlySelectedCard.Deselect();
            }

            // Lock this card for selection
            selectionHandler.LockCardForSelection();
            currentlySelectedCard = this; // Update the global reference
        }
        // Handle selectable cards
        else if (mode == CardMode.selectable)
        {
            Select();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayActions.OnMouseOverCard();
        if (!isSelected && currentlySelectedCard != this && cardState != CardStateEnum.lockedForSelection)
        {
            cardState = CardStateEnum.mousedOver;
            cardAnimations.SimpleAnimation(mode == CardMode.selectable ? 1.6f : 1.2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayActions.OnMouseNotOverCard();
        // Prevent resetting state if the card is selected or locked for animation
        if (cardState == CardStateEnum.lockedForSelection || cardState == CardStateEnum.lockedForAnimation)
        {
            return;
        }
        // Reset state and scale only if the card is not selected
        cardState = CardStateEnum.availible;
        cardAnimations.SimpleAnimation(1f); // Reset scale to default
    }

    public void Select()
    {
        // Deselect the previously selected card if it's not this one
        if (currentlySelectedCard != null && currentlySelectedCard != this)
        {
            currentlySelectedCard.Deselect();
        }

        // Set this card as the currently selected card
        currentlySelectedCard = this;
        isSelected = true;
        outline.SetActive(true);

        // Lock the card for selection
        cardState = CardStateEnum.lockedForSelection;

        // Immediately override any ongoing animations
        cardAnimations.AnimateScale(cardAnimations.clickScale, this);

        // Notify listeners
        SelectCardsActions.InvokeCardSelected(selectionBase);
    }

    private void Deselect()
    {
        isSelected = false;
        outline.SetActive(false);
        cardState = CardStateEnum.availible;

        // Immediately reset scale
        cardAnimations.ScaleResetAndRelease(this);

        // Clear the global reference to the currently selected card
        if (currentlySelectedCard == this)
        {
            currentlySelectedCard = null;
        }
    }

    public ClickableType GetClickableType()
    {
        return ClickableType.card;
    }
}
