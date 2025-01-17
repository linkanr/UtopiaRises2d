using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary>
/// Mediates the interaction between different stats modifiers and queries.
/// </summary>
public class StatsMediator
{
    public readonly LinkedList<StatsModifier> modifiers = new LinkedList<StatsModifier>();

    /// <summary>
    /// Event triggered when a query is performed.
    /// </summary>
    public event EventHandler<Query> queries;

    /// <summary>
    /// Performs a query by invoking the queries event.
    /// </summary>
    /// <param name="sender">The sender of the query.</param>
    /// <param name="query">The query to be performed.</param>
    public void PerformQuery(object sender, Query query) => queries?.Invoke(sender, query);

    /// <summary>
    /// Adds a stats modifier to the mediator.
    /// </summary>
    /// <param name="modifier">The stats modifier to add.</param>
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

    /// <summary>
    /// Updates the mediator by disposing of any modifiers marked for removal.
    /// </summary>
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
            node = nextNode;
        }
    }
}
/// <summary>
/// Represents a query for stats information.
/// </summary>
public class Query
{
    /// <summary>
    /// The type of stats information being queried.
    /// </summary>
    public readonly StatsInfoTypeEnum statsType;

    /// <summary>
    /// The value associated with the query.
    /// </summary>
    public float value;

    /// <summary>
    /// Initializes a new instance of the Query class.
    /// </summary>
    /// <param name="_statsType">The type of stats information being queried.</param>
    /// <param name="_value">The value associated with the query.</param>
    public Query(StatsInfoTypeEnum _statsType, float _value)
    {
        statsType = _statsType;
        value = _value;
    }
}
