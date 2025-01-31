using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class SoCardBase:ScriptableObject
{

    public string description;
    
    public string title;
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