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
        Debug.Log("Playing VFX");
        Instance.PlayVisualEffectInternal(visualEffectsEnum, position);
    }
    private void PlayVisualEffectInternal(visualEffectsEnum visualEffectsEnum, Vector3 position)
    {
        Debug.Log("Playing VFX internal" + visualEffectsEnum);
        if (visualEffects.ContainsKey(visualEffectsEnum))
        {
            Debug.Log("VFX found");
            VisualEffect visualEffect = Instantiate(visualEffects[visualEffectsEnum]); 
             
            visualEffect.transform.position = position;
            visualEffect.Play();
            StartCoroutine(WaitForVFXToEnd(visualEffect));
        }
        else
        {
            Debug.Log("no VFX ");
        }
    }
    private IEnumerator WaitForVFXToEnd(VisualEffect vfx)
    {
        yield return new WaitForSeconds(1f);
        // Wait until all particles are dead
        while (vfx.aliveParticleCount > 0)
        {
            yield return null;
        }

        Destroy(vfx.gameObject);
    }
}
public enum visualEffectsEnum
{
    death,
    minor,
    bridge,
    none
}