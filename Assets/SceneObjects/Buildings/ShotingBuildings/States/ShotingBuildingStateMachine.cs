public class ShotingBuildingStateMachine : BaseStateMachine<ShotingBuildingStateMachine>
{
    public ShootingBuilding shootingBuilding;

    protected override void Init()
    {
        shootingBuilding = GetComponent<ShootingBuilding>();
    }
}