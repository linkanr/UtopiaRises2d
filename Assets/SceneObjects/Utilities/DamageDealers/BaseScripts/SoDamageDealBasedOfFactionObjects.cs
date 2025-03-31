using UnityEngine;
[System.Serializable]
public class SoDamageDealBasedOnFactionObjects : DamagerBaseClass
{
    public int additionPerObject;
    public Faction factionBonus;





    public override float CalculateAttackRange()
    {
        return attackRange;
    }

    public override int CalculateDamageImplementation(int _baseDamage)
    {
        //Debug.Log("Calculating damage based on faction");
        var objectList = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(Vector3.zero);
        //Debug.Log("Object list count is " + objectList.Count);
        foreach (var obj in objectList)
        {
            //  Debug.Log("name of object is " + obj.GetStats().name);
            //Debug.Log("Object faction is " + obj.GetStats().faction.factionEnum);
        }
        var filteredList = SceneObjectManager.Instance.sceneObjectGetter.FilterBasedOnFaction(objectList, factionBonus);
        //Debug.Log("Filtered list count is " + filteredList.Count);
        return filteredList.Count * additionPerObject + _baseDamage;
    }

    public override float CalculateReloadTime()
    {
        return reloadTime;
    }
}
