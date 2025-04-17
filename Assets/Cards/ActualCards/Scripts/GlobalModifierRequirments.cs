using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class GlobalModifierRequirement
{
    public enum RequirementType
    {
        None,
        Faction,
        PoliticalAlignment
    }

    public RequirementType requirementType;

    [ShowIf("requirementType", RequirementType.Faction)]
    public Faction faction;

    [ShowIf("requirementType", RequirementType.PoliticalAlignment)]
    public bool requiresGalTan;
    [ShowIf("@requirementType == RequirementType.PoliticalAlignment && requiresGalTan")]
    public galTan galTan;

    [ShowIf("requirementType", RequirementType.PoliticalAlignment)]
    public bool requiresLeftRight;
    [ShowIf("@requirementType == RequirementType.PoliticalAlignment && requiresLeftRight")]
    public leftRigt leftRight;

    [ShowIf("requirementType", RequirementType.PoliticalAlignment)]
    public bool requiresSprituality;
    [ShowIf("@requirementType == RequirementType.PoliticalAlignment && requiresSprituality")]
    public sprituality sprituality;

    public bool MatchesQuery(PlayerGlobalQuery query)
    {
        switch (requirementType)
        {
            case RequirementType.None:
                return true;

            case RequirementType.Faction:
                return query.faction == faction;

            case RequirementType.PoliticalAlignment:
                if (query.politicalAlignment == null)
                {
                    Debug.LogError("Query has no political alignment to match against");
                    return false;
                }

                bool anyRequirementSet = requiresGalTan || requiresLeftRight || requiresSprituality;
                if (!anyRequirementSet)
                {
                    Debug.LogError("Political alignment requirement selected, but no axis requirement was set (galTan, leftRight, or sprituality)");
                    return false;
                }

                if (requiresGalTan && query.politicalAlignment.galTan != galTan) return false;
                if (requiresLeftRight && query.politicalAlignment.leftRight != leftRight) return false;
                if (requiresSprituality && query.politicalAlignment.sprituality != sprituality) return false;

                return true;


            default:
                return false;
        }
    }


    public Faction GetFaction() => requirementType == RequirementType.Faction ? faction : null;
}
