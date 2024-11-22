using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance;
   
    private void Awake()
    {
        Instance = this;
    }

}
