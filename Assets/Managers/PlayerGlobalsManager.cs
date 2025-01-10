using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalsManager : MonoBehaviour
{
    public SoPlayerBaseBuilding soPlayerBaseBuilding;
    public static PlayerGlobalsManager instance;
    public Vector3  basePositions; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
