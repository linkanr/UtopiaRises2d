using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void OnEnable()
    {
        BattleSceneActions.OnPreSceneInit += Initilization;
}
    private void OnDisable()
    {
        BattleSceneActions.OnPreSceneInit -= Initilization;
    }
    private void Initilization()
    {
        CardManager.Instance.GetStartingCards();
        BattleSceneActions.setInfluence(3);
        BattleSceneActions.OnPreSceneDone();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
