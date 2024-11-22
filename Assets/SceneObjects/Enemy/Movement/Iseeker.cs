using UnityEngine;

internal interface Iseeker
{
    public void Seek(Vector2 position, TargetableAttacksEnums targetableAttacksEnums);
    public void ReachedTarget();

}