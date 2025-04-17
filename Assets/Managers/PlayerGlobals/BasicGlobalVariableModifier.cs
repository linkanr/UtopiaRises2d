using Sirenix.OdinInspector;
using System;

[Serializable]
public class BasicGlobalVariableModifier : GlobalVariableModifier
{
    public PlayerGlobalVarTypeEnum varType;
    public GlobalModificationType modType; // Add or Multiply
    public float amount;
    public ModifierLifetime lifetime;
    [EnableIf("@this.lifetime == ModifierLifetime.Timed")]
    public float duration;

    public GlobalModifierRequirement requirement;
    public bool isDogma;

    public BasicGlobalVariableModifier() : base(ModifierLifetime.Permanent) { }

    public BasicGlobalVariableModifier(
        PlayerGlobalVarTypeEnum varType,
        GlobalModificationType modType,
        float amount,
        ModifierLifetime lifetime = ModifierLifetime.Permanent,
        float duration = 0f,
        GlobalModifierRequirement requirement = null,
        bool isDogma = false)
        : base(lifetime, duration)
    {
        this.varType = varType;
        this.modType = modType;
        this.amount = amount;
        this.lifetime = lifetime;
        this.duration = duration;
        this.requirement = requirement;
        this.isDogma = isDogma;
    }

    public override void Handle(object sender, PlayerGlobalQuery query)
    {
        if (query.type != varType) return;
        if (requirement != null && !requirement.MatchesQuery(query)) return;

        switch (modType)
        {
            case GlobalModificationType.Add:
                query.addBuffer += amount;
                break;

            case GlobalModificationType.Multiply:
                query.multiplyBuffer *= amount;
                break;
        }
    }

    public BasicGlobalVariableModifier Clone()
    {
        return new BasicGlobalVariableModifier(
            varType,
            modType,
            amount,
            lifetime,
            duration,
            requirement,
            isDogma
        );
    }
}
