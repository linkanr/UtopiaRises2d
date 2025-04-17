using System.Collections.Generic;
using UnityEngine;

public static class CellCardColoringUtil
{
    public static Color Evaluate(Cell cell, Card card, Color available, Color unavailable)
    {
        return cell.hasSceneObejct
            ? EvaluateNonEmptyCell(card, cell.containingSceneObjects.ToArray(), cell, available, unavailable)
            : EvaluateEmptyCell(card, cell, available, unavailable);
    }

    public static Color EvaluateEmptyCell(Card card, Cell cell, Color available, Color unavailable)
    {
        var availableEnums = CardUtils.GetAvailableEnums(cell.cellTerrain.cellTerrainEnum);

        foreach (var option in availableEnums)
        {
            if (option == card.cardBase.cardCanBePlayedOnEnum)
            {
                if (option == CardCanBePlayedOnEnum.influencedTerritory && !cell.isPlayerInfluenced)
                    return unavailable;

                return available;
            }
        }

        return unavailable;
    }

    public static Color EvaluateNonEmptyCell(Card card, SceneObject[] objects, Cell cell, Color available, Color unavailable)
    {
        switch (card.cardBase.cardCanBePlayedOnEnum)
        {
            case CardCanBePlayedOnEnum.damagable:
                foreach (var obj in objects)
                    if (obj.healthSystem != null)
                        return available;
                return unavailable;

            case CardCanBePlayedOnEnum.influencedTerritory:
                foreach (var obj in objects)
                    if (obj.GetStats().sceneObjectType == SceneObjectTypeEnum.playerConstructionBase)
                        return available;
                return unavailable;

            case CardCanBePlayedOnEnum.enemyGround:
                return (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass)
                        ? available : unavailable;

            case CardCanBePlayedOnEnum.playerGround:
                return cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain
                        ? available : unavailable;

            case CardCanBePlayedOnEnum.playerOrEnemyGround:
                return (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
                        ? available : unavailable;

            default:
                return unavailable;
        }
    }
}
