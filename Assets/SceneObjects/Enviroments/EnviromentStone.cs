
using UnityEngine;

public class EnviromentStone : EnviromentObject
{


    public SceneObjectTypeEnum damageableType => SceneObjectTypeEnum.enviromentObject;

    public IdamagableComponent idamageableComponent { get; set; }










    protected override void AddStatsForClick(Stats _stats)
    {
        base.AddStatsForClick(_stats);
        _stats.Add(StatsInfoTypeEnum.health, idamageableComponent.healthSystem.health);

    }



}