using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentForest : EnviromentObject
{
    public ForestEnum forestState;
    public float age;


    public SceneObjectTypeEnum damageableType => SceneObjectTypeEnum.enviromentObject;

  










    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.health,healthSystem.GetHealth());

    }


}

public enum ForestEnum
{
    sapline,
    tree,
    largeTree
}