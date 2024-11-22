using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    public static GridCellManager Instance;
    public GridConstrution gridConstrution;
    public float gridSize;
    public int gridAmount;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gridConstrution = new GridConstrution(gridAmount, gridAmount, gridSize, new Vector3(0f, 0f, 0f));
    }

}