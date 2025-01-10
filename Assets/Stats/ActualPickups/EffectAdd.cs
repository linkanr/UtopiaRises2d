using System;
using UnityEngine;

public class EffectAdd
{
    public static void AddEffect(PickupTypes pickupType, SceneObject sceneObject, float duration)
    {
        Debug.Log("actualEffect");
        // Check if the pickup type is already active
        if (sceneObject.IsPickupActive(pickupType))
        {
            Debug.Log($"Pickup {pickupType} is already active. Effect not applied.");
            return;
        }

        // Create and apply the corresponding StatPickupAddMulti
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

        Debug.Log(" Apply the effect and track the pickup");
        pickup.ApplyPickupEffect(sceneObject);
        sceneObject.AddPickup(pickupType);

        // Subscribe to the OnDispose event for cleanup
        pickup.OnDispose += _ =>
        {
            sceneObject.RemovePickup(pickupType);
            Debug.Log($"Pickup {pickupType} has expired.");
        };
    }
}
