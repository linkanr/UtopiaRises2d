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
    private SceneObject sceneObject;
    TargeterBaseClass targetSystem;

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


    public void  Init(Seeker _seeker, TargeterBaseClass _targetsystem)
    {
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        seeker = _seeker;
        aIPath = gameObject.GetComponent<AIPath>();
        //aIPath = gameObject.AddComponent<AIPath>();
        aIPath.orientation = OrientationMode.YAxisForward;
        aIPath.gravity = new Vector3 (0f,0f,0f);
        sceneObject = _targetsystem.attacker;
        targetSystem = _targetsystem;

        aIPath.maxSpeed = sceneObject.GetStats().speed;
        aIPath.canMove = true;
        stateAllowsMovement = true;
        movementBlocked = false;

        targetSystem.OnTargetChanged += OnTargetChanged;
        TimeActions.OnPause += HandlePause;
        TimeActions.GlobalTimeChanged += MoveUpdate;

    }
    private void OnDestroy()
    {
        targetSystem.OnTargetChanged -= OnTargetChanged;
        TimeActions.OnPause -= HandlePause;
        TimeActions.GlobalTimeChanged -= MoveUpdate;
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
    private void MoveUpdate(BattleSceneTimeArgs args)
    {
        if (sceneObject == null)
        {
            Debug.LogError("sceneObject is null");
            return;
        }

        var stats = sceneObject.GetStats();
        if (stats == null)
        {
            Debug.LogError("sceneObject.GetStats() returned null");
            return;
        }

        var gridCell = GridCellManager.instance.gridConstrution.GetCellByWorldPosition(sceneObject.transform.position);
        if (gridCell == null)
        {
            Debug.LogError("GridCellManager.Instance.gridConstrution.GetCellByWorldPostion returned null");
            return;
        }

        var walkPenalty = gridCell.GetWalkPenalty();
        aIPath.maxSpeed = stats.speed * walkPenalty;
    }


}