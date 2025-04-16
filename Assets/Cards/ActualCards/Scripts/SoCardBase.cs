using Sirenix.OdinInspector;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class SoCardBase:ScriptableObject
{

    public string title;
    [MultiLineProperty(5)]
    public string description;

    public CardType cardType;
    public Sprite image;
    public CardRareEnums rarity;
    public CardCanBePlayedOnEnum cardCanBePlayedOnEnum;
    public CardPlayTypeEnum cardPlayTypeEnum;

    public int influenceCost;
    public int healthCost = 0;
    public Faction faction;
    

    



    public bool Effect(Vector3 position, out string result, CardCostModifier cardCostModifier = null)
    {
        int modifiedCost = influenceCost;
        if (cardCostModifier != null)
        {
            modifiedCost = cardCostModifier.modifiedCost;
        }
        if (FisicalResources.TryToBuy(modifiedCost))
        {
            
            if (ActualEffect(position, out result))
            {
                result = "success";
                FisicalResources.Buy(modifiedCost);
                HealthManager.Instance.TakeDamage(healthCost);
                return true;
            }
            else
            {
                return false;   
            }
            


        }
        else
        {
            result = "Cant afford";
            return false;   
        }
     
    }
    public abstract bool ActualEffect(Vector3 position, out string failuerReason);
    protected bool CheckIfCardMathesTerrain(Cell cell)
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
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass)
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
                    if (cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.soil || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.grass || cell.cellTerrain.cellTerrainEnum == CellTerrainEnum.playerTerrain)
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
            case CardCanBePlayedOnEnum.influencedTerritory:
                {
                    if (cell.isPlayerInfluenced)
                    {
                        return true;
                    }

                    return false;
                }
            default:
                Debug.LogError("Unhandled CardCanBePlayedOnEnum value:" + cardCanBePlayedOnEnum);
                return false;
        }
    }
}

public enum CardCanBePlayedOnEnum
{
    playerGround,
    enemyGround,
    damagable,
    instantClick,
    playerOrEnemyGround,
    influencedTerritory

}
public enum CardType
{
    skill,
    summonTower,
    areaDamage,
    directDamage,
    power,
    summonObjects

}