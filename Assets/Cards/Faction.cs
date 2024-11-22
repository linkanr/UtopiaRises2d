using Sirenix.OdinInspector;
using System.Drawing;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Factions/faction")]
public class Faction:SerializedScriptableObject
{
    public FactionsEnums factionEnum;
    public string description;
    public UnityEngine.Color color;
    public PoliticalAlignment politicalAlignment;
}