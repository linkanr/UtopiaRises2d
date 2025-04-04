using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Settings/Map Settings", fileName = "MapSettings")]
public class MapSettings : SerializedScriptableObject
{
    [Header("Node Generation")]
    public int startingNodesMinCount = 2;
    public int startingNodesMaxCount = 4;
    public int nodesLevelCount = 6;
    public int leftRightCount = 4;

    [Header("Chance of Node Connections")]
    public float chanceOfTwoNodes = 0.4f;
    public float chanceOfThreeNodes = 0.4f;
    public float chanceOfFourNodes = 0.2f;
    public float chanceOfRandomLine = 0.2f;

    [Header("Noise Settings")]
    public float noiseFreq = 0.2f;
    public float noiseAmp = 0.5f;
    public float noiseAmpRand = 0.1f;
    public float noiseFreq2 = 0.5f;
    public float noiseAmp2 = 0.3f;

    [Header("Special Node Counts")]
    public int eliteMin = 4;
    public int eliteMax = 6;
    public int shopMin = 2;
    public int shopMax = 4;
    public int restMin = 2;
    public int restMax = 4;
    public int eventMin = 5;
    public int eventMax = 7;
}
