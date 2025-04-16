using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionLookup : MonoBehaviour
{
    private Dictionary<FactionsEnums, Faction> factionCache = new();
    public static FactionLookup instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of FactionLookup found. Destroying this instance.");
            Destroy(gameObject);
        }
        var faction = Resources.LoadAll<Faction>("Factions/");
        foreach (var item in faction)
        {
            if (item != null)
            {
                factionCache[item.factionEnum] = item;
            }
        }

    }
    public Faction GetFaction(FactionsEnums factionEnum)
    {
        if (factionCache.TryGetValue(factionEnum, out Faction faction))
        {
            return faction;
        }
        else
        {
            Debug.LogWarning($"Faction {factionEnum} not found in cache.");
            return null;
        }
    }
}
