using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(menuName = "Cards/BuildWoodenTower")]
public class CardBuildShotingTower : SoCardInstanciate, IHasClickEffect
{


    public Sprite GetSprite()
    {
        SoshotingBuilding soshotingBuilding = prefab as SoshotingBuilding; 
        return soshotingBuilding.attackSystem.displayRangeSprite;
    }

    public float GetSpriteSize()
    {
        SoshotingBuilding soshotingBuilding = prefab as SoshotingBuilding;
        return soshotingBuilding.maxShootingDistance *2f;
    }
}