using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EffectSpriteOrganizer : SerializedMonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new(); // Replace array
    private readonly Queue<Sprite> spriteQueue = new();
    private Dictionary<PickupTypes, Sprite> spriteEffects;
    private Transform baseTransform;
    private StatsMediator statsMediator;

    public void Init(Transform parent, StatsMediator mediator)
    {
        spriteEffects = new Dictionary<PickupTypes, Sprite>();

        // Load all sprites from the layered PSD (named Effects.psd)
        Sprite[] allSprites = Resources.LoadAll<Sprite>("SpriteEffects/Effects");
        foreach (var sprite in allSprites)
        {
            Debug.Log($"Loaded Sprite: {sprite.name}");
            if (Enum.TryParse(sprite.name, out PickupTypes type))
            {
                spriteEffects[type] = sprite;
            }
        }

        baseTransform = parent;
        statsMediator = mediator;
        statsMediator.onModifierAdded += OnModifiersUpdated;
        OnModifiersUpdated(); // Initial load
    }

    private void OnDestroy()
    {
        if (statsMediator != null)
            statsMediator.onModifierAdded -= OnModifiersUpdated;
    }

    private void Update()
    {
        if (baseTransform == null) return;
        transform.position = baseTransform.position + Vector3.up;
        transform.localRotation = Quaternion.Inverse(baseTransform.rotation);
    }

    private void OnModifiersUpdated()
    {
        if (statsMediator == null) return;

        spriteQueue.Clear();
        foreach (var mod in statsMediator.modifiers)
        {
            if (spriteEffects.TryGetValue(mod.pickupTypes, out var sprite))
                spriteQueue.Enqueue(sprite);
        }

        UpdateRenderers();
    }

    private void UpdateRenderers()
    {
        while (spriteRenderers.Count < spriteQueue.Count)
        {
            GameObject go = new GameObject("EffectSpriteRenderer");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.transform.SetParent(transform);
            spriteRenderers.Add(sr);
        }

        Sprite[] sprites = spriteQueue.ToArray();
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            if (i < sprites.Length)
            {
                spriteRenderers[i].sprite = sprites[i];
                spriteRenderers[i].enabled = true;
            }
            else
            {
                spriteRenderers[i].sprite = null;
                spriteRenderers[i].enabled = false;
            }
        }
    }
}
