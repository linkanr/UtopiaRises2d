public class ShotingBuildingStateMachine : BaseStateMachine<ShotingBuildingStateMachine>
{
    public SceneObjectShootingBuilding shootingBuilding;

    protected override void Init()
    {
        shootingBuilding = GetComponent<SceneObjectShootingBuilding>();
    }
}