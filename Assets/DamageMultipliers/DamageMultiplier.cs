using UnityEngine;

public abstract class DamageMultiplier:ScriptableObject
{

    public abstract int GetExtraDamageAmount(int damage,SceneObject target);

}