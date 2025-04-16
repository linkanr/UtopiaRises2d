using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimations
{
    private Ease ease;
    private LayoutElement layoutElement;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform; // Reference to the parent layout's RectTransform
    private float prefX;
    private float prefY;
    public  float inAnimationTime = 0.1f;
    public const  float upDownScaleTime = 0.1f;
    public  float mouseOverScale = 1.5f;
    public float clickScale = 2f;

    public CardAnimations(LayoutElement layoutElement, RectTransform rectTransform, Ease ease, RectTransform parentRectTransform)
    {
        this.layoutElement = layoutElement;
        this.rectTransform = rectTransform;
        this.ease = ease;
        this.prefX = 1f;
        this.prefY = 1f;
        this.parentRectTransform = parentRectTransform;
    }

    public void SetScale(float scale)
    {
        //Debug.Log("setting scale to " + scale);
        rectTransform.localScale = new Vector3(scale, scale, 1f);
        UpdateLayoutElement(scale);
        ForceUIUpdate();
    }

    public void SimpleAnimation(float scale)
    {
        Vector3 targetSize = new Vector3(prefX * scale, prefY * scale, 1f);
        rectTransform.DOScale(targetSize, inAnimationTime)

            .SetEase(ease)
            .OnUpdate(() =>
            {
                UpdateLayoutElement(scale);
                ForceUIUpdate();
            });
    }

    public void AnimateScale(float scale, Card card, float time = upDownScaleTime)
    {

        //Debug.Log("setting scale "+ scale + "  in time " + time);
        Vector3 targetSize = new Vector3(prefX * scale, prefY * scale, 1f);

        // Temporarily lock the card's state
        CardStateEnum previousState = card.cardState;
        card.cardState = CardStateEnum.lockedForAnimation;

        rectTransform.DOScale(targetSize, time)
            .SetEase(ease)
            .OnUpdate(() =>
            {
                UpdateLayoutElement(scale);
                ForceUIUpdate();
            })
            .OnComplete(() =>
            {
                // Restore the previous state only if the card is not selected


                    card.cardState = previousState;

 

                ForceUIUpdate();
            });
    }




    public void ScaleResetAndRelease(Card card)
    {
        Vector3 originalSize = new Vector3(prefX, prefY, 1f);
        CardStateEnum previousState = card.cardState;
        card.cardState = CardStateEnum.resetting;

        rectTransform.DOScale(originalSize, upDownScaleTime)
            .SetEase(ease)
            .OnUpdate(() =>
            {
                UpdateLayoutElement(1f);
                ForceUIUpdate();
            })
            .OnComplete(() =>
            {
                card.cardState = previousState;
                ForceUIUpdate();
            });
    }

    private void UpdateLayoutElement(float scale)
    {
        if (layoutElement != null)
        {
            layoutElement.preferredWidth = prefX * scale;
            layoutElement.preferredHeight = prefY * scale;
        }
    }

    private void ForceUIUpdate()
    {
        if (parentRectTransform != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
            Canvas.ForceUpdateCanvases();
        }
    }
}
