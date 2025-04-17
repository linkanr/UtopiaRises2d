using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectStatsHandler
{
    private readonly SceneObject parentObject;
    private readonly Dictionary<PickupTypes, StatsModifier> activeModifiers = new Dictionary<PickupTypes, StatsModifier>();
    private Stats stats;
    private EffectSpriteOrganizer effectSpriteOrganizer;

    public SceneObjectStatsHandler(SceneObject parentObject)
    {
        this.parentObject = parentObject;
    }

    // Stats Management
    public void SetStats(Stats newStats)
    {
        stats = newStats;
        stats.Add(StatsInfoTypeEnum.sceneObjectsTransform, parentObject.transform);
    }
    public void SetEffectSpriteOrganizer(EffectSpriteOrganizer organizer)
    {
        effectSpriteOrganizer = organizer;
        effectSpriteOrganizer.Init(parentObject.transform, stats.statsMediator);
    }
    public Stats GetStats()
    {
        return stats;
    }

    // Pickup Management
    public bool IsPickupActive(PickupTypes pickupType)
    {
        return activeModifiers.ContainsKey(pickupType);
    }

    public void AddPickup(PickupTypes pickupType, StatsModifier modifier)
    {
        activeModifiers[pickupType] = modifier;

    }

    public void RemovePickup(PickupTypes pickupType)
    {

        activeModifiers.Remove(pickupType);
    }

    public StatsModifier GetActiveModifier(PickupTypes pickupType)
    {
        return activeModifiers.TryGetValue(pickupType, out var modifier) ? modifier : null;
    }
}
