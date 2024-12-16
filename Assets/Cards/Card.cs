using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card:MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    
    private CardAnimations cardAnimations;
    private SoCardBase cardBase;
    public CardStateEnum cardState;
    private float inAnimationTime = .5f;
    private float upDownScaleTime = .2f;
    private Ease ease;
    private bool isSelected = false;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI descriptionText;
    public Image factionColorImage;
    public Image backgroundImage;

    public static Card Create(SoCardBase soCardBase, Transform parent = null)
    {
        GameObject gameObject =  Instantiate(Resources.Load("Card") as GameObject);
        if (parent != null)
        {
            gameObject.transform.SetParent(parent);
        }
        Card card = gameObject.GetComponent<Card>();
        card.cardBase = soCardBase;
        card.TitleText.text = soCardBase.description;
        card.descriptionText.text = soCardBase.description;
        card.factionColorImage.color = soCardBase.faction.color;
        card.backgroundImage.sprite = soCardBase.image;
        return card;

    }
    private void Awake()
    {
        
        cardAnimations = new CardAnimations(GetComponent<LayoutElement>(), new Vector2(1f, 1f), GetComponent<RectTransform>(), ease);


    }
    internal void AddToDiscardPile()
    {
        cardAnimations.SetScale(0f);
        this.transform.SetParent( GameSceneRef.instance.discardPile);
    }
    internal void AddedToDrawPile()
    {
        cardAnimations.SetScale(0f);
        this.transform.parent = GameSceneRef.instance.drawPile;
    }
    internal void AddToExhaustPile()
    {
        this.transform.parent = GameSceneRef.instance.exhusedPile;
    }
    internal void DestroySingleUseCard()
    {
        CardManager.Instance.cardsList.Remove(cardBase);
        Destroy(gameObject);
    }
    public void Dealt()
    {
        cardAnimations.SetScale(0f);
        this.transform.SetParent(GameSceneRef.instance.panel);

        BattleSceneActions.OnCardLocked(false);
        //BattleSceneActions.OnNewCardAdded(this);
        cardState = CardStateEnum.availible;
        cardAnimations.AnimateScale(inAnimationTime, this, 1f);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");

        if (cardState != CardStateEnum.lockedForSelection && cardState != CardStateEnum.resetting)//if free card then lock
        {
            if (cardBase is IHasClickEffect)
            {
                IHasClickEffect effect = cardBase as IHasClickEffect;
                MouseDisplayManager.OnRemoveDisplay();
                MouseDisplayManager.OnSetNewSprite(new OnSetSpriteArgs {sprite = effect.GetSprite(),size = effect.GetSpriteSize() } );
            }

            Debug.Log("is availbe, locking");
            isSelected = true;
            cardState = CardStateEnum.lockedForSelection;
            
            cardAnimations.AnimateScale(upDownScaleTime, this, 1.22f);
            BattleSceneActions.OnCardLocked(true);
            
        }

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("on point enter");
        if (cardState != CardStateEnum.lockedForSelection && !isSelected)
        {
            cardState = CardStateEnum.mousedOver;
            cardAnimations.AnimateScale(upDownScaleTime,this,1.1f);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("on point exit");
        if (cardState != CardStateEnum.lockedForSelection && !isSelected)
        {
            cardState = CardStateEnum.availible;
            cardAnimations.ScaleResetAndRelease(upDownScaleTime, this);

        }
        


    }
    private void Update()
    {
        if (cardState == CardStateEnum.lockedForSelection)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())//not clicking another object do stuff
                    
                {
                    string result;
                    if (cardBase.Effect(WorldSpaceUtils.GetMouseWorldPosition(),out result)) 
                    {
                        cardState = CardStateEnum.availible;
                        isSelected = false;
                        BattleSceneActions.OnCardLocked(false);
                        MouseDisplayManager.OnRemoveDisplay();
                        AddToDiscardPile();
                        cardAnimations.ScaleResetAndRelease(inAnimationTime, this);
                    }
                    else
                    {
                        GlobalActions.Tooltip(new ToolTipArgs { time= 3f, Tooltip = result });
                        return;
                    }
                    

                }
                else //clicked on another GO
                {
                    Debug.Log("clicked a GO, realeasing");
                    MouseDisplayManager.OnRemoveDisplay();
                    cardState = CardStateEnum.availible;
                    isSelected = false;
                    BattleSceneActions.OnCardLocked(false);
                    cardAnimations.ScaleResetAndRelease(inAnimationTime, this);
                }
            
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            cardState = CardStateEnum.availible;
            isSelected = false;
            BattleSceneActions.OnCardLocked(false);
            MouseDisplayManager.OnRemoveDisplay();
            cardAnimations.ScaleResetAndRelease(inAnimationTime, this);
        }

    }


}

public enum CardStateEnum
{
    lockedForSelection,
    lockedForAnimation,
    mousedOver,
    availible,
    resetting,
    inDiscardPile,
    inDrawPile
}
