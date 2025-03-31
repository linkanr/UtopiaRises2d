public class DamagerIncreaseForEachKill : DamagerBaseClass
{
    public int additionPerKill;
    private int currentKillCount;
    public int maxKillCount;

    public override float CalculateAttackRange()
    {
        return attackRange;
    }
    public override int CalculateDamageImplementation(int _baseDamage)
    {
        return currentKillCount * additionPerKill + _baseDamage;
    }
    public override float CalculateReloadTime()
    {
        return reloadTime;
    }
}