using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.VFX;
/// <summary>
/// SoShoting building adds attacksystem to the building so and must correspond to a shoting building scene object
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/SoBuildnings/shotingBuildings")]
public class SoshotingBuilding : SoBuilding
{

    public SoAttackSystem attackSystem;
    public SoSeekSystemForBuildings seekSystemForBuildings;
    public List<SceneObjectTypeEnum> possibleTargetTypes;
    public DamagerBaseClass damagerBaseClass;
    public SoSceneObjectAnimationData soSceneObjectAnimationData;
    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        base.GetStatsInernal(_statsInforDic);
        DamagerBaseClass damagerInstance = damagerBaseClass.Clone();
        damagerBaseClass = damagerInstance;
        _statsInforDic.Add(StatsInfoTypeEnum.damager, damagerBaseClass);
        _statsInforDic.Add(StatsInfoTypeEnum.onClickDisplaySprite, attackSystem.displayRangeSprite);
        _statsInforDic.Add(StatsInfoTypeEnum.canTargetThefollowingSceneObjects, possibleTargetTypes);


        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        base.ObjectInitialization(sceneObject);
        Debug.Log("SoShotingBuildigng ObjectIntialization");
        damagerBaseClass.Init(sceneObject);
        SceneObjectShootingBuilding shootingBuilding = (SceneObjectShootingBuilding)sceneObject;
        shootingBuilding.targeter = shootingBuilding.AddComponent<TargeterForStaticObjects>();
        
        shootingBuilding.targeter.Initialize(shootingBuilding, seekSystemForBuildings, possibleTargetTypes, attackSystem);
        GameObject go = new GameObject("EnemyAnimator");
        sceneObject.objectAnimator = SceneObjectAnimator.Create(sceneObject);
        sceneObject.objectAnimator.Init(soSceneObjectAnimationData);



    }









}