using System;



public class StatPickupAddMulti : StatPickup
{
    public enum OperatorType { Add, Multiply }
    public OperatorType operatorType;
    public StatsInfoTypeEnum statType;
    float statsModifierValue;
    float statsModifierDuration;

    // Reference to the created StatsModifier
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
        // Create a BasicStatModifier based on the operator type
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

        // Apply the modifier to the SceneObject's stats mediator
        sceneObject.GetStats().statsMediator.AddModifier(createdModifier);
    }

    // Expose the OnDispose event of the created StatsModifier
    public event Action<StatsModifier> OnDispose
    {
        add => createdModifier.OnDispose += value;
        remove => createdModifier.OnDispose -= value;
    }
}
