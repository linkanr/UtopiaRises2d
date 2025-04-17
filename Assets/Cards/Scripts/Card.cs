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

    public static Card currentlySelectedCard;
    [SerializeField] private CardVisualLayers visualLayers;


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
            GameSceneRef.instance.inHandPile,
            visualLayers
        )
        {

        };
        isSelected = false;
        cardState = mode == CardMode.selectable ? CardStateEnum.inDisplayMenu : CardStateEnum.availible;

        switch (cardBase.cardType)
        {
            case CardType.summonTower:
                SoCardInstanciate soCardInstanciate = (SoCardInstanciate)cardBase;
                Stats stats = soCardInstanciate.prefab.GetStats();
                damageText.text = stats.damageAmount().ToString();
                reachText.text = stats.maxRange().ToString();
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
        titleText.text = GameStringParser.Parse(cardBase.title);
        descriptionText.text = GameStringParser.Parse(cardBase.description);
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
        if (mode == CardMode.playable && selectionHandler.IsCardClickable())
        {
            if (currentlySelectedCard != null && currentlySelectedCard != this)
            {
                currentlySelectedCard.Deselect();
            }

            selectionHandler.LockCardForSelection();
            currentlySelectedCard = this;
        }
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
        if (cardState == CardStateEnum.lockedForSelection || cardState == CardStateEnum.lockedForAnimation)
        {
            return;
        }
        cardState = CardStateEnum.availible;
        cardAnimations.SimpleAnimation(1f);
    }

    public void Select()
    {
        if (currentlySelectedCard != null && currentlySelectedCard != this)
        {
            currentlySelectedCard.Deselect();
        }

        currentlySelectedCard = this;
        isSelected = true;
        outline.SetActive(true);

        cardState = CardStateEnum.lockedForSelection;
        cardAnimations.AnimateScale(cardAnimations.clickScale, this);

        SelectCardsActions.InvokeCardSelected(selectionBase);
    }

    private void Deselect()
    {
        isSelected = false;
        outline.SetActive(false);
        cardState = CardStateEnum.availible;
        cardAnimations.ScaleResetAndRelease(this);

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
