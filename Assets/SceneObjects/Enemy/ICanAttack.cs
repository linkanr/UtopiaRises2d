using System;

public  interface ICanAttack
{
   
    public SceneObject attacker { get; set; }
    public Target target { get; set; }
    /// <summary>
    /// This is a function that gets called 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void RemoveTarget(object sender, IdamageAbleArgs e);
}