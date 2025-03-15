using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages scene objects within the game, handling their creation, destruction, and retrieval.
/// </summary>
public class SceneObjectManager : MonoBehaviour
{
    public static SceneObjectManager Instance;
    public SceneObjectGetter sceneObjectGetter;
    private List<SceneObject> allSceneObjectsInScene; // All idamageable should add themselves to this list
    private Dictionary<SceneObjectTypeEnum, List<SceneObject>> sceneObjectsInSceneByType;


    public List<SceneObject> RetriveSceneObjects(SceneObjectTypeEnum sceneObjectTypeEnum)

    {
        switch (sceneObjectTypeEnum)
        {
            case SceneObjectTypeEnum.all:
                return allSceneObjectsInScene;
            default:
                if (!sceneObjectsInSceneByType.ContainsKey(sceneObjectTypeEnum))
                {
                    return new List<SceneObject>();
                }
                return sceneObjectsInSceneByType[sceneObjectTypeEnum];
        }
   
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Duplicate manager detected");
        }

        allSceneObjectsInScene = new List<SceneObject>();
        sceneObjectGetter = new SceneObjectGetter(this);
        sceneObjectsInSceneByType = new Dictionary<SceneObjectTypeEnum, List<SceneObject>>();
    }

    private void OnEnable()
    {
        BattleSceneActions.OnSceneObjectDestroyed += HandleSceneObjectDestroyed;
        BattleSceneActions.OnSceneObejctCreated += HandleSceneObjectCreated;
    }

    private void OnDisable()
    {
        BattleSceneActions.OnSceneObjectDestroyed -= HandleSceneObjectDestroyed;
        BattleSceneActions.OnSceneObejctCreated -= HandleSceneObjectCreated;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            string objInScene = "Scene objects in scene: ";
            //Debug.Log("Amount of scene objects is " + sceneObjectsInScene.Count);
            foreach (SceneObject sceneObject in allSceneObjectsInScene)
            {
                objInScene += sceneObject.GetStats().GetString(StatsInfoTypeEnum.name) + " ";
            }
           // Debug.Log(objInScene);
        }
    }

    /// <summary>
    /// Handles the creation of a damageable scene object.
    /// </summary>
    /// <param name="target">The scene object that was created.</param>
    private void HandleSceneObjectCreated(SceneObject target)
    {
        // Ensure the object is added to the master list
        allSceneObjectsInScene.Add(target);

        // Get the object's type
        SceneObjectTypeEnum type = target.GetStats().sceneObjectType;

        // Ensure the dictionary has a list for this type
        if (!sceneObjectsInSceneByType.ContainsKey(type))
        {
            sceneObjectsInSceneByType[type] = new List<SceneObject>();
        }

        // Add the object to its type-specific list
        sceneObjectsInSceneByType[type].Add(target);
    }


    /// <summary>
    /// Handles the destruction of a damageable scene object.
    /// </summary>
    /// <param name="target">The scene object that was destroyed.</param>
    private void HandleSceneObjectDestroyed(SceneObject target)
    {
        // Remove from the master list
        allSceneObjectsInScene.Remove(target);

        // Get the object's type
        SceneObjectTypeEnum type = target.GetStats().sceneObjectType;

        // Remove from the dictionary if it exists
        if (sceneObjectsInSceneByType.ContainsKey(type))
        {
            sceneObjectsInSceneByType[type].Remove(target);

            // If the list is now empty, remove the key from the dictionary
            if (sceneObjectsInSceneByType[type].Count == 0)
            {
                sceneObjectsInSceneByType.Remove(type);
            }
        }
    }

}
