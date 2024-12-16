

using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdamagableGetter
{
    private SceneObjectManager sceneObjectManager;
    public IdamagableGetter(SceneObjectManager _sceneObjectManager)
    {
        sceneObjectManager = _sceneObjectManager;
    } 
    public IDamageable GetIdamagable(Vector3 position)
    {
        IDamageable newDamageAble = null;
        float dist = float.MaxValue;
        foreach (IDamageable sceneObject in sceneObjectManager.iDamagablesInScene)
        {
            Vector3 objPos = sceneObject.GetTransform().position;
            float newDist = Vector3.Distance(objPos, position);
            if (newDist < dist) 
            {
                newDamageAble = sceneObject;
                dist = newDist;
            }
        }
        return newDamageAble;
    }
    public List<IDamageable> GetIdamagables(Vector3 position, int amount)
    {
        List<ListPostition> listPoss = new List<ListPostition>();
        float dist = float.MaxValue;
        int i = 0;
        foreach (IDamageable sceneObject in sceneObjectManager.iDamagablesInScene)
        {
            Vector3 objPos = sceneObject.GetTransform().position;
            float newDist = Vector3.Distance(objPos, position);
            ListPostition listPostition = new ListPostition(i, dist);
            i++;
        }
        listPoss.Sort((list1, list2) => list1.dist.CompareTo(list2.dist)); // Sort list based on distance
        List<IDamageable> list = new List<IDamageable>();
        for (int j = 0; j<amount ; j++) // Loop for the amount and then add it using created list
        {
            list.Add(sceneObjectManager.iDamagablesInScene[listPoss[j].i]);
        }
        return list;

    }
    public List<IDamageable> GetIdamagables(Vector3 position, float distance)
    {

        List<IDamageable> list = new List<IDamageable>();
        Debug.Log("distance is " + distance);
        foreach (IDamageable sceneObject in sceneObjectManager.iDamagablesInScene)
        {

            if (sceneObject.GetTransform() == null)
            {
                Debug.Log("no transform");
                continue;
            }
                
            Vector3 objPos = sceneObject.GetTransform().position;
            float newDist = Vector3.Distance(objPos, position);
            if (newDist < distance)
            {
                list.Add(sceneObject);
                Debug.Log("adding object to list");
            }
            else
            {
                Debug.Log("out of reach distance is " + newDist + " sceneobject " + sceneObject.sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
            }
        }
        return list;



    }

}
public class ListPostition 
{
    internal ListPostition(int _i, float _dist) 
        { 
            i = _i;
            dist = _dist;
        }
    internal int i;
    internal float dist;



}