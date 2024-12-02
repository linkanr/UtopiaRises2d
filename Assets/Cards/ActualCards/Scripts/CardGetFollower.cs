using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Cards/GetFollower")]
public class CardGetFollower : SoCardInstanciate
{
   
    public override void ActualEffect(Vector3 position)
    {
        prefab.Init(GameSceneRef.instance.goalPosition.position); 
    }
}