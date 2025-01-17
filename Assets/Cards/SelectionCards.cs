using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionCards : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IClickableObject
{
    [SerializeField]

    private CardAnimations cardAnimations;
    public SoCardBase cardBase { get; private set; }
    public CardStateEnum cardState;
    private float inAnimationTime = .5f;
    private float upDownScaleTime = .2f;

    private bool isSelected = false;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI cost;
    public Image factionColorImage;
    public Image backgroundImage;
    public GameObject outline;


    public static SelectionCards CreateDisplayCard(SoCardBase soCardBase, RectTransform parent)
    {
        GameObject gameObject = Instantiate(Resources.Load("DisplayCard") as GameObject);
        gameObject.transform.SetParent(parent);

        SelectionCards card = gameObject.GetComponent<SelectionCards>();
        card.cardState = CardStateEnum.inDisplayMenu;
        card.cardBase = soCardBase;
        card.TitleText.text = soCardBase.title;
        card.cost.text = soCardBase.influenceCost.ToString();
        card.descriptionText.text = soCardBase.description;
        card.factionColorImage.color = soCardBase.faction.color;
        card.backgroundImage.sprite = soCardBase.image;
        card.outline.SetActive(false);
        return card;

    }
    private void OnEnable()
    {
        SelectCardsActions.OnCardSelected += OnCardSelected;
    }
    private void OnDisable()
    {
        SelectCardsActions.OnCardSelected -= OnCardSelected;
    }

    private void OnCardSelected(SelectionCards cards)
    {
        if (cards != this)
        {
            if (isSelected)
            {
                isSelected = false;
                outline.SetActive(false);
                cardAnimations.SimpleAnimation(1f);

            }

        }

    }

    private void Awake()
    {

        cardAnimations = new CardAnimations(GetComponent<LayoutElement>(), GetComponent<RectTransform>(), Ease.InOutSine, GetComponentInParent<RectTransform>());


    }
    private void OnDestroy()
    {

    }






    public void OnPointerClick(PointerEventData eventData)
    {
        
        isSelected = true;
        outline.SetActive(true);
        SelectCardsActions.OnCardSelected.Invoke(this);


    }



    public void OnPointerEnter(PointerEventData eventData)
    {



            cardState = CardStateEnum.mousedOver;
            cardAnimations.SimpleAnimation( 1.6f);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayActions.OnMouseNotOverCard();
        //Debug.Log("on point exit");
        if (cardState != CardStateEnum.lockedForSelection && !isSelected)
        {
            cardState = CardStateEnum.availible;
            cardAnimations.SimpleAnimation(1f);

        }



    }
    

   



    public ClickableType GetClickableType()
    {
        return ClickableType.card;
    }
}
