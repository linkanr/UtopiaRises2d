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
    private bool stateAllowsMovement;
    private bool movementBlocked;   
    public void StateAllowsMovement(bool _blocked)
    {
        stateAllowsMovement = _blocked;
        OnMovementBlockersChanged();
    }
    private void MovementBlocked (bool _blocked)
    {
        movementBlocked = _blocked;
        OnMovementBlockersChanged();

    }


    public void  Init(Seeker _seeker, TargeterBaseClass targetsystem)
    {
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        seeker = _seeker;
        aIPath = gameObject.GetComponent<AIPath>();
        //aIPath = gameObject.AddComponent<AIPath>();
        aIPath.orientation = OrientationMode.YAxisForward;
        aIPath.gravity = new Vector3 (0f,0f,0f);
        Debug.Log(targetsystem + "is not null");
        Debug.Log(targetsystem.attacker + "is not null");
        Debug.Log(targetsystem.attacker.GetStats() + " is not null");
        stats = targetsystem.attacker.GetStats();
        aIPath.maxSpeed = stats.speed;
        aIPath.canMove = true;

        targetsystem.OnTargetChanged += OnTargetChanged;
        TimeActions.OnPause += HandlePause;
        stateAllowsMovement = true;
        movementBlocked = false;

    }

    private void OnMovementBlockersChanged()
    {
        TryToMove();
    }
    private void OnTargetChanged(Target e)
    {

        destinationSetter.target= e.transform;
    }

    private void HandlePause(bool obj)
    {
        if (obj)
            MovementBlocked(true);
        else
            MovementBlocked(false);

    }
    public void TryToMove()
    {
        if (stateAllowsMovement && !movementBlocked)
            aIPath.canMove = true;
        else
            aIPath.canMove = false;
    }
    private void Update()
    {
        aIPath.maxSpeed = stats.speed;
    }
}