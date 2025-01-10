using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class StatsMediator
{
    readonly LinkedList<StatsModifier> modifiers = new LinkedList<StatsModifier>();
    public event EventHandler<Query> queries;
    public void PerformQuery(object sender, Query query) => queries?.Invoke(sender, query);
    public void AddModifier(StatsModifier modifier)
    {
        modifiers.AddLast(modifier);
        queries += modifier.Handle;
        modifier.OnDispose += _ =>
        {
            modifiers.Remove(modifier);
            queries -= modifier.Handle;
        };
    }
    public void Update()
    {
        var node = modifiers.First;
        while (node != null) 
        {
            var nextNode = node.Next;
            if (node.Value.markedForRemoval) 
            {
                node.Value.Dispose();
            }
        }
    }
}
public class Query
{
    public readonly StatsInfoTypeEnum statsType;
    public float value;
    public Query (StatsInfoTypeEnum _statsType, float _value)
    {
        statsType = _statsType;
        value = _value;
    }


}
