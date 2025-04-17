using System;

public class PlayerGlobalQuery
{
    public PlayerGlobalVarTypeEnum type;
    public float baseValue;

    public float addBuffer = 0f;
    public float multiplyBuffer = 1f;
    public PoliticalAlignment politicalAlignment;
    public Faction faction; 

    public float FinalValue => (baseValue + addBuffer) * multiplyBuffer;

    public PlayerGlobalQuery(PlayerGlobalVarTypeEnum type, float baseValue, Faction faction = null)
    {
        if (faction != null && politicalAlignment != null)
            throw new ArgumentException("Query cannot have both faction and political alignment.");
        this.type = type;
        this.baseValue = baseValue;
        this.faction = faction;
        this.politicalAlignment = faction.politicalAlignment;
    }

}
