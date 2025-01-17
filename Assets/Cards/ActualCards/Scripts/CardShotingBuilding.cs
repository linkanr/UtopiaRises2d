using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(menuName = "Cards/BuildShotingBuilding")]
public class CardShotingBuilding : SoCardInstanciate, IHasClickEffect
{


    public Sprite GetSprite()
    {
        SoshotingBuilding soshotingBuilding = prefab as SoshotingBuilding; 
        return soshotingBuilding.attackSystem.displayRangeSprite;
    }

    public float GetSpriteSize()
    {
        SoshotingBuilding soshotingBuilding = prefab as SoshotingBuilding;
        return soshotingBuilding.damagerBaseClass.CalculateAttackRange() *2f;
    }
}