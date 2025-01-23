using System;
using UnityEngine;

internal class CellTerrainToColor
{
    
    internal static CellTerrainEnum GetTerrain(Color pixelColor)
    {
        float offset = 0.1f;
        Vector3 color = new Vector3(pixelColor.r, pixelColor.g, pixelColor.b);
        float distanceR = Vector3.Distance(color, new Vector3(1f, 0f, 0f));
        float distanceG = Vector3.Distance(color, new Vector3(0f, 1f, 0f));
        if (distanceR < offset)
        {
            return CellTerrainEnum.g;
        }
        else if (distanceG < offset)
        {
            return CellTerrainEnum.u;
        }

        else
        {
            throw new Exception("Color not found");
        }

    }
}
