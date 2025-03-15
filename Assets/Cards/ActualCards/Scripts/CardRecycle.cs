using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Recycle")]
public class CardRecycle: SoCardBase
{
    public int drawCardAmount;
    public override bool ActualEffect(Vector3 position, out string failuerReason)
    {
        failuerReason = "";
        List<SceneObject> sceneObject = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(position, objectTypeEnum: SceneObjectTypeEnum.playerbuilding);
        GeneralUtils.ShuffleList(sceneObject);
        if (sceneObject.Count == 0)
        {
            failuerReason = "No buildings to recycle";
            return false;
        }
        else
        {
            sceneObject[0].KillSceneObject();
            CardsInPlayManager.instance.DrawCards(drawCardAmount);
            return true;
        }
    }
}