using Pathfinding;

public interface IMoverComponent
{
    

    public AIDestinationSetter destinationSetter { get; set; }
    public Seeker seeker { get; set; }
    public AIPath aIPath { get; set; }

   
}