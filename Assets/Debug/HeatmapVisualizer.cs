using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeatmapVisualizer : MonoBehaviour
{
    Material mat;

    private void Start()
    {
       
    }
    private void OnEnable()
    {
        mat = GetComponent<SpriteRenderer>().material;
        CellActions.UpdatedTexture += GenerateHeatTexture;
    }
    private void OnDisable()
    {
        CellActions.UpdatedTexture -= GenerateHeatTexture;
    }

    private void GenerateHeatTexture(Texture2D d)
    {
        mat.SetTexture("_Texture2D", d);
    }
}




