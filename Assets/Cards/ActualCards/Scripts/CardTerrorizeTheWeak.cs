using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "Cards/TerrorizeTheWeak")]

public class CardTerrorizeTheWeak : SoCardBase

{
    public int damage;

    //public VisualEffect visualEffect; add this later


    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        Debug.Log("ActualEffect called with position: " + position);
        List<SceneObject> damageAbles = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(position, onlyDamageables: true);
        Debug.Log("Found " + damageAbles.Count + " damageable objects.");
        List<SceneObject> weak = SceneObjectManager.Instance.sceneObjectGetter.FilterBasedOnModifers(damageAbles, PickupTypes.Weak);
        Debug.Log("Filtered " + weak.Count + " weak objects.");
        foreach (SceneObject obj in weak)
        {
            
            if (obj != null)
            {
                Debug.Log("Applying damage to object: " + obj.name);
                obj.healthSystem.TakeDamage(damage, null);
            }
            else
            {
                Debug.LogWarning("Object " + obj.name + " does not implement IDamageAble.");
            }
        }
        failureReason = "";
        return true;
    }
}
