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
    public List<SceneObject> sceneObjectsInScene; // All idamageable should add themselves to this list

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

        sceneObjectsInScene = new List<SceneObject>();
        sceneObjectGetter = new SceneObjectGetter(this);
    }

    private void OnEnable()
    {
        BattleSceneActions.OnSceneObjectDestroyed += HandleDamagableDestroyed;
        BattleSceneActions.OnSceneObejctCreated += HandleDamagableCreated;
    }

    private void OnDisable()
    {
        BattleSceneActions.OnSceneObjectDestroyed -= HandleDamagableDestroyed;
        BattleSceneActions.OnSceneObejctCreated -= HandleDamagableCreated;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            string objInScene = "Scene objects in scene: ";
            Debug.Log("Amount of scene objects is " + sceneObjectsInScene.Count);
            foreach (SceneObject sceneObject in sceneObjectsInScene)
            {
                objInScene += sceneObject.GetStats().GetString(StatsInfoTypeEnum.name) + " ";
            }
            Debug.Log(objInScene);
        }
    }

    /// <summary>
    /// Handles the creation of a damageable scene object.
    /// </summary>
    /// <param name="target">The scene object that was created.</param>
    private void HandleDamagableCreated(SceneObject target)
    {
        Debug.Log("Added " + target.GetStats().GetString(StatsInfoTypeEnum.name) + " to sceneObjectsInScene");
        sceneObjectsInScene.Add(target);
    }

    /// <summary>
    /// Handles the destruction of a damageable scene object.
    /// </summary>
    /// <param name="target">The scene object that was destroyed.</param>
    private void HandleDamagableDestroyed(SceneObject target)
    {
        sceneObjectsInScene.Remove(target);
    }
}
