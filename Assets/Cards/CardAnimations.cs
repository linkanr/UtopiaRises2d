using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Xml.Serialization;
using System;

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
    public Vector3 transformVector;
    public RectTransform rectTransform;
    public bool animating = false;
    public float prefX;
    public float prefY;
    public Sequence mouseOverSequence;


    public void SetScale(float scale)
    {
        rectTransform.localScale = new Vector2(scale, scale);
    }
    public void SimpleAnimation(float animationTime, float scale, Action _onComplete = null)
    {
        Vector3 origSize = new Vector3(prefX * scale, prefY * scale);
        rectTransform.DOScale(origSize, animationTime).SetEase(ease);
    }
    public  void AnimateScale(float animationTime, Card card, float scale, Action _onComplete = null)
    {
        

            Vector3 origSize = new Vector3(prefX* scale, prefY *scale);
            CardStateEnum previusState = card.cardState;
            card.cardState = CardStateEnum.lockedForAnimation;
            BattleSceneActions.OnCardsBeginDrawn?.Invoke();            
            rectTransform.DOScale(origSize, animationTime).SetEase(ease).onComplete = () => { card.cardState = previusState;BattleSceneActions.OnCardsEndDrawn();_onComplete?.Invoke(); };
            //layoutElement.DOPreferredSize(origSize, animationTime, false).SetEase(ease).onComplete = () => { card.cardState = previusState; };


       
    }


    public void ScaleResetAndRelease(float animationTime, Card card, Action _onComplete = null)
    {
        BattleSceneActions.OnCardsBeginDrawn?.Invoke();
        Vector3 origSize = new Vector3(prefX, prefY);
        CardStateEnum previusState = card.cardState;
        card.cardState = CardStateEnum.resetting;
        rectTransform.DOScale(origSize, animationTime).SetEase(ease).onComplete = () => { card.cardState = previusState; _onComplete?.Invoke(); BattleSceneActions.OnCardsEndDrawn(); };
        //layoutElement.DOPreferredSize(origSize, animationTime, false).SetEase(ease).onComplete = () => { card.cardState = previusState; };
    }


}
