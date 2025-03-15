public class SpawningBuildingStateMachine : BaseStateMachine<SpawningBuildingStateMachine>
{
    public SpawningBuilding spawningBuilding;
    protected override void Init()
    {
        SpawningBuilding spawningBuilding = GetComponent<SpawningBuilding>();
    }

}