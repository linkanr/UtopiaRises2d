using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemHandler
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute() => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("System")));
}
