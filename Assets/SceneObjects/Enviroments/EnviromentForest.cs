using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentForest : EnviromentObject,IDamageAble
{
    public ForestEnum forestState;
    public float age;


    public SceneObjectTypeEnum damageableType => SceneObjectTypeEnum.enviromentObject;

    public IdamagableComponent idamageableComponent{ get; set; }










    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.health,idamageableComponent.healthSystem.health);

    }


}

public enum ForestEnum
{
    sapline,
    tree,
    largeTree
}