using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[System.Serializable]
public abstract class DamagerBaseClass
{

    public int baseDamage;
    public float reloadTime;
    public float attackRange;
    public SceneObject sceneObjectParent;

    public void Init(SceneObject _sceneObject)
    {
        sceneObjectParent = _sceneObject;
        InitImplemantation(_sceneObject);
    }
    public abstract void InitImplemantation(SceneObject _sceneObject);

    public virtual int CaclulateDamage(Stats stats)
    {
        return CalculateDamageImplementation(
            PlayerGlobalsManager.instance.playerGlobalVariables.GetDamage(baseDamage, stats.faction)
        );
    }
    public abstract float CalculateReloadTime();

    public virtual float CalculateAttackRange(Stats stats)
    {

        return PlayerGlobalsManager.instance.playerGlobalVariables.GetRange(attackRange, stats.faction);

    }
    public abstract int CalculateDamageImplementation(int _baseDamage);

    public abstract DamagerBaseClass Clone();



}
