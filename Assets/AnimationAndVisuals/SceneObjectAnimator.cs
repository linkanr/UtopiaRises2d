using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SceneObjectAnimator : MonoBehaviour
{
    private SpriteRenderer sr;
    public Coroutine currentAnim { get; private set; }
    public SoSceneObjectAnimationData animData { get; private set; }
    public string currentAnimationString;
    private float zRot;


    public static SceneObjectAnimator Create(SceneObject sceneObject)
    {
        var go = new GameObject("SceneObjectAnimator");
        go.transform.SetParent(sceneObject.transform);
        var animator = go.AddComponent<SceneObjectAnimator>();
        animator.sr = sceneObject.spriteRenderer;

        return animator;
    }

    public void Init(SoSceneObjectAnimationData data)
    {
        animData = data;
        
        PlayBirth();
    }
    public Action OnDeathAnimationFinished;
    public void PlayIdle() => PlayAnimation(animData.idleAnimationKey,true, false);
    public void PlayBirth() => PlayAnimation(animData.birthAnimationKey,false, false, ()=> PlayIdle());
    public void PlayDeath() => PlayAnimation(animData.deathAnimationKey,false, false,()=> OnDeathAnimationFinished());
    public void PlayAction() => PlayAnimation(animData.actionAnimationKey,animData.actionIs8Sliced, animData.loopAction, ()=>PlayIdle());
    private void PlayAnimation(string sheetKey, bool loop, bool slicedAnimation, Action OnComplete = null)
    {
        if (!this.enabled || !gameObject.activeInHierarchy)
        {
            Debug.LogWarning("⚠️ SceneObjectAnimator is disabled or inactive — cannot start animation.");
            return;
        }

        if (currentAnim != null)
            StopCoroutine(currentAnim);

        currentAnim = StartCoroutine(Animate(sheetKey, loop, OnComplete, slicedAnimation));
    }

    private IEnumerator Animate(string key, bool loop, Action OnComplete, bool slicedAnimation)
    {
        if (string.IsNullOrEmpty(key))
        {
            Debug.LogWarning("No animation key provided.");
            yield break;
        }

        currentAnimationString = key;
        Sprite[] frames = Resources.LoadAll<Sprite>($"spriteSheets/{key}");

        if (frames == null || frames.Length == 0)
        {
            Debug.LogError($"No sprites found for key: spriteSheets/{key}");
            yield break;
        }

        float frameDuration = 1f / animData.frameRate;

        if (slicedAnimation)
        {
            int directions = 8;
            int frameCountPerDirection = frames.Length / directions;
            int rot = (int)math.round(((((zRot - 90f) % 360f) + 360f) % 360f) / 45f) % directions;

            if (loop)
            {
                int index = 0;
                while (true)
                {
                    int spriteIndex = index + rot * frameCountPerDirection;
                    if (spriteIndex >= 0 && spriteIndex < frames.Length)
                        sr.sprite = frames[spriteIndex];

                    yield return new WaitForSeconds(frameDuration);
                    index = (index + 1) % frameCountPerDirection;
                }
            }
            else
            {
                for (int index = 0; index < frameCountPerDirection; index++)
                {
                    int spriteIndex = index + rot * frameCountPerDirection;
                    if (spriteIndex >= 0 && spriteIndex < frames.Length)
                        sr.sprite = frames[spriteIndex];

                    yield return new WaitForSeconds(frameDuration);
                }
            }
        }
        else
        {
            int frameCount = frames.Length;

            if (loop)
            {
                int index = 0;
                while (true)
                {
                    if (index >= 0 && index < frameCount)
                        sr.sprite = frames[index];

                    yield return new WaitForSeconds(frameDuration);
                    index = (index + 1) % frameCount;
                }
            }
            else
            {
                for (int index = 0; index < frameCount; index++)
                {
                    if (index >= 0 && index < frameCount)
                        sr.sprite = frames[index];

                    yield return new WaitForSeconds(frameDuration);
                }
            }
        }

        OnComplete?.Invoke();
    }





    internal void SetAngle(float angle)
    {
        zRot = angle;
    }
}
