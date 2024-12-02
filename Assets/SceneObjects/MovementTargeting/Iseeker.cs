using System.Collections.Generic;
using UnityEngine;

internal interface Iseeker
{
    public void Seek(Vector2 position, List<iDamageableTypeEnum> IdamageableEnum, TargetPriorityEnum targetPriorityEnum);
    

}