using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Factions/faction")]
public class Faction:SerializedScriptableObject
{
    public FactionsEnums factionEnum;
    public string description;
    public Color color;
    public PoliticalAlignment politicalAlignment;
}