using System.Collections.Generic;
using UnityEngine;

public static class GameStringFormatter
{
    // ---------------- Helpers ----------------

    private static string FormatSpriteTag(string spriteName, string label, string colorHex,bool includeSprite = false,  bool noBreak = false)
    {
        string space = noBreak ? "\u00A0" : " ";

        string iconTag = $"<sprite name=\"{spriteName}\">";

        if (includeSprite)
        {
            return $"<color=#{colorHex}><sprite name=\"{spriteName}\">{space}{label}</color>";
        }
        else
        {
            return $"<color=#{colorHex}>{label}</color>";
        }
    }

    public static string FormatSprituality(sprituality value, bool includeSprite = true)
    {
        string colorHex = value switch
        {
            sprituality.Spiritual => "9370DB",         // MediumPurple
            sprituality.Materialistic => "B22222",     // Firebrick
            sprituality.Agnostic => "CCCCCC",
            _ => "FFFFFF"
        };

        return FormatSpriteTag($"sprituality_{value}", value.ToString(), colorHex, includeSprite);
    }

    // ---------------- Factions ----------------

    public static string FormatFaction(FactionsEnums factionEnum)
    {
        Faction faction = FactionLookup.instance.GetFaction(factionEnum);
        if (faction == null)
            return factionEnum.ToString();

        string colorHex = ColorUtility.ToHtmlStringRGB(faction.color);
        return FormatSpriteTag($"faction_{factionEnum}", factionEnum.ToString(), colorHex, includeSprite: true);
    }

    // ---------------- Rarity ----------------

    public static string FormatRarity(CardRareEnums rarity)
    {
        string colorHex = GetRarityColor(rarity);
        return FormatSpriteTag($"rarity_{rarity}", rarity.ToString(), colorHex, includeSprite:false);
    }

    private static string GetRarityColor(CardRareEnums rarity)
    {
        return rarity switch
        {
            CardRareEnums.Common => "AAAAAA",
            CardRareEnums.Uncommon => "007BFF",
            CardRareEnums.Rare => "FFD700",
            _ => "FFFFFF"
        };
    }

    // ---------------- Dogmas ----------------

    public static string FormatDogma(string dogmaId)
    {
        SoDogmaBase dogma = DogmaManager.instance.GetDogmaBase(dogmaId);
        if (dogma == null)
            return dogmaId;

        string colorHex = ColorUtility.ToHtmlStringRGB(SoDogmaBase.displayColor);
        return FormatSpriteTag($"dogma_{dogmaId}", dogma.displayName, colorHex, includeSprite: false);
    }

    // ---------------- Ideological Alignments ----------------

    public static string FormatIdeology(IdeolgicalAlignment alignment)
    {
        var so = IdeologyLookup.instance.GetIdeology(alignment);
        if (so == null)
            return alignment.ToString();

        string colorHex = ColorUtility.ToHtmlStringRGB(so.displayColor);
        return FormatSpriteTag(so.alignment.ToString(), so.alignment.ToString(), colorHex, includeSprite: true);
    }

    // ---------------- Political Axis Tags ----------------

    public static string FormatPoliticalAxisTag(PoliticalAxisTag tag)
    {
        var so = IdeologyLookup.instance.GetAxisTag(tag);
        if (so == null)
            return tag.ToString();

        string colorHex = ColorUtility.ToHtmlStringRGB(so.displayColor);
        return FormatSpriteTag(so.tag.ToString(), so.tag.ToString(), colorHex, includeSprite: true);
    }
}
