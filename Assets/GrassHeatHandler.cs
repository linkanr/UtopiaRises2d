using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassHeatHandler : MonoBehaviour
{
    public Material grassMaterial;
    private void OnEnable()
    {
        CellActions.updatedTexture += UpdateHeatMap;
    }
    private void OnDisable()
    {
        CellActions.updatedTexture -= UpdateHeatMap;
    }
    private void Start()
    {
        grassMaterial = GetComponent<TilemapRenderer>().material;
    }
    private void UpdateHeatMap(Texture2D d)
    {
        Debug.Log("Updating Heatmap texture");
        grassMaterial.SetTexture("_HeatTexture", d);
    }
}
