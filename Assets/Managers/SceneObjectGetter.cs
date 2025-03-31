

using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectGetter
{
    private SceneObjectManager sceneObjectManager;
    public SceneObjectGetter(SceneObjectManager _sceneObjectManager)
    {
        sceneObjectManager = _sceneObjectManager;
    }

    public List<SceneObject> GetSceneObjects(Vector3 position, int amount = -1, SceneObjectTypeEnum objectTypeEnum = SceneObjectTypeEnum.all, List<SceneObjectTypeEnum> sceneObjectTypeEnumsList = null, float maxDistance = float.PositiveInfinity, bool onlyDamageables = false)
    {
        position.z = 0;
        List<ListPostition> listPoss = new List<ListPostition>();

        List<SceneObject> sceneObjects = sceneObjectManager.RetriveSceneObjects(objectTypeEnum);
        for (int i=0; i < sceneObjects.Count; i++)
        {
            SceneObject sceneObject = sceneObjects[i];
            if (sceneObject.healthSystem == null && onlyDamageables)
            {
              
               
                continue;
            }
            if (objectTypeEnum != SceneObjectTypeEnum.all)
            {
                if (objectTypeEnum != sceneObject.GetStats().sceneObjectType)
                {
                 
                    
                    continue;
                }
            }

            if (sceneObjectTypeEnumsList != null)
            {
                if (!sceneObjectTypeEnumsList.Contains(sceneObject.GetStats().sceneObjectType) || sceneObjectTypeEnumsList.Contains(SceneObjectTypeEnum.all))
                {
                 
                    
                    continue;
                }
            }

            Vector3 objPos = sceneObject.sceneObjectPosition;
            float newDist = Vector3.Distance(objPos, position);
            ListPostition listPostition = new ListPostition(i, newDist);
            if (newDist < maxDistance)
            {


                listPoss.Add(listPostition);

            }

            
        }
        listPoss.Sort((list1, list2) => list1.dist.CompareTo(list2.dist)); // Sort list based on distance
        List<SceneObject> list = new List<SceneObject>();

        if (amount == -1)
        {
            amount = listPoss.Count;
        }
        else if (amount > listPoss.Count)
        {
            amount = listPoss.Count;
        }
        for (int j = 0; j < amount; j++) // Loop for the amount and then add it using created list
        {

            list.Add(sceneObjectManager.RetriveSceneObjects(objectTypeEnum)[listPoss[j].i]);
        }
        return list;

    }
    public List<SceneObject> FilterBasedOnFaction(List<SceneObject> sceneObjects, Faction faction)
    {
        List<SceneObject> newList = new List<SceneObject>();
        foreach (SceneObject obj in sceneObjects)
        {
            if (obj.GetStats().faction == faction)
            {
                newList.Add(obj);
            }


        }
        return newList;
    }
    public List<SceneObject> FilterBasedOnModifers(List<SceneObject> sceneObjects, PickupTypes pickupTypes)
    {
        List<SceneObject> newList = new List<SceneObject>();
        foreach (SceneObject obj in sceneObjects)
        {
            foreach (StatsModifier statsModifier in obj.GetStats().statsMediator.modifiers)
            {
            
                if (statsModifier.pickupTypes == pickupTypes)
                {
     
                    newList.Add(obj);
                    break;
                }
            }
        }
        return newList;
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