using System;
using UnityEngine;
[Serializable]
public class PlayerGlobalVariables
{
    public readonly PlayerGlobalsMediator mediator = new();
    public readonly PoliticalSystem politicalSystem = new();
    public void AddModifier(BasicGlobalVariableModifier modifier)
    {
        mediator.AddModifier(modifier, this);
        Debug.Log("modifier added");
        BattleSceneActions.OnGlobalModifersChanged?.Invoke(modifier);
    }

    public void UpdateModifiers()
    {
        mediator.Update();
        BattleSceneActions.OnGlobalModifersChanged?.Invoke(null);
    }
    public void ClearBattleModifiers()
    {
        mediator.ClearBattleModifiers();
    }
    public PoliticalSystem PoliticalSystem => politicalSystem;
    public PoliticalAlignment GetPoliticalAlignment() => politicalSystem.GetAlignment();
    public IdeolgicalAlignment GetIdeologicalAlignment() => politicalSystem.GetIdeologicalAlignment();
    public PolicalCompassOrientation GetCompassOrientation() => politicalSystem.GetCompass();


    public int GetExtraLifetime(Faction faction )
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.ExtraLifetime, 0,  faction);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }

    public int GetExtraHeal(Faction faction)
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.ExtraHeal, 0,  faction);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }

    public int GetDamage(int baseDamage, Faction faction )
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.DamageModifier, baseDamage,  faction);
        mediator.PerformQuery(query);
        return Mathf.RoundToInt(query.FinalValue);
    }

    public float GetRange(float baseRange, Faction faction)
    {
        var query = new PlayerGlobalQuery(PlayerGlobalVarTypeEnum.RangeModifier, baseRange, faction);
        mediator.PerformQuery(query);
        return query.FinalValue;
    }


}
