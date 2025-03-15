using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectManager : SerializedMonoBehaviour
{
    public static VisualEffectManager Instance { get; private set; }

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("VisualEffectManager already exists!");
            return;
        }
        Instance = this;
    }
    public Dictionary<visualEffectsEnum, VisualEffect> visualEffects = new Dictionary<visualEffectsEnum, VisualEffect>();

     
    public static void PlayVisualEffect(visualEffectsEnum visualEffectsEnum, Vector3 position)
    {
        Instance.PlayVisualEffectInternal(visualEffectsEnum, position);
    }
    private void PlayVisualEffectInternal(visualEffectsEnum visualEffectsEnum, Vector3 position)
    {
        if (visualEffects.ContainsKey(visualEffectsEnum))
        {
            VisualEffect visualEffect = Instantiate(visualEffects[visualEffectsEnum]); 
             
            visualEffect.transform.position = position;
            visualEffect.Play();
        }
        else
        {
            Debug.Log("no VFX FOR DEATH");
        }
    }
}
public enum visualEffectsEnum
{
    death,
    minor,
    none
}