using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Cards/GetFollower")]
public class CardGetFollower : SoCardInstanciate
{
   
    public override void ActualEffect(Vector3 position)
    {
        Instantiate(prefab, GameSceneRef.instance.goalPosition.position, Quaternion.identity);
    }
}