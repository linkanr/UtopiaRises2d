using System;
using UnityEngine;


public class PickupEffectAdd
{
    public static void AddEffect(PickupTypes pickupType, SceneObject sceneObject, float duration)
    {
        Debug.Log("Checking for existing effect...");
        if (sceneObject.GetStatsHandler().IsPickupActive(pickupType))
        {
            var activeModifier = sceneObject.GetStatsHandler().GetActiveModifier(pickupType);
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
                StatPickupAddMulti.OperatorType.Multiply,
                pickupType
            ),
            PickupTypes.Rage => new StatPickupAddMulti(
                StatsInfoTypeEnum.damageAmount,
                2.0f,
                duration,
                StatPickupAddMulti.OperatorType.Multiply,pickupType
            ),
            PickupTypes.Weak => new StatPickupAddMulti(
                StatsInfoTypeEnum.takesDamageMultiplier,
                2f,
                duration,
                StatPickupAddMulti.OperatorType.Multiply, pickupType
            ),
            _ => throw new ArgumentOutOfRangeException($"Unknown PickupType: {pickupType}")
        };

        pickup.ApplyPickupEffect(sceneObject);

        var modifier = pickup.GetModifier();
        sceneObject.GetStatsHandler().AddPickup(pickupType, modifier);

        // Subscribe to the OnDispose event for cleanup
        modifier.OnDispose += _ =>
        {
            sceneObject.GetStatsHandler().RemovePickup(pickupType);
            Debug.Log($"Pickup {pickupType} has expired.");
        };
    }
}



