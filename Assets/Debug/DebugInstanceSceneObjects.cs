using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DebugInstanceSceneObjects :SerializedMonoBehaviour , IDebugFunction<string>
{
    public static DebugInstanceSceneObjects instance;
    public List <SoSceneObjectBase> soSceneObjectList;
    public string sceneObjectName;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of DebugInstanceSceneObjects found!");
            return;
        }
        instance = this;
    }
    public void SetString(string sceneObjectName)
    {
       
        this.sceneObjectName = sceneObjectName;
    }
    public void Execute(string sceneObjectName)

    {

        SoSceneObjectBase sceneObjectToInstance = null;
        foreach (var soSceneObject in soSceneObjectList)
        {
            if (soSceneObject.sceneObjectName == sceneObjectName)
            {

                sceneObjectToInstance = soSceneObject;
               

                break;
            }
        }
        if (sceneObjectToInstance == null)
        {
            Debug.LogError("SceneObject with name " + sceneObjectName + " not found!");
            return;
        }
        if (sceneObjectToInstance.sceneObjectType== SceneObjectTypeEnum.enemy)
        {
        
            EnemyCreator.CreateEnemy((SoEnemyObject)sceneObjectToInstance, WorldSpaceUtils.GetMouseWorldPosition());
        }
        else
        {
            sceneObjectToInstance.Init(WorldSpaceUtils.GetMouseWorldPosition());
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
          
            Execute(sceneObjectName);
        }

    }
}

