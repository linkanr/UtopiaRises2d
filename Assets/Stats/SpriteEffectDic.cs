using Sirenix;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DataResources/SpriteEffectDic")]
public class SpriteEffectDic :SerializedScriptableObject
{
    public Dictionary<PickupTypes, Sprite> spriteEffects;
}