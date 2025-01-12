using System;



public class StatPickupAddMulti : StatPickup
{
    public enum OperatorType { Add, Multiply }
    public OperatorType operatorType;
    public StatsInfoTypeEnum statType;
    private float statsModifierValue;
    private float statsModifierDuration;
    private StatsModifier createdModifier;

    public StatPickupAddMulti(StatsInfoTypeEnum statType, float value, float duration, OperatorType operatorType)
    {
        this.statType = statType;
        this.statsModifierValue = value;
        this.statsModifierDuration = duration;
        this.operatorType = operatorType;
    }

    public override void ApplyPickupEffect(SceneObject sceneObject)
    {
        createdModifier = operatorType switch
        {
            OperatorType.Add => new BasicStatModifier(
                statType,
                statsModifierDuration,
                v => v + statsModifierValue
            ),
            OperatorType.Multiply => new BasicStatModifier(
                statType,
                statsModifierDuration,
                v => v * statsModifierValue
            ),
            _ => throw new ArgumentOutOfRangeException()
        };

        sceneObject.GetStats().statsMediator.AddModifier(createdModifier);
    }

    // Expose the created StatsModifier
    public StatsModifier GetModifier()
    {
        return createdModifier;
    }
}


