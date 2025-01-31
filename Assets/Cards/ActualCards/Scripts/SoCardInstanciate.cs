using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;



public abstract class SoCardInstanciate : SoCardBase

{
    public SoSceneObjectBase prefab;
    public int sizeX;
    public int sizeY;




    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        failureReason = "";
        Cell centerCell = GridCellManager.Instance.gridConstrution.GetCurrecntCellByMouse();
        List<Cell> cells = GridCellManager.Instance.gridConstrution.GetCellListByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition(), sizeX, sizeY);
        if (cells.Count < 1)
        {
            failureReason = "NO CELL FOUND";
            return false;
        }
       



        
        foreach (Cell cell in cells)
        {
            if (CheckIfCardMathesTerrain(cell))
            {
                prefab.Init(cell.worldPosition);
            }

        }
        return true;





    }

    private bool CheckIfCardMathesTerrain(Cell cell)
    {
        switch (cardCanBePlayedOnEnum)
        {
            case CardCanBePlayedOnEnum.playerGround:
                if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case CardCanBePlayedOnEnum.enemyGround:
                {
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.enemyTerain)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case CardCanBePlayedOnEnum.playerOrEnemyGround:
                {
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.enemyTerain || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case CardCanBePlayedOnEnum.damagable:
                {
                    Debug.LogError("Card can not be played on damagable");
                    return false;

                }
            case CardCanBePlayedOnEnum.instantClick:
                {
                    Debug.LogError("Card can not be played on instantClick");
                    return false;
                }
            case CardCanBePlayedOnEnum.construtcionBase:
                {
                    if (cell.hasSceneObejct)
                    {
                        foreach (SceneObject sceneObject in cell.containingSceneObjects)
                        {
                            if (sceneObject.GetStats().sceneObjectType == SceneObjectTypeEnum.playerConstructionBase)
                            {
                                return true;
                            }
                  
                        }
                    }
                    return false;
                }
            default:
                Debug.LogError("Unhandled CardCanBePlayedOnEnum value:" +cardCanBePlayedOnEnum);
                return false;
        }
    }
}