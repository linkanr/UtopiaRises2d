using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyType")]
[System.Serializable]
public class SoEnemyObject:SoSceneObjectBase

{
    public int health;
    public SoAttackSystem attackSystem;
    public SoSeekSytemForEnemies seekSystem;
    public DamagerBaseClass damagerBaseClass;
    public SoSceneObjectAnimationData soSceneObjectAnimationData;
    public List<SceneObjectTypeEnum> possibleTargetTypes;
    public float speed;
    public SoDamageEffect soDamageEffect;



    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        _statsInforDic.Add(StatsInfoTypeEnum.health, health);
        DamagerBaseClass damagerInstance = damagerBaseClass.Clone();
        damagerBaseClass = damagerInstance;
        _statsInforDic.Add(StatsInfoTypeEnum.damager, damagerBaseClass);
        _statsInforDic.Add(StatsInfoTypeEnum.speed, speed);
        _statsInforDic.Add(StatsInfoTypeEnum.Sprite, sprite);

        _statsInforDic.Add(StatsInfoTypeEnum.seekSystem, seekSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.attackSystem, attackSystem);
        _statsInforDic.Add(StatsInfoTypeEnum.canTargetThefollowingSceneObjects, possibleTargetTypes);
        return _statsInforDic;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {


        damagerBaseClass.Init(sceneObject);
        GameObject go = new GameObject("EnemyAnimator");
        sceneObject.objectAnimator = SceneObjectAnimator.Create(sceneObject);
        sceneObject.objectAnimator.Init(soSceneObjectAnimationData);
    }
}