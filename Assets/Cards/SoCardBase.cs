using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class SoCardBase:ScriptableObject
{

    public string description;
    public bool instant;
    public string title;
    public Sprite image;
    public int influenceCost;
    public Faction faction;
    public CardPlayType cardType;
    



    public bool Effect(Vector3 position, out string result)
    {
        if (FisicalResources.TryToBuy(influenceCost))
        {
            ActualEffect(position);
            
            result = "success";
            return true;

        }
        else
        {
            result = "Cant afford";
            return false;   
        }
     
    }
    public abstract void ActualEffect(Vector3 position);
}
public enum CardPlayType
{
    normal,
    yearly,
    once
    
}