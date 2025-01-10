using UnityEngine;
[CreateAssetMenu(menuName = "Cards/GetFollower")]
public class CardGetFollower : SoCardInstanciate
{

    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        failureReason = "";
        prefab.Init(GameSceneRef.instance.followerBirthPlace.position); 
        return true;
    }
}