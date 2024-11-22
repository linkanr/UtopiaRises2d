using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneRef : MonoBehaviour
{
    public static GameSceneRef instance;
    public LayerMask collisionLayerGrid;
    public Transform goalPosition;
    public Transform worldGrid;
    public RectTransform panel;
    public RectTransform drawPile;
    public RectTransform discardPile;
    public RectTransform endTurnParent;
    public RectTransform exhusedPile;
    public Transform enemyParent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("error game scene ref");
    }


}
