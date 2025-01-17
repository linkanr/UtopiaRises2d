using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class CardAnimations
{
    private Ease ease;
    private LayoutElement layoutElement;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform; // Reference to the parent layout's RectTransform
    private float prefX;
    private float prefY;
    public float inAnimationTime = 0.5f;
    public float upDownScaleTime = 0.2f;
    public float mouseOverScale = 1.5f;
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
                ForceUIUpdate(); // Ensure updates during animation
            });
    }

    public void AnimateScale(float scale, Card card)
    {
        Vector3 targetSize = new Vector3(prefX * scale, prefY * scale, 1f);
        CardStateEnum previousState = card.cardState;
        card.cardState = CardStateEnum.lockedForAnimation;

        rectTransform.DOScale(targetSize, upDownScaleTime)
            .SetEase(ease)
            .OnUpdate(() =>
            {
                UpdateLayoutElement(scale);
                LayoutRebuilder.ForceRebuildLayoutImmediate(GameSceneRef.instance.inHandPile);
                Canvas.ForceUpdateCanvases(); // Ensure updates during animation
            })
            .OnComplete(() =>
            {
                card.cardState = previousState;
                LayoutRebuilder.ForceRebuildLayoutImmediate(GameSceneRef.instance.inHandPile); // Final layout update
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
                ForceUIUpdate(); // Ensure updates during animation
            })
            .OnComplete(() =>
            {
                card.cardState = previousState;
                ForceUIUpdate(); // Ensure updates after animation completes
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
            Canvas.ForceUpdateCanvases(); // Ensure canvas redraw
        }
    }
}
