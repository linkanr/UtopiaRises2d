using UnityEditor.Build;
using UnityEngine;

public interface ITargetableByEnemy // Basic interface that lets enemy find you
{


    public void OnTargetableByEnemyDestroyed();
    public void OnTargetableByEnemyCreated(); // adds to the list in playerBuildingsManager
    public Transform GetTransform();// returns transform of gameobject
    public TargetableAttacksEnums TargatebleEnum();
}