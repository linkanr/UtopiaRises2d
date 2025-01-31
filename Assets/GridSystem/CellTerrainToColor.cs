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
        CellReturnInfoArgs cellReturnInfoArgs = new CellReturnInfoArgs();
        if (distanceR < offset)

        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.playerTerrain;
            return cellReturnInfoArgs;

        }
        if (distanceG < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.enemyTerain;
            return cellReturnInfoArgs;
        }
        if (distanceB < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.enemyTerain;
            cellReturnInfoArgs.cEllContainsEnum = CellContainsEnum.enemyBase;
            return cellReturnInfoArgs;
        }
        if (distanceC < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.playerTerrain;
            cellReturnInfoArgs.cEllContainsEnum = CellContainsEnum.constructionCore;
            return cellReturnInfoArgs;
        }
        if (distanceM < offset)
        {
            cellReturnInfoArgs.cellTerrainEnum = CellTerrainEnum.enemyTerain;
            cellReturnInfoArgs.cEllContainsEnum = CellContainsEnum.playerBase;
            return cellReturnInfoArgs;
        }


            throw new Exception("Color not found");
   

    }
}
public class CellReturnInfoArgs
{
    public CellTerrainEnum cellTerrainEnum;
    public CellContainsEnum cEllContainsEnum;
}

public enum CellContainsEnum
{
    none,
    playerBase,
    enemyBase,
    constructionCore,
    
}