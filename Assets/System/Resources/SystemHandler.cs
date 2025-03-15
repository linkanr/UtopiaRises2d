using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SystemHandler
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        if (GameSystemSettings.loadSystems)
        {
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("System")));
        }
    }
}