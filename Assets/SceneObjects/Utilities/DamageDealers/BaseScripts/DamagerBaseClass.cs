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
        Debug.Log("DamagerBaseClass Init");
        sceneObjectParent = _sceneObject;
        InitImplemantation();
    }
    public abstract void InitImplemantation();

    public int CaclulateDamage()
    {
        return CalculateDamageImplementation( PlayerGlobalsManager.instance.playerGlobalVariables.GetDamage(baseDamage));
    }

    public abstract float CalculateReloadTime();

    public abstract float CalculateAttackRange();
    public abstract int CalculateDamageImplementation(int _baseDamage);

    public abstract DamagerBaseClass Clone();



}
