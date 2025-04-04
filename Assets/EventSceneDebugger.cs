using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSceneDebugger : MonoBehaviour
{
    public void ReloadLevel()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
