using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public static Enemy CreateEnemy(SoEnemyObject soEnemyInformationPackage, Vector3 pos)
    {
        // Initialize the enemy instance
        SceneObject newEnemyInstance = soEnemyInformationPackage.Init(pos);
        newEnemyInstance.SetStats(soEnemyInformationPackage.GetStats());
        newEnemyInstance.transform.parent = GameSceneRef.instance.enemyParent;

        // Get the Enemy component and configure it
        Enemy newEnemy = newEnemyInstance.GetComponent<Enemy>();
        SetEnemyHealthSystem(soEnemyInformationPackage, newEnemyInstance.gameObject, newEnemy);
        SetAiPathSeek(soEnemyInformationPackage, newEnemy);
        SetSpriteRenders(soEnemyInformationPackage, newEnemy);

        return newEnemy;
    }

    #region Enemy Setter Functions

    private static void SetSpriteRenders(SoEnemyObject soEnemyObj, Enemy newEnemy)
    {
        SpriteRenderer[] spriteRenderers = newEnemy.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = soEnemyObj.sprite;
        }
    }

    private static void SetAiPathSeek(SoEnemyObject soEnemyInformationPackage,  Enemy newEnemy)
    {
        // Initialize AI Path components
        Mover moverComponent = newEnemy.gameObject.AddComponent<Mover>();
        newEnemy.mover = moverComponent;
        TargeterForEnemies _targeter = newEnemy.AddComponent<TargeterForEnemies>();
        newEnemy.targeter = _targeter;
        newEnemy.targeter.Initialize(moverComponent,soEnemyInformationPackage,newEnemy);
        newEnemy.mover.Init(newEnemy.transform.GetComponent<Seeker>(), newEnemy.targeter);
        newEnemy.targeter.Seek();

    }

    private static void SetEnemyHealthSystem(SoEnemyObject soEnemyInformationPackage, GameObject newEnemyTransform, Enemy newEnemy)
    {

        newEnemy.idamageableComponent.healthSystem.SetInitialHealth(soEnemyInformationPackage.health);
        newEnemy.idamageableComponent.healthSystem.damageEffect = soEnemyInformationPackage.damageEffect;
    }

    #endregion
}
