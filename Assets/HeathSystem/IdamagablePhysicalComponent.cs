using System;
using UnityEngine;
/// <summary>
/// Adds functionality to be able to take damage, The corresponding SO needs to have health
/// </summary>
public interface IdamagablePhysicalComponent:IDamagableComponent
{
    public PhysicalHealthSystem healthSystem { get; }





    
}

public class IdamageAbleArgs
{
    public IDamagableComponent damageable;

}
[System.Serializable]
public enum SceneObjectTypeEnum
{
    playerbuilding,
    follower,
    enemy,
    enemyBase,
    playerBase,
    enviromentObject,
    playerConstructionBase,
    all,
    minorObject,
    defensiveStructure

}