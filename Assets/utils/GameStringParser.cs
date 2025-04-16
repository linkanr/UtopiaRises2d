using System.Text.RegularExpressions;

/// <summary>
/// =============================================================
/// GameStringParser Format Documentation
/// =============================================================
/// This parser replaces special markup tags in text with
/// formatted strings including icons and colors for TMP output.
///
/// ---------- Supported Tags ----------
///   @Faction.SomeFactionEnum
///   @Rarity.SomeRarityEnum
///   @Dogma.SomeDogmaId
///   @Ideology.SomeIdeologicalAlignment
///   @Ideology.SomePoliticalAxisTag (e.g. Left, Gal, Tan)
///   @Sprituality.SomeSpiritualAlignment
///
/// ---------- Sprite Setup ----------
/// All icons used in formatted strings rely on TextMeshPro's
/// sprite asset. Sprites must be added to the TMP sprite atlas
/// with the following naming convention:
///
/// Factions:
///   sprite name = "faction_{FactionEnum}"
///   e.g. faction_Amish
///
/// Rarities:
///   sprite name = "rarity_{RarityEnum}"
///   e.g. rarity_Epic
///
/// Dogmas:
///   sprite name = "dogma_{DogmaId}"
///   e.g. dogma_SwordsToPlowshare
///
/// Ideological Alignments:
///   sprite name = "{IdeolgicalAlignment}"
///   e.g. LibertarianLeft, Rigth, Centrist
///
/// Political Axis Tags:
///   sprite name = "{PoliticalAxisTag}"
///   e.g. Left, Tan, GalTanNeutral
///
/// Spiritual Alignments:
///   sprite name = "sprituality_{SpritualityEnum}"
///   e.g. sprituality_Spritual, sprituality_Materialistic
///
/// ---------- Notes ----------
/// - All SO references are loaded from Resources via their getters
/// - Icons will appear inline via <sprite name="...">
/// - Colors are defined in each ScriptableObject
/// </summary>

public static class GameStringParser
{
    private static readonly Regex tokenRegex = new(@"@(?<type>Faction|Dogma|Rarity|Ideology|Sprituality)\.(?<id>[a-zA-Z0-9_]+)", RegexOptions.Compiled);


    public static string Parse(string input)
    {
        return tokenRegex.Replace(input, match =>
        {
            string type = match.Groups["type"].Value;
            string id = match.Groups["id"].Value;

            return type switch
            {
                "Faction" => TryFormatFaction(id),
                "Dogma" => GameStringFormatter.FormatDogma(id),
                "Rarity" => TryFormatRarity(id),
                "Ideology" => TryFormatIdeology(id),
                "Sprituality" => TryFormatSprituality(id),
                _ => match.Value
            };

        });
    }

    private static string TryFormatFaction(string id)
    {
        if (System.Enum.TryParse(id, out FactionsEnums faction))
            return GameStringFormatter.FormatFaction(faction);
        return $"<color=red>INVALID FACTION: {id}</color>";
    }
    private static string TryFormatSprituality(string id)
    {
        if (System.Enum.TryParse(id, out sprituality value))
            return GameStringFormatter.FormatSprituality(value);

        return $"<color=red>INVALID SPRITUALITY: {id}</color>";
    }

    private static string TryFormatRarity(string id)
    {
        if (System.Enum.TryParse(id, out CardRareEnums rarity))
            return GameStringFormatter.FormatRarity(rarity);
        return $"<color=red>INVALID RARITY: {id}</color>";
    }

    private static string TryFormatIdeology(string id)
    {
        if (System.Enum.TryParse(id, out IdeolgicalAlignment ideology))
            return GameStringFormatter.FormatIdeology(ideology);

        if (System.Enum.TryParse(id, out PoliticalAxisTag axisTag))
            return GameStringFormatter.FormatPoliticalAxisTag(axisTag);

        return $"<color=red>INVALID IDEOLOGY: {id}</color>";
    }
}
