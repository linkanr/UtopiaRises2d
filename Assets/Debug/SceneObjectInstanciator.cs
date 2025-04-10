using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SceneObjectInstanciator :SerializedMonoBehaviour 
{
    public static SceneObjectInstanciator instance;

    public List<SoSceneObjectBase> allSceneObjects;
    public string sceneObjectName;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of DebugInstanceSceneObjects found!");
            return;
        }
        instance = this;



        allSceneObjects = new List<SoSceneObjectBase>();

        allSceneObjects.AddRange(Resources.LoadAll<SoSceneObjectBase>("SceneObjectSoBase/buildings"));
        allSceneObjects.AddRange(Resources.LoadAll<SoSceneObjectBase>("SceneObjectSoBase/enemies"));
        allSceneObjects.AddRange(Resources.LoadAll<SoSceneObjectBase>("SceneObjectSoBase/enemybase"));
        allSceneObjects.AddRange(Resources.LoadAll<SoSceneObjectBase>("SceneObjectSoBase/enviroment"));
        allSceneObjects.AddRange(Resources.LoadAll<SoSceneObjectBase>("SceneObjectSoBase/minor"));
        allSceneObjects.AddRange(Resources.LoadAll<SoSceneObjectBase>("SceneObjectSoBase/playerbase"));


    }
    public void SetString(string sceneObjectName)
    {
       
        this.sceneObjectName = sceneObjectName;
    }
    public SceneObject Execute(string _sceneObjectName, Vector3 pos)

    {
       // Debug.Log("SceneObjectInstanciator Execute " + _sceneObjectName);
        SoSceneObjectBase sceneObjectToInstance = null;
        foreach (var soSceneObject in allSceneObjects)
        {
            if (soSceneObject.sceneObjectName == _sceneObjectName)
            {

                sceneObjectToInstance = soSceneObject;
               

                break;
            }
        }
        if (sceneObjectToInstance == null)
        {
            Debug.LogError("SceneObject with name " + _sceneObjectName + " not found!");
            return null;
        }
        if (sceneObjectToInstance.sceneObjectType== SceneObjectTypeEnum.enemy)
        {
        
            return EnemyCreator.CreateEnemy((SoEnemyObject)sceneObjectToInstance, WorldSpaceUtils.GetMouseWorldPosition());
        }
        else
        {
            return sceneObjectToInstance.Init(pos);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Vector3 pos = WorldSpaceUtils.GetMouseWorldPosition();
            Vector3 pos2 = GridCellManager.instance.gridConstrution.GetCellPositionByPosition(pos);
            Execute(sceneObjectName, pos2);
        }

    }
}

