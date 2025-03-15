/// <summary>
/// Interface for objects that can be stepped on by the enemy
/// </summary>
internal interface IStepable
{
    public void CheckTrigger(BattleSceneTimeArgs args);
}