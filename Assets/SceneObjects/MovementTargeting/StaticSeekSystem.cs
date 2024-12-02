using Sirenix.OdinInspector.Editor.Drawers;
using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class StaticSeekSystem : ScriptableObject, Iseeker
{ 

    public float lookForNewTargetTime;
    public Action<Transform> OnNewTarget;





    public abstract void Seek(Vector2 position, List<iDamageableTypeEnum> IdamageableEnum, TargetPriorityEnum targetPriorityEnum);

}