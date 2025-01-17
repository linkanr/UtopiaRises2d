using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "Cards/FacistPropaganda")]

public class CardScapegoating : SoCardBase, IHasClickEffect

{

    public float lifetime;
    public float radius;
    //public VisualEffect visualEffect; add this later
    public Sprite clickSprite;

    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        Vector3 pos = GridCellManager.Instance.gridConstrution.GetCellPositionByPosition(position);
        //Debug.Log("Applying slow to all enemies at position " + position);
        List<SceneObject> objects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(pos,objectTypeEnum:SceneObjectTypeEnum.enemy, maxDistance: radius, onlyDamageables: true);
        foreach (SceneObject obj in objects)
         {
            //Debug.Log("Applying slow to " + obj.GetStats().GetString(StatsInfoTypeEnum.name));
            EffectAdd.AddEffect(PickupTypes.Weak, obj,lifetime);
         }
       
        failureReason = "";
        return true;
    }

    public Sprite GetSprite()
    {
        return clickSprite;
    }

    public float GetSpriteSize()
    {
        return radius*2;
    }
}
