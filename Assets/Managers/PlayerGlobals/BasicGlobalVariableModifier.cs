using System;
using UnityEngine;

[Serializable]
public class BasicGlobalVariableModifier : GlobalVariableModifier
{
    public PlayerGlobalVarTypeEnum varType;
    public GlobalModificationType modType;
    public float amount;
    public float duration; // Store this for cloning
    public ModifierLifetime lifetime;

    // Empty constructor for serialization (e.g., inside ScriptableObjects)
    public BasicGlobalVariableModifier() : base(ModifierLifetime.Permanent) { }

    public BasicGlobalVariableModifier(
        PlayerGlobalVarTypeEnum varType,
        GlobalModificationType modType,
        float amount,
        ModifierLifetime lifetime = ModifierLifetime.Permanent,
        float duration = 0f)
        : base(lifetime, duration)
    {
        this.varType = varType;
        this.modType = modType;
        this.amount = amount;
        this.lifetime = lifetime;
        this.duration = duration;
   
    }

    public override void Handle(object sender, PlayerGlobalQuery query)
    {
        if (query.type != varType) return;

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

    /// <summary>
    /// Creates a copy of this modifier to be safely used at runtime.
    /// </summary>
    public BasicGlobalVariableModifier Clone()
    {
        return new BasicGlobalVariableModifier(
            varType,
            modType,
            amount,
            lifetime,
            duration
        );
    }
}
