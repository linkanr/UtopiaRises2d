using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    public static GridCellManager Instance;
    public GridConstrution gridConstrution;
    public float gridSize;
    public int gridAmountX;
    public int gridAmountY;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gridConstrution = new GridConstrution(gridAmountX, gridAmountY, gridSize, new Vector3(0f, 0f, 0f));
    }

}