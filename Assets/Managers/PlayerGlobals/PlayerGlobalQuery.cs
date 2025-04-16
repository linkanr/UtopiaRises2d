public class PlayerGlobalQuery
{
    public PlayerGlobalVarTypeEnum type;
    public float baseValue;

    public float addBuffer = 0f;
    public float multiplyBuffer = 1f;

    public float FinalValue => (baseValue + addBuffer) * multiplyBuffer;

    public PlayerGlobalQuery(PlayerGlobalVarTypeEnum type, float baseValue)
    {
        this.type = type;
        this.baseValue = baseValue;
    }
}
