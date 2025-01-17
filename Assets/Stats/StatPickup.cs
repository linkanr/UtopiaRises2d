public abstract class StatPickup
{
    public abstract void ApplyPickupEffect(SceneObject sceneObject);
    public PickupTypes pickupType { get; protected set; }
}
