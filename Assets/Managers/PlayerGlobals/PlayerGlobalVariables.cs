using UnityEngine;

public class PlayerGlobalVariables
{
    private readonly PlayerGlobalsMediator mediator = new();
    private readonly PoliticalSystem politicalSystem = new();
    public void AddModifier(GlobalVariableModifier modifier)
    {
        mediator.AddModifier(modifier, this);
        Debug.Log("modifier added");
        BattleSceneActions.OnGlobalModifierAdded(modifier);
    }

    public void UpdateModifiers()
    {
        mediator.Update();
        BattleSceneActions.OnGlobalModifierAdded(null);
    }
    public void ClearBattleModifiers()
    {
        mediator.ClearBattleModifiers();
    }
    public PoliticalSystem PoliticalSystem => politicalSystem;
    public PoliticalAlignment GetPoliticalAlignment() => politicalSystem.GetAlignment();
    public IdeolgicalAlignment GetIdeologicalAlignment() => politicalSystem.GetIdeologicalAlignment();
    public PolicalCompassOrientation GetCompassOrientation() => politicalSystem.GetCompass();



    public int GetExtraLifetime()
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.ExtraLifetime, 0);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }

    public int GetExtraHeal()
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.ExtraHeal, 0);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }

    /// <summary>
    /// Applies global modifiers (additive then multiplicative) to a base damage value.
    /// </summary>
    /// <param name="baseDamage">The base damage before modifiers.</param>
    /// <returns>The final modified damage.</returns>
    public int GetDamage(int baseDamage)
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.DamageModifier, baseDamage);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }
    public float GetRange(float baseRange)
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.RangeModifier, baseRange);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }
}
