using Pathfinding;
using System;
using UnityEngine;
/// <summary>
/// Handles the movment and speed. 
/// </summary>
public class Mover :MonoBehaviour, IMoverComponent
{
    
    public AIDestinationSetter destinationSetter { get; set; }
    public Seeker seeker { get; set; }
    public AIPath aIPath { get; set; }
    private Stats stats;


    public void  Init(Seeker _seeker, TargeterBaseClass targetsystem)
    {
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        seeker = _seeker;
        aIPath = gameObject.GetComponent<AIPath>();
        //aIPath = gameObject.AddComponent<AIPath>();
        aIPath.orientation = OrientationMode.YAxisForward;
        aIPath.gravity = new Vector3 (0f,0f,0f);
       
        stats = targetsystem.attacker.GetStats();
        aIPath.maxSpeed = stats.speed;

        targetsystem.OnTargetChanged += OnTargetChanged;
        BattleSceneActions.OnPause += HandlePause;
       
    }

    private void OnTargetChanged(Target e)
    {

        destinationSetter.target= e.transform;
    }

    private void HandlePause(bool obj)
    {
        if (obj)
            Move(false);
        else
            Move(true);

    }
    public void Move(bool move)
    {
        if (move)
            aIPath.canMove = true;
        else
            aIPath.canMove = false;
    }
    private void Update()
    {
        aIPath.maxSpeed = stats.speed;
    }
}