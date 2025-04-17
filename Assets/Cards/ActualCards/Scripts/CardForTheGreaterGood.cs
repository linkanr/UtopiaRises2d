using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "Cards/ForTheGreaterGood")]

public class CardForTheGreaterGood : SoCardBase, IHasClickEffect

{

    public float lifetime;
    public float radius;
    //public VisualEffect visualEffect; add this later
    public Sprite clickSprite;

    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        var potentialBuidlings = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(Vector3.zero, objectTypeEnum: SceneObjectTypeEnum.playerbuilding);
        List<SceneObject> buildings = new List<SceneObject>();
        foreach (var building in potentialBuidlings)
        {
            if (building.GetStats().faction.politicalAlignment.isSpiritual() || building.GetStats().faction.politicalAlignment.isLeft())
            {
                buildings.Add(building);
            }
        }
        if (buildings.Count == 0)
        {
            failureReason = "No buildings to apply the effect";
            return false;
        }
        else
        {
            GeneralUtils.ShuffleList(buildings);
            buildings[0].KillSceneObject();
        }
        Vector3 pos = GridCellManager.instance.gridConstrution.GetCellPositionByPosition(position);
        List<SceneObject> objects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(pos, objectTypeEnum: SceneObjectTypeEnum.playerbuilding, maxDistance: radius);
        foreach (SceneObject obj in objects)
        {
            //Debug.Log("Applying slow to " + obj.GetStats().GetString(StatsInfoTypeEnum.name));
            PickupEffectAdd.AddEffect(PickupTypes.Rage, obj, lifetime);
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
        return radius * 2;
    }
}
