using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeologyLookup : MonoBehaviour
{
    private Dictionary<IdeolgicalAlignment, SoPoliticalAlignment> ideologyTag = new();
    private Dictionary<PoliticalAxisTag, SoPoliticalAxisTag> axisTag = new();
    public static IdeologyLookup instance { get; private set; }

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
        var ideology = Resources.LoadAll<SoPoliticalAlignment>("Ideology/Alignment/");
        var axis = Resources.LoadAll<SoPoliticalAxisTag>("Ideology/Axis/");
        foreach (var item in ideology)
        {
            if (item != null)
            {
                ideologyTag[item.alignment] = item;
            }
        }

        foreach (var item in axis)
        {
            if (item != null)
            {
                axisTag[item.tag] = item;
            }
        }
    }
    public SoPoliticalAlignment GetIdeology(IdeolgicalAlignment alignment)
    {
        if (ideologyTag.TryGetValue(alignment, out SoPoliticalAlignment ideology))
        {
            return ideology;
        }
        else
        {
            Debug.LogWarning($"Ideology {alignment} not found in cache.");
            return null;
        }
    }
    public SoPoliticalAxisTag GetAxisTag(PoliticalAxisTag tag)
    {
        if (axisTag.TryGetValue(tag, out SoPoliticalAxisTag axis))
        {
            return axis;
        }
        else
        {
            Debug.LogWarning($"Axis {tag} not found in cache.");
            return null;
        }
    }
}
