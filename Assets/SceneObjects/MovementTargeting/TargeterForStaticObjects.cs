using System.Collections.Generic;
using UnityEngine;

public class TargeterForStaticObjects : TargeterBaseClass
{
    public SoSeekSystemBase seeker { get; set; }

    public override SoSeekSystemBase GetSeeker()
    {
        return seeker;
    }
    public void Initialize(SceneObject _attacker, SoSeekSystemBase _seeker, List<SceneObjectTypeEnum> _possibleTargetTypes, SoAttackSystem _soAttackSystem)
    {
       soAttackSystem = Instantiate( _soAttackSystem);
       possibleTargetTypes = _possibleTargetTypes;
       attacker = _attacker;
       seeker = Instantiate(_seeker);
    }

}