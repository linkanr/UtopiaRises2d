using System;
using UnityEngine;


public class EffectAdd
{
    public static void AddEffect(PickupTypes pickupType, SceneObject sceneObject, float duration)
    {
        Debug.Log("Checking for existing effect...");
        if (sceneObject.IsPickupActive(pickupType))
        {
            var activeModifier = sceneObject.GetActiveModifier(pickupType);
            if (activeModifier != null)
            {
                activeModifier.ProlongDuration(duration);
                Debug.Log($"Pickup {pickupType} is already active. Duration prolonged by {duration} seconds.");
                return;
            }
        }

        Debug.Log("Applying new effect...");
        StatPickupAddMulti pickup = pickupType switch
        {
            PickupTypes.Slow => new StatPickupAddMulti(
                StatsInfoTypeEnum.speed,
                0.5f,
                duration,
                StatPickupAddMulti.OperatorType.Multiply
            ),
            PickupTypes.Rage => new StatPickupAddMulti(
                StatsInfoTypeEnum.damageAmount,
                2.0f,
                duration,
                StatPickupAddMulti.OperatorType.Multiply
            ),
            _ => throw new ArgumentOutOfRangeException($"Unknown PickupType: {pickupType}")
        };

        pickup.ApplyPickupEffect(sceneObject);

        var modifier = pickup.GetModifier();
        sceneObject.AddPickup(pickupType, modifier);

        // Subscribe to the OnDispose event for cleanup
        modifier.OnDispose += _ =>
        {
            sceneObject.RemovePickup(pickupType);
            Debug.Log($"Pickup {pickupType} has expired.");
        };
    }
}



