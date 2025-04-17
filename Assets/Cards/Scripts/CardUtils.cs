using System.Collections.Generic;
using UnityEngine;

public static class CardUtils
{
    public static List<CardCanBePlayedOnEnum> GetAvailableEnums(CellTerrainEnum terrain)
    {
        var result = new List<CardCanBePlayedOnEnum>();
        switch (terrain)
        {
            case CellTerrainEnum.playerTerrain:
                result.Add(CardCanBePlayedOnEnum.playerGround);
                result.Add(CardCanBePlayedOnEnum.playerOrEnemyGround);
                result.Add(CardCanBePlayedOnEnum.influencedTerritory);
                break;
            case CellTerrainEnum.soil:
            case CellTerrainEnum.grass:
            case CellTerrainEnum.water:
                result.Add(CardCanBePlayedOnEnum.enemyGround);
                result.Add(CardCanBePlayedOnEnum.playerOrEnemyGround);
                break;
        }
        return result;
    }

    public static Color EvaluateEmptyCell(Card card, Cell cell, Color available, Color unavailable)
    {
        var enums = GetAvailableEnums(cell.cellTerrain.cellTerrainEnum);
        foreach (var option in enums)
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
    public static bool IsCardPlayableOnCell(Card card, ClickableType clickType, Cell cell)
    {
        var type = card.cardBase.cardCanBePlayedOnEnum;

        switch (type)
        {
            case CardCanBePlayedOnEnum.instantClick:
                return clickType != ClickableType.card;

            case CardCanBePlayedOnEnum.damagable:
                return clickType == ClickableType.SceneObject;

            case CardCanBePlayedOnEnum.playerGround:
                return clickType != ClickableType.card &&
                       cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain;

            case CardCanBePlayedOnEnum.enemyGround:
                return clickType != ClickableType.card &&
                       (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.water);

            case CardCanBePlayedOnEnum.playerOrEnemyGround:
                return clickType != ClickableType.card &&
                       (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass ||
                        cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain);

            case CardCanBePlayedOnEnum.influencedTerritory:
                return clickType != ClickableType.card &&
                       cell.isPlayerInfluenced &&
                       cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain;

            default:
                Debug.LogError($"Unhandled CardCanBePlayedOnEnum: {type}");
                return false;
        }
    }
}
