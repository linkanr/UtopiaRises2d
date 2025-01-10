using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IClickableObject
{
    [SerializeField]

    private CardAnimations cardAnimations;
    private SoCardBase cardBase;
    public CardStateEnum cardState;
    private float inAnimationTime = .5f;
    private float upDownScaleTime = .2f;

    private bool isSelected = false;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI cost;
    public Image factionColorImage;
    public Image backgroundImage;
    public bool selected;

    public static DisplayCard CreateDisplayCard(SoCardBase soCardBase, RectTransform parent)
    {
        GameObject gameObject = Instantiate(Resources.Load("DisplayCard") as GameObject);
        gameObject.transform.SetParent(parent);

        DisplayCard card = gameObject.GetComponent<DisplayCard>();
        card.cardState = CardStateEnum.inDisplayMenu;
        card.cardBase = soCardBase;
        card.TitleText.text = soCardBase.title;
        card.cost.text = soCardBase.influenceCost.ToString();
        card.descriptionText.text = soCardBase.description;
        card.factionColorImage.color = soCardBase.faction.color;
        card.backgroundImage.sprite = soCardBase.image;
        return card;

    }

    private void Awake()
    {

        cardAnimations = new CardAnimations(GetComponent<LayoutElement>(), new Vector2(1f, 1f), GetComponent<RectTransform>(), Ease.InOutSine);


    }
    private void OnDestroy()
    {

    }






    public void OnPointerClick(PointerEventData eventData)
    {
        
        selected = true;


    }



    public void OnPointerEnter(PointerEventData eventData)
    {



            cardState = CardStateEnum.mousedOver;
            cardAnimations.SimpleAnimation(upDownScaleTime, 1.6f);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayActions.OnMouseNotOverCard();
        //Debug.Log("on point exit");
        if (cardState != CardStateEnum.lockedForSelection && !isSelected)
        {
            cardState = CardStateEnum.availible;
            cardAnimations.SimpleAnimation(upDownScaleTime, 1f);

        }



    }
    

   



    public ClickableType GetClickableType()
    {
        return ClickableType.card;
    }
}
