

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

    
    public SceneObject GetSceneObject(Vector3 position, SceneObjectTypeEnum sceneObjectTypeEnum = SceneObjectTypeEnum.all, List<SceneObjectTypeEnum> sceneObjectTypeEnumsList = null, float maxDistance = float.PositiveInfinity, bool onlyDamageables = false)
    {
        SceneObject newSceneObject = null;
        
        foreach (SceneObject sceneObject in sceneObjectManager.sceneObjectsInScene)
        {
            //Debug.Log("starting to look for scene object " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
            if (sceneObject is not IDamageAble && onlyDamageables)
            {
              //  Debug.Log("not damagable " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
                continue;
            }
            Vector3 objPos = sceneObject.transform.position;
            if (Vector3.Distance(objPos, position) > maxDistance)
            {
                //Debug.Log("to far away " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
                continue;
            }

            if (sceneObject.GetStats().sceneObjectType != sceneObjectTypeEnum && sceneObjectTypeEnum != SceneObjectTypeEnum.all)
            {
                //Debug.Log("not matching type " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
                continue;
            }

            if (sceneObjectTypeEnumsList != null)
            {
                if (!sceneObjectTypeEnumsList.Contains(sceneObject.GetStats().sceneObjectType))
                {
                  //  Debug.Log("not matching list" + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
                    continue;

                }
            }


            float newDist = Vector3.Distance(objPos, position);
            if (newDist < maxDistance)
            {
                //Debug.Log("closest object " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
                newSceneObject = sceneObject;
                maxDistance = newDist;
            }
        }
        return newSceneObject;
    }
    





    
    


    public List<SceneObject> GetSceneObjects(Vector3 position, int amount = -1,SceneObjectTypeEnum objectTypeEnum = SceneObjectTypeEnum.all, List<SceneObjectTypeEnum> sceneObjectTypeEnumsList = null, float maxDistance = float.PositiveInfinity, bool onlyDamageables = false)
    {
        if (DebuggerGlobal.instance.debugSceneObejcts)
            Debug.Log("looking for" +  objectTypeEnum.ToString() + " at " + position.ToString());
        List<ListPostition> listPoss = new List<ListPostition>();

        int i = 0;
        foreach (SceneObject sceneObject in sceneObjectManager.sceneObjectsInScene)
        {
            if (sceneObject is not IDamageAble && onlyDamageables)
            {
                if (DebuggerGlobal.instance.debugSceneObejcts)
                    Debug.Log("found " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name) + " at " + sceneObject.transform.position.ToString() +" not added because its not damagable" );
                continue;
            }
            if (objectTypeEnum !=SceneObjectTypeEnum.all)
            {
                if (objectTypeEnum !=sceneObject.GetStats().sceneObjectType)
                {
                    if (DebuggerGlobal.instance.debugSceneObejcts)
                        Debug.Log("found " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name) + " at " + sceneObject.transform.position.ToString() + " not added because its not " + objectTypeEnum.ToString());
                    continue;
                }
            }

            if (sceneObjectTypeEnumsList != null)
            {
                if (!sceneObjectTypeEnumsList.Contains(sceneObject.GetStats().sceneObjectType) || sceneObjectTypeEnumsList.Contains(SceneObjectTypeEnum.all))
                {
                    if (DebuggerGlobal.instance.debugSceneObejcts)
                        Debug.Log("found " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name) + " at " + sceneObject.transform.position.ToString() + " not added because " + objectTypeEnum.ToString()+ " is not in list");
                    continue ;
                }
            }

            Vector3 objPos = sceneObject.transform.position;
            float newDist = Vector3.Distance(objPos, position);
            ListPostition listPostition = new ListPostition(i, newDist);
            if (newDist < maxDistance)
            {
                if (DebuggerGlobal.instance.debugSceneObejcts)
                {
                    Debug.Log("found " + sceneObject.GetStats().GetString(StatsInfoTypeEnum.name) + " at " + sceneObject.transform.position.ToString() + " added to the list at position " + i);
                    DebuggerGlobal.DrawLine(objPos, position, Color.red);
                }
                    
                listPoss.Add(listPostition);
                
            }

            i++;
        }
        listPoss.Sort((list1, list2) => list1.dist.CompareTo(list2.dist)); // Sort list based on distance
        List<SceneObject> list = new List<SceneObject>();

        if (amount == -1)
        {
            amount = listPoss.Count;
        }
        else if (amount> listPoss.Count)
        {
            amount = listPoss.Count;
        }
        for (int j = 0; j<amount ; j++) // Loop for the amount and then add it using created list
        {
            if (DebuggerGlobal.instance.debugSceneObejcts)
                Debug.Log("added " + sceneObjectManager.sceneObjectsInScene[listPoss[j].i].GetStats().GetString(StatsInfoTypeEnum.name));
            list.Add(sceneObjectManager.sceneObjectsInScene[listPoss[j].i]);
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