using UnityEngine;

/// <summary>
/// Represents a shooting building in the game, inheriting from Building and implementing IcanAttack interface.
/// </summary>
public class SceneObjectShootingBuilding : SceneObjectBuilding, IcanAttack
{
    /// <summary>
    /// The position from where the building will shoot.
    /// </summary>
    public Transform shotingPos;

    /// <summary>
    /// The state machine managing the shooting building's states.
    /// </summary>
    public ShotingBuildingStateMachine stateMachine;

    /// <summary>
    /// The targeter component responsible for seeking and attacking targets.
    /// </summary>
    public TargeterBaseClass targeter { get; set; }

    protected override void AddStatsForClick(Stats _stats)
    {
        
    }



    /// <summary>
    /// Initializes the shooting building, setting up the state machine and time limiter.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ShotingBuildingStateMachine>();
      
    }

}
