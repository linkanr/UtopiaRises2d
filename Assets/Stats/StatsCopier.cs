using System.Collections.Generic;

public static class StatsCopier
{
    /// <summary>
    /// Creates an exact copy of the given Stats object, including its modifiers.
    /// </summary>
    /// <param name="originalStats">The original Stats object to copy.</param>
    /// <returns>A new Stats object that is an exact copy of the original.</returns>
    public static Stats CopyStats(Stats originalStats)
    {
        Stats newStats = new Stats();

        // Copy all values from the original statsInfoDic to the new one
        foreach (KeyValuePair<StatsInfoTypeEnum, object> entry in originalStats.statsInfoDic)
        {
            newStats.statsInfoDic.Add(entry.Key, entry.Value);
        }

        // Copy the stats mediator
        newStats.statsMediator = originalStats.statsMediator;
        // Copy any active modifiers
     /*   var modifiers = new List<StatsModifier>(originalStats.statsMediator.modifiers);
        foreach (var modifier in modifiers)
        {
            newStats.statsMediator.AddModifier(modifier);
        }
    */
        return newStats;
    }
}
