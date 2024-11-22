using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/CardNames")]
public class SoCardGlobalDic : SerializedScriptableObject
{
    public Dictionary<CardNames, SoCardBase> CardEnumsToCards;
}
public enum CardNames
{
    createWoodTower,
    createSniperTower,
    createFollower,
    placeStone,


}



