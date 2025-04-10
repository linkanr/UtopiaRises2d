using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "ScriptableObjects/SoBuildnings/SpawningBuildings")]
public class SoBuildingSpawner : SoBuilding
{
    public DamagerBaseClass damagerBaseClass;
    public SpawningData spawningData;

    protected override Stats GetStatsInernal(Stats _statsInforDic)
    {
        base.GetStatsInernal(_statsInforDic);
        DamagerBaseClass damagerInstance = damagerBaseClass.Clone();
        damagerBaseClass = damagerInstance;
        _statsInforDic.Add(StatsInfoTypeEnum.damager, damagerBaseClass);

        _statsInforDic.Add(StatsInfoTypeEnum.spawningData, spawningData);
        return _statsInforDic;
    }
    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        damagerBaseClass.Init(sceneObject);
    }
}
