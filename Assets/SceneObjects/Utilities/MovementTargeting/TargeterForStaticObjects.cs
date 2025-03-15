
using System.Collections.Generic;

public class TargeterForStaticObjects : TargeterBaseClass
{
    public SoSeekSystemBase seeker { get; set; }

    public override SoSeekSystemBase GetSeeker()
    {
        return seeker;
    }


    public override void Initialize(SceneObject sceneObject, SoSeekSystemBase SeekSystem, List<SceneObjectTypeEnum> _possibleTargetTypes, SoAttackSystem attackSystem, Mover mover = null)
    {
        soAttackSystem = Instantiate(attackSystem);
        possibleTargetTypes = _possibleTargetTypes;
        attacker = sceneObject;
        seeker = Instantiate(SeekSystem);
    }
}