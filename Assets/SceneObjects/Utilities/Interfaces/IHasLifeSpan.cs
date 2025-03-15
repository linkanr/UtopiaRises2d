/// <summary>
/// This adds the ability to have a limited life spann, the corresponding SO needs to add float lifetime
/// </summary>
public interface IHasLifeSpan
{
    public TimeStruct getBirthLifeSpan();//Return the span from the scene object
    /// <summary>
    /// this is the initialization, shoudl contain add component and reference for the timeLimiter object
    /// </summary>

    public void OnLifeUp(); 
    public TimeTickerForIHasLifeSpan GetTimeLimiter(); // Returns the timelimiter set up in settimelimiter

}