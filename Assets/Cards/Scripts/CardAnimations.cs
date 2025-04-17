using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimations
{
    private Ease ease;
    private LayoutElement layoutElement;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private float prefX;
    private float prefY;
    private CardVisualLayers cardVisualLayers;
    public float inAnimationTime = 0.1f;
    public const float upDownScaleTime = 0.1f;
    public float mouseOverScale = 1.5f;
    public float clickScale = 2f;

    public CardAnimations(LayoutElement layoutElement, RectTransform rectTransform, Ease ease, RectTransform parentRectTransform, CardVisualLayers cardVisualLayers)
    {
        this.layoutElement = layoutElement;
        this.rectTransform = rectTransform;
        this.ease = ease;
        this.prefX = 1f;
        this.prefY = 1f;
        this.parentRectTransform = parentRectTransform;
        this.cardVisualLayers = cardVisualLayers;
    }

    public void SetScale(float scale)
    {
        rectTransform.localScale = new Vector3(scale, scale, 1f);
        UpdateLayoutElement(scale);
        ForceUIUpdate();
    }

    public void SimpleAnimation(float scale, Action onComplete = null)
    {
        Vector3 targetSize = new Vector3(prefX * scale, prefY * scale, 1f);
        rectTransform.DOScale(targetSize, inAnimationTime)
            .SetEase(ease)
            .OnUpdate(() =>
            {
                UpdateLayoutElement(scale);
                ForceUIUpdate();
            })
            .OnComplete(() => onComplete?.Invoke());
    }

    public void AnimateScale(float scale, Card card, float time = upDownScaleTime, Action onComplete = null)
    {
        Vector3 targetSize = new Vector3(prefX * scale, prefY * scale, 1f);
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
                card.cardState = previousState;
                ForceUIUpdate();
                onComplete?.Invoke();
            });
    }

    public void ScaleResetAndRelease(Card card, Action onComplete = null)
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
                onComplete?.Invoke();
            });
    }

    public void PlayCardAnimation(Action onComplete = null)
    {

        Vector2 mouseScreenPos = Input.mousePosition;

        Vector2 mouseLocalPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRectTransform, mouseScreenPos, null, out mouseLocalPos);

        Vector2 startLocalPos = rectTransform.anchoredPosition;
        Vector2 direction = (mouseLocalPos - startLocalPos).normalized;

        float launchDistance = 400f;
        Vector2 finalTarget = startLocalPos + direction * launchDistance;

        float duration = 0.65f;
        Vector3 endScale = new Vector3(0.1f, 0.1f, 1f);

        float spin = UnityEngine.Random.Range(0, 1f) < 0.5f
            ? UnityEngine.Random.Range(-270f, -90f)
            : UnityEngine.Random.Range(90f, 270f);

        CanvasGroup cg = rectTransform.GetComponent<CanvasGroup>();
        if (cg == null) cg = rectTransform.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 1f;

        Sequence s = DOTween.Sequence();

        s.Append(rectTransform.DOAnchorPos(finalTarget, duration).SetEase(Ease.InOutCubic))
         .Join(rectTransform.DOScale(endScale, duration).SetEase(Ease.InOutCubic))
         .Join(rectTransform.DORotate(new Vector3(0, 0, spin), duration, RotateMode.FastBeyond360))
         .Join(cg.DOFade(0f, duration))
         .OnComplete(() =>
         {
             rectTransform.localRotation = Quaternion.identity;
             ForceUIUpdate();
             onComplete?.Invoke();
         });
    }

    public void StartDissolveEffect()
    {
        if (cardVisualLayers == null || cardVisualLayers.bg == null || cardVisualLayers.burnMaterial == null)
        {
            Debug.LogWarning("Missing references for dissolve.");
            return;
        }

        Material mat = new Material(cardVisualLayers.burnMaterial);
        mat.SetTexture("_MainTex", cardVisualLayers.bg.sprite.texture);
        mat.SetFloat("_StartTime", Time.time);

        cardVisualLayers.bg.StartCoroutine(DelayedMaterialAssign(cardVisualLayers.bg, mat));
    }

    public IEnumerator DelayedMaterialAssign(Image image, Material mat)
    {
        yield return null;
        image.material = mat;
    }

    public void DiscardAnimation(Action onComplete = null)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(300f, 200f);

        CanvasGroup cg = rectTransform.GetComponent<CanvasGroup>();
        if (cg == null) cg = rectTransform.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 1f;

        float duration = 0.4f;

        DOTween.Kill(rectTransform);
        DOTween.Kill(cg);

        Sequence seq = DOTween.Sequence();
        seq.Append(rectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.InSine))
           .Join(cg.DOFade(0f, duration))
           .OnComplete(() =>
           {
               rectTransform.localRotation = Quaternion.identity;
               cardVisualLayers.bg.material = null;
               onComplete?.Invoke();
           });
    }

    public void ExhaustCardAnimation(Action onComplete = null)
    {
        StartDissolveEffect();

        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(0f, 50f);

        CanvasGroup cg = rectTransform.GetComponent<CanvasGroup>();
        if (cg == null) cg = rectTransform.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 1f;

        float duration = 0.6f;

        DOTween.Kill(rectTransform);
        DOTween.Kill(cg);

        Sequence seq = DOTween.Sequence();
        seq.Append(rectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.OutQuad))
           .Join(cg.DOFade(0f, duration).SetEase(Ease.InCubic))
           .OnComplete(() =>
           {
               rectTransform.localRotation = Quaternion.identity;
               cardVisualLayers.bg.material = null;
               onComplete?.Invoke();
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
