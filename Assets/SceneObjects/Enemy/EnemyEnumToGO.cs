using Sirenix;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/EnemyNames")]
public class EnemyEnumToGO: SerializedScriptableObject
{
    public Dictionary<EnemyNames, SoEnemyObject> EnemyEnumToName;
}
public enum EnemyNames
{
    dog,
    slime, 
    octopus

} 