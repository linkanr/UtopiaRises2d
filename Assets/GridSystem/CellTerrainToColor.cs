using System;
using UnityEngine;
using UnityEngine.UIElements;

internal class CellTerrainToColor
{
    
    internal static CellReturnInfoArgs GetTerrain(Color pixelColor)
    {
        float offset = 0.1f;
        Vector3 color = new Vector3(pixelColor.r, pixelColor.g, pixelColor.b);
        float distanceR = Vector3.Distance(color, new Vector3(1f, 0f, 0f));
        float distanceG = Vector3.Distance(color, new Vector3(0f, 1f, 0f));
        float distanceB = Vector3.Distance(color, new Vector3(0f, 0f, 1f));
        float distanceC = Vector3.Distance(color, new Vector3(0f, 1f, 1f));
        float distanceM = Vector3.Distance(color, new Vector3(1f, 0f, 1f));
        float distanceY = Vector3.Distance(color, new Vector3(1f, 1f, 0f));
        float distaanceBlack = Vector3.Distance(color, new Vector3(0f, 0f, 0f));    
        float distanceWhite = Vector3.Distance(color, new Vector3(1f, 1f, 1f));
        CellReturnInfoArgs cellReturnInfoArgs = new CellReturnInfoArgs();
        if (distanceR < offset)

        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.playerTerrain;
            return cellReturnInfoArgs;

        }
        if (distanceG < offset)
        {

                cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.grass;
                return cellReturnInfoArgs;
         

        }
        if (distanceY < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.soil;
            return cellReturnInfoArgs;
        }
        if (distanceB < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.water;
            
            return cellReturnInfoArgs;
        }
        if (distanceC < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.playerTerrain;
            cellReturnInfoArgs.cEllContainsEnum = "Construction Base";
            return cellReturnInfoArgs;
        }
        if (distanceM < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.water;
            cellReturnInfoArgs.cEllContainsEnum = "Bridge";

            return cellReturnInfoArgs;
        }
        if (distaanceBlack < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.soil;
        
        }


            throw new Exception("Color not found");
   

    }
}
public class CellReturnInfoArgs
{
    public CellTerrainEnum cellTerrainEnum;
    public string cEllContainsEnum;
}

public enum CellContainsEnum
{
    none,
    playerBase,
    enemyBase,
    constructionCore,
    
}