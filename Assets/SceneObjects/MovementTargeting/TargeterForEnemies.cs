using Pathfinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// Takes responsibility for setting new target with multi path
/// </summary>
public class TargeterForEnemies : TargeterBaseClass
{
    public Mover mover;
    public SoSeekSytemForEnemies seeker;

    public override SoSeekSystemBase GetSeeker()
    {
        return seeker;
    }

    public void Initialize(Mover moverComponent, SoEnemyObject soEnemyInformationPackage, SceneObject attacker)
    {
        this.mover = moverComponent;
        this.soAttackSystem = Instantiate(soEnemyInformationPackage.attackSystem);
        this.possibleTargetTypes = soEnemyInformationPackage.possibleTargetTypes;
        this.seeker = Instantiate(soEnemyInformationPackage.seekSystem);
        this.attacker = attacker;
    }

    public override void Seek()
    {
        if (mover == null)
        {
            UnityEngine.Debug.LogError("mover is null");
        }
        else
        {
            UnityEngine.Debug.Log("mover is present");
        }
        seeker.Seek(attacker.transform.position, possibleTargetTypes, this, SeekStyle.findRoute, mover);
    }
    public void SetNewTarget(Path p)
    {
        var m = p as MultiTargetPath;

        SetNewTarget(SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(m.endPoint, sceneObjectTypeEnumsList: possibleTargetTypes, onlyDamageables: true));

    }


}