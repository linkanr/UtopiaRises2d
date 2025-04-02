using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{

    public static Action mapSceneLoaded;

    private void Start()
    {
        StartCoroutine("WaitUntilSceneIntialized");
        
    }

    private IEnumerator WaitUntilSceneIntialized()
    {
        yield return new WaitUntil(() => MapManager.instance.Intialized);
        yield return new WaitForEndOfFrame();
        mapSceneLoaded?.Invoke();

    }

}
