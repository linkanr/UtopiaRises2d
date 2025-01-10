using UnityEngine;
[CreateAssetMenu(menuName = "Cards/DirectAttack")]

public class CardDirectAttack : SoCardBase
{
    public int damagaAmount;
    public float delay;
    public int amountOfTimes;
    public DamageMultiplier damageMultiplier;
   

    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        failureReason = "";
        SceneObject sceneObject = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(position, maxDistance: 2, onlyDamageables:true);
        if (sceneObject == null) 
        {
            failureReason = "No target found";
            return false;
        }
        if (sceneObject is not IDamageAble)
        {
            failureReason = "cant play it on that target";
            return false;
        }
        if (damageMultiplier != null)
        {
            Debug.Log("incoming damage" + damagaAmount);
            damagaAmount= damageMultiplier.GetExtraDamageAmount(damagaAmount, sceneObject);
            Debug.Log("outgoing damage" + damagaAmount);
        }
           
        DirectDamageEffect.Create(SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(position), damagaAmount, delay, amountOfTimes);
        return true;
    }
}
public enum ExtraDamageAgainstEnum
{
  none,
  sceneObjectType,
  faction
}