using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public static Enemy CreateEnemy(SoEnemyObject soEnemyInformationPackage, Vector3 pos)
    {
        // Initialize the enemy instance
        SceneObject newEnemyInstance = soEnemyInformationPackage.Init(pos);
        Enemy newEnemy = newEnemyInstance as Enemy;
        newEnemy.SetStats(soEnemyInformationPackage.GetStats());
        newEnemy.transform.parent = GameSceneRef.instance.enemyParent;

        // Get the Enemy component and configure it

        SetEnemyHealthSystem(soEnemyInformationPackage, newEnemy);
        SetAiPathSeek(soEnemyInformationPackage, newEnemy);
        SetSpriteRenders(soEnemyInformationPackage, newEnemy);

        return newEnemy;
    }

    #region Enemy Setter Functions
    private static void SetEnemyHealthSystem(SoEnemyObject soEnemyInformationPackage, Enemy newEnemy)
    {

        newEnemy.idamageableComponent.healthSystem.SetInitialHealth(soEnemyInformationPackage.health);
        newEnemy.idamageableComponent.healthSystem.damageEffect = soEnemyInformationPackage.damageEffect;
    }

    private static void SetAiPathSeek(SoEnemyObject soEnemyInformationPackage, Enemy newEnemy)
    {
        // Initialize AI Path components
        Mover moverComponent = newEnemy.gameObject.AddComponent<Mover>();
        newEnemy.mover = moverComponent;
        TargeterForEnemies _targeter = newEnemy.AddComponent<TargeterForEnemies>();
        newEnemy.targeter = _targeter;
        newEnemy.targeter.Initialize(newEnemy, soEnemyInformationPackage.seekSystem, soEnemyInformationPackage.possibleTargetTypes, soEnemyInformationPackage.attackSystem, moverComponent);
        newEnemy.mover.Init(newEnemy.transform.GetComponent<Seeker>(), newEnemy.targeter);
        newEnemy.targeter.Seek();

    }
    private static void SetSpriteRenders(SoEnemyObject soEnemyObj, Enemy newEnemy)
    {
        Transform spritePartent = newEnemy.transform.Find("EnemyMovment");


        SpriteRenderer[] spriteRenderers = spritePartent.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = soEnemyObj.sprite;
        }
    }




    #endregion
}
