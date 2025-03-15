using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class SoCardBase:ScriptableObject
{

    public string title;
    public string description;
    public CardType cardType;
    public Sprite image;
    public CardRareEnums rarity;
    public CardCanBePlayedOnEnum cardCanBePlayedOnEnum;
    public CardPlayTypeEnum cardPlayTypeEnum;

    public int influenceCost;
    public Faction faction;
    

    



    public bool Effect(Vector3 position, out string result, CardCostModifier cardCostModifier = null)
    {
        if (cardCostModifier != null)
        {
            influenceCost = cardCostModifier.modifiedCost;
        }
        if (FisicalResources.TryToBuy(influenceCost))
        {
            
            if (ActualEffect(position, out result))
            {
                result = "success";
                FisicalResources.Buy(influenceCost);
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
    construtcionBase

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