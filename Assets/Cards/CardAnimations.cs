using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Xml.Serialization;

public class CardAnimations 
{
    public CardAnimations(LayoutElement _layOutElement, Vector3 size, RectTransform _rectTransform,Ease ease)
    {
        layoutElement = _layOutElement;
        prefX = size.x;
        prefY = size.y;
        rectTransform = _rectTransform;
        this.ease = ease;
    } 
    Ease ease;
    public LayoutElement layoutElement;
    private Vector3 transformVector;
    private RectTransform rectTransform;
    public bool animating = false;
    public float prefX;
    public float prefY;
    public Sequence mouseOverSequence;
   

    public void SetScale(float scale)
    {
        rectTransform.localScale = new Vector2(scale, scale);
    }
    public  void AnimateScale(float animationTime, Card card, float scale)
    {
        

            Vector3 origSize = new Vector3(prefX* scale, prefY *scale);
            CardStateEnum previusState = card.cardState;
            card.cardState = CardStateEnum.lockedForAnimation;
            BattleSceneActions.OnCardsBeginDrawn();            
            rectTransform.DOScale(origSize, animationTime).SetEase(ease).onComplete = () => { card.cardState = previusState;BattleSceneActions.OnCardsEndDrawn(); };
            //layoutElement.DOPreferredSize(origSize, animationTime, false).SetEase(ease).onComplete = () => { card.cardState = previusState; };


       
    }


    public void ScaleResetAndRelease(float animationTime, Card card)
    {
        Vector3 origSize = new Vector3(prefX, prefY);
        CardStateEnum previusState = card.cardState;
        card.cardState = CardStateEnum.resetting;
        rectTransform.DOScale(origSize, animationTime).SetEase(ease).onComplete = () => { card.cardState = previusState; };
        //layoutElement.DOPreferredSize(origSize, animationTime, false).SetEase(ease).onComplete = () => { card.cardState = previusState; };
    }


}
