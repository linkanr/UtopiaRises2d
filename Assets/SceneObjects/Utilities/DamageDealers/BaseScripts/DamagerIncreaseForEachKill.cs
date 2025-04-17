using System;
using UnityEngine;


public class DamagerIncreaseForEachKill : DamagerBaseClass
{
    public int additionPerKill;
    public int currentKillCount;
    public int maxKillCount;

    

    public override int CalculateDamageImplementation(int _baseDamage)
    {
        Debug.Log("Calculating damage based on kill count" + currentKillCount ) ;
        return currentKillCount * additionPerKill + _baseDamage;
    }
    public override float CalculateReloadTime()
    {
        return reloadTime;
    }

    public override void InitImplemantation(SceneObject sceneObject)
    {
        Debug.Log("Initimplimentation Damager increase per kill called");
        BattleSceneActions.OnSceneObjectKilled += OnEnemyDefeated;
    }

    private void OnEnemyDefeated(OnSceneObjectDestroyedArgs args)
    {
        Debug.Log("On enemy defeated called");
        if (args.killer == sceneObjectParent)
        {
            Debug.Log("On enemy defeated called and the killer is me");
            if (currentKillCount <= maxKillCount)
            {
                Debug.Log("On enemy defeated called and the killer is me and the kill count is " + currentKillCount);
                currentKillCount++;
            }
        }
    }
    public override DamagerBaseClass Clone()
    {
        return new DamagerIncreaseForEachKill
        {
            baseDamage = this.baseDamage,
            reloadTime = this.reloadTime,
            attackRange = this.attackRange,
            additionPerKill = this.additionPerKill,
            currentKillCount = 0, // start fresh
            maxKillCount = this.maxKillCount
        };
    }


}