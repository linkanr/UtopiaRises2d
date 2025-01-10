using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
/// <summary>
/// Based on buidling but add functionality with the shoting building state machince and IcanShot interface
/// </summary>
public class ShootingBuilding : Building
{
    

    public Transform shotingPos;
    public ShotingBuildingStateMachine stateMachine;
    public TargeterForStaticObjects targeter;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ShotingBuildingStateMachine>();
        SetTimeLimiter();
       
    }

}
