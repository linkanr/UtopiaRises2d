public interface IHasLifeSpan
{
    public TimeStruct getBirthLifeSpan();//Return the span from the scene object
    /// <summary>
    /// this is the initialization, shoudl contain add component and reference for the timeLimiter object
    /// </summary>
    public void SetTimeLimiter(); // this should just add component, set up ref for GetTimeLimiter() and INIT()
/// <summary>
/// This is the death call
/// </summary>
    public void OnLifeUp(); 
    public TimeLimterSceneObject GetTimeLimiter(); // Returns the timelimiter set up in settimelimiter

}