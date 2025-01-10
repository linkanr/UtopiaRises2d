using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card:MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,IClickableObject
{
    [SerializeField]
    
    private CardAnimations cardAnimations;
    private SoCardBase cardBase;
    public CardStateEnum cardState;
    private float inAnimationTime = .5f;
    private float upDownScaleTime = .2f;
    public float mouseOverScale = 1.8f;
    public float clickScale = 2.5f;

    private bool isSelected = false;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI cost;
    public Image factionColorImage;
    public Image backgroundImage;

    public static Card Create(SoCardBase soCardBase)
    {
        GameObject gameObject =  Instantiate(Resources.Load("Card") as GameObject);
        gameObject.transform.SetParent(GameSceneRef.instance.drawPile);

        Card card = gameObject.GetComponent<Card>();
        card.cardState = CardStateEnum.inDrawPile;
        card.cardBase = soCardBase;
        card.TitleText.text = soCardBase.title;
        card.cost.text =soCardBase.influenceCost.ToString();
        card.descriptionText.text = soCardBase.description;
        card.factionColorImage.color = soCardBase.faction.color;
        card.backgroundImage.sprite = soCardBase.image;
        BattleSceneActions.OnStartSpawning += card.MoveFromHandToDiscardList;
        return card;

    }

    private void Awake()
    {

        cardAnimations = new CardAnimations(GetComponent<LayoutElement>(), new Vector2(1f, 1f), GetComponent<RectTransform>(), Ease.InOutSine);


    }
    private void OnDestroy()
    {
        BattleSceneActions.OnStartSpawning -= MoveFromHandToDiscardList;
    }

    private void MoveFromHandToDiscardList()
    {
        if (cardState == CardStateEnum.availible)
        CardsInPlayManager.instance.DiscardCardInHand(this);
    }

    internal void AddToDiscardPileWithAnimation()
    {
        cardState = CardStateEnum.inDiscardPile;
        Action action = () => { transform.SetParent(GameSceneRef.instance.discardPile); };
        cardAnimations.AnimateScale(.5f,this,0f,action);
        

   
    }
    internal void MoveLeftOversToDiscard()
    {
        cardState = CardStateEnum.inDiscardPile;
        transform.SetParent(GameSceneRef.instance.discardPile);
    }

    internal void AddedToDrawPile()
    {
        cardState = CardStateEnum.inDrawPile;
        cardAnimations.SetScale(0f);
        transform.SetParent(GameSceneRef.instance.drawPile);
    }
    internal void AddToExhaustPile()
    {
        cardState = CardStateEnum.exhausted;
        transform.SetParent(GameSceneRef.instance.exhusedPile);
    }

    internal void DestroySingleUseCard()
    {
        CardManager.Instance.ownedCards.Remove(cardBase);
        Destroy(gameObject);
    }
    public void AddToHand()
    {
        cardAnimations.SetScale(0f);
        transform.SetParent( GameSceneRef.instance.inHandPile);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs( false));
        //BattleSceneActions.OnNewCardAdded(this);
        cardState = CardStateEnum.availible;
        cardAnimations.AnimateScale(inAnimationTime, this, 1f);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("clicked");

        if (cardState != CardStateEnum.lockedForSelection && cardState != CardStateEnum.resetting && cardState != CardStateEnum.inDisplayMenu)//if free card then lock
        {
            
            if (cardBase is IHasClickEffect)
            {
                IHasClickEffect effect = cardBase as IHasClickEffect;
                MouseDisplayManager.OnRemoveDisplay();
                MouseDisplayManager.OnSetNewSprite(new OnSetSpriteArgs {sprite = effect.GetSprite(),size = effect.GetSpriteSize() } );
            }
            if (cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.damagable)
            {
                DisplayActions.OnHighligtSceneObject(true);
            }


            Debug.Log("is availbe, locking");
            isSelected = true;
            cardState = CardStateEnum.lockedForSelection;
            
            cardAnimations.AnimateScale(upDownScaleTime, this, clickScale);
            int sizeX = 1;
            int sizeY = 1;
            if ( cardBase is SoCardInstanciate) 
            {
               SoCardInstanciate soCardInstanciate = cardBase as SoCardInstanciate;
               sizeX = soCardInstanciate.sizeX;
                sizeY = soCardInstanciate.sizeY;
            }

            DisplayActions.OnDisplayCell(new OnDisplayCellArgs(cardBase.cardCanBePlayedOnEnum == CardCanBePlayedOnEnum.emptyGround,sizeX,sizeY) );
            
        }

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayActions.OnMouseOverCard();
        // Debug.Log("on point enter");
        if (cardState != CardStateEnum.lockedForSelection && !isSelected)
        {
            
            cardState = CardStateEnum.mousedOver;
            cardAnimations.AnimateScale(upDownScaleTime,this,mouseOverScale);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayActions.OnMouseNotOverCard();
        //Debug.Log("on point exit");
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
                ClickableType clickableType = WorldSpaceUtils.CheckClickableType();
                //Debug.Log("clicked on " + clickableType.ToString());
                if (MouseDisplayManager.instance.mouseOverCard)
                {
                    FailedToPlayCard();
                    return; 
                }
                switch (cardBase.cardCanBePlayedOnEnum)
                {
                    case CardCanBePlayedOnEnum.instantClick:
                        if (clickableType != ClickableType.card)
                        {
                            PlayCard();

                        }
                        else
                        {
                            FailedToPlayCard();
                        }   
                        return;
                    case CardCanBePlayedOnEnum.damagable:
                        if (clickableType == ClickableType.SceneObject)
                        {
                            PlayCard(); 
                        }
                        else
                        {
                            FailedToPlayCard();
                        }
                        return ;
                        case CardCanBePlayedOnEnum.emptyGround:
                        if (clickableType!= ClickableType.card)
                        {
                            PlayCard();
                        }
                        else
                        {
                            FailedToPlayCard();
                        }
                        return ;
                }
         


            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            FailedToPlayCard();
        }

    }

    private bool CheckIfNotClickedCard()/// Add diffent checks based of card type
    {
        ClickableType clickableType = WorldSpaceUtils.CheckClickableType();
        if (clickableType != ClickableType.card)
        {
            return true;
        }
        return false;



    }

    private void FailedToPlayCard()
    {
        Debug.Log("clicked a GO, realeasing");
        MouseDisplayManager.OnRemoveDisplay();
        cardState = CardStateEnum.availible;
        isSelected = false;
        DisplayActions.OnHighligtSceneObject(false);
        DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false));
        cardAnimations.ScaleResetAndRelease(inAnimationTime, this);
    }

    private void PlayCard()
    {
        string result;
        if (cardBase.Effect(WorldSpaceUtils.GetMouseWorldPosition(), out result))
        {
            cardState = CardStateEnum.availible;
            isSelected = false;
            DisplayActions.OnDisplayCell(new OnDisplayCellArgs(false));
            DisplayActions.OnHighligtSceneObject(false);
            MouseDisplayManager.OnRemoveDisplay();
            CardsInPlayManager.instance.DiscardCardInHand(this);    
            cardState = CardStateEnum.inDiscardPile;
            //cardAnimations.ScaleResetAndRelease(inAnimationTime, this);
        }
        else
        {
            GlobalActions.Tooltip(new ToolTipArgs { time = 3f, Tooltip = result });
            return;
        }
    }

    public ClickableType GetClickableType()
    {
        return ClickableType.card;
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
    inDrawPile,
    exhausted,
    inDisplayMenu
    
}
