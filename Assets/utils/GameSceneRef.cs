using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneRef : MonoBehaviour
{
    public static GameSceneRef instance;
    public LayerMask collisionLayerGrid;
    public Transform followerBirthPlace;
    public Transform worldGrid;
    public Transform constructionBaseParent;
    public RectTransform inHandPile;
    public RectTransform drawPile;
    public RectTransform discardPile;
    public RectTransform endTurnParent;
    public RectTransform exhusedPile;
    public Transform enemyParent;
    public List<Transform> extraEndPoints;
    public RectTransform stagingArea;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("error game scene ref");
    }


}
