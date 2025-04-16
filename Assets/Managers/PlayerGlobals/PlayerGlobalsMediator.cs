using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerGlobalsMediator
{
    private readonly LinkedList<GlobalVariableModifier> modifiers = new();

    public event EventHandler<PlayerGlobalQuery> queries;
    
    
    public void AddModifier(GlobalVariableModifier modifier, PlayerGlobalVariables globals)
    {
        modifiers.AddLast(modifier);
        queries += modifier.Handle;
        modifier.OnDispose += mod =>
        {
            modifiers.Remove(mod);
            queries -= mod.Handle;
        };
    }
    public void ClearBattleModifiers()
    {
        foreach (var mod in modifiers.ToArray())
        {
            if (mod.Lifetime == ModifierLifetime.UntilEndOfBattle)
            {
                mod.Dispose(); // Triggers OnDispose and removes it
            }
        }
    }
    public void PerformQuery(PlayerGlobalQuery query)
    {
        queries?.Invoke(this, query);
    }

    public void Update()
    {
        foreach (var mod in modifiers.ToArray())
        {
            if (mod.MarkedForRemoval)
                mod.Dispose();
        }
    }
}
