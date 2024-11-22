using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalsManager : MonoBehaviour
{
    public static PlayerGlobalsManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("two global managers");
        }
    }

    public int influenceEachTurn { get; private set; }
    public int cardAmount { get; private set; }
    private void Start()
    {
        influenceEachTurn = 3;
        cardAmount = 5;
    }
}
