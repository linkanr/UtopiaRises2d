using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;
using System.Linq;
using Pathfinding;

public class EffectSpriteOrganizer : SerializedMonoBehaviour
{
    public SpriteRenderer[] spriteRenderers; // Array of SpriteRenderers
    public Dictionary<PickupTypes, Sprite> spriteEffects;
    private Queue<Sprite> spriteQueue = new Queue<Sprite>(); // Queue to manage sprite order
    private Transform baseTransform;

    public void Init(Transform parent)
    {
        SpriteEffectDic spriteEffectDic = Resources.Load("SpriteEffectDic") as SpriteEffectDic;
        spriteEffects = spriteEffectDic.spriteEffects;
        foreach (SpriteRenderer s in spriteRenderers)
        {
            s.sprite = null;
        }
        baseTransform = parent;
    }

    private void Update()
    {
        transform.position = baseTransform.position + new Vector3(0, 1, 0);
        transform.localRotation = Quaternion.Inverse(baseTransform.rotation);
    }
    /// <summary>
    /// Adds a new sprite effect to the organizer.
    /// </summary>
    public void AddSpriteEffect(PickupTypes pickupType)
    {
        if (!spriteEffects.TryGetValue(pickupType, out Sprite newSprite))
        {
            Debug.LogWarning($"Sprite for pickup type {pickupType} not found.");
            return;
        }

        // Add the new sprite to the queue
        spriteQueue.Enqueue(newSprite);

        // If the queue exceeds the number of sprite renderers, remove the oldest sprite
        if (spriteQueue.Count > spriteRenderers.Length)
        {
            spriteQueue.Dequeue();
        }

        // Update the SpriteRenderers to reflect the current queue state
        UpdateSprites();
    }

    /// <summary>
    /// Updates the SpriteRenderers to match the queue state.
    /// </summary>
    private void UpdateSprites()
    {
        // Get all sprites from the queue as an array
        Sprite[] sprites = spriteQueue.ToArray();

        // Update each SpriteRenderer with the corresponding sprite
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (i < sprites.Length)
            {
                spriteRenderers[i].sprite = sprites[i];
                spriteRenderers[i].enabled = true; // Enable renderer if a sprite exists
            }
            else
            {
                spriteRenderers[i].sprite = null;
                spriteRenderers[i].enabled = false; // Disable renderer if no sprite exists
            }
        }
    }

    /// <summary>
    /// Removes a specific sprite effect if it exists in the queue.
    /// </summary>
    public void RemoveSpriteEffect(PickupTypes pickupType)
    {
        if (!spriteEffects.TryGetValue(pickupType, out Sprite targetSprite))
        {
            Debug.LogWarning($"Sprite for pickup type {pickupType} not found.");
            return;
        }

        // Rebuild the queue without the target sprite
        spriteQueue = new Queue<Sprite>(spriteQueue.Where(sprite => sprite != targetSprite));

        // Update the SpriteRenderers to reflect the modified queue state
        UpdateSprites();
    }
}
