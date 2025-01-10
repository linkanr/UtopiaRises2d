internal interface IClickableObject
{
    public ClickableType GetClickableType();    
}
public enum ClickableType
{
    card,
    SceneObject,
    notFound
}