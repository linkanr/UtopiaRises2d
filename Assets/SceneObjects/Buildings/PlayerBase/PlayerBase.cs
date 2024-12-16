using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Building, IDamageable

{

    
    public override iDamageableTypeEnum damageableType  {get { return iDamageableTypeEnum.playerBase; } }



    protected override void Start()
    {
       base.Start();
       OnCreated();
    }

    protected override void OnObjectDestroyed()
    {
        
    }

    public override void OnCreated()
    {
        base.OnCreated();
        SetStats(CreateBaseStats());
 
    }

    private Stats CreateBaseStats()
    {
        Stats newStats = new Stats();
        newStats.Add(StatsInfoTypeEnum.name, "PlayerBase");
        newStats.Add(StatsInfoTypeEnum.description, "Your base");
        return newStats;
    }
}
