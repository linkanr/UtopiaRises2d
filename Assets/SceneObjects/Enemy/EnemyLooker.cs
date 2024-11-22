
using UnityEngine;
using static ShootingBuilding;

public class EnemyLooker
{
    public EnemyLooker(EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
    }
    public EnemyManager enemyManager;

    internal IDamageable LookForTarget(LookForEnemyType lookForEnemyType, Vector3 postion, float maxDistance)
    {
        switch (lookForEnemyType)
        {
            case LookForEnemyType.closest:
                {
                    return GetClosestEnemy(postion, maxDistance);
                }
            case LookForEnemyType.mostHelth:
                {
                    return GetEnemyMostHealth(postion, maxDistance);
                }
            case LookForEnemyType.furthest:
                {
                    return GetFurthestEnemy(postion, maxDistance);

                }
            case LookForEnemyType.leastHealth:
                {
                    return GetEnemyLeastHealth(postion, maxDistance);
                }
        }
        return null;
    }
    public Enemy GetClosestEnemy(Vector3 pos, float maxDistance)
    {
        float dist = Mathf.Infinity;
        Enemy closeEnemy = null;
        foreach (Enemy enemy in enemyManager.spawnedEnemiesList)
        {
            float newDist = Vector3.Distance(enemy.transform.position, pos);
            if (newDist < dist && newDist < maxDistance)
            {
                dist = newDist;
                closeEnemy = enemy;
            }
        }
        return closeEnemy;

    }
    public Enemy GetFurthestEnemy(Vector3 pos, float maxDistance)
    {
        float dist = 0;
        Enemy closeEnemy = null;
        foreach (Enemy enemy in enemyManager.spawnedEnemiesList)
        {
            float newDist = Vector3.Distance(enemy.transform.position, pos);
            if (newDist > dist && newDist < maxDistance)
            {
                dist = newDist;
                closeEnemy = enemy;
            }
        }
        return closeEnemy;
    }

    public Enemy GetEnemyMostHealth(Vector3 pos, float maxDistance)
    {
        int health = -1;
        Enemy returnEnemy = null;

        foreach (Enemy enemy in enemyManager.spawnedEnemiesList)
        {
            float newDist = Vector3.Distance(enemy.transform.position, pos);
            int newHealth = enemy.healthSystem.GetHealth();
            if (newHealth > health && newDist < maxDistance)
            {
                returnEnemy = enemy;
            }

        }
        return returnEnemy;
    }
    public Enemy GetEnemyLeastHealth(Vector3 pos, float maxDistance)
    {
        int health = 1000000;
        Enemy returnEnemy = null;
        foreach (Enemy enemy in enemyManager.spawnedEnemiesList)
        {
            float newDist = Vector3.Distance(enemy.transform.position, pos);
            int newHealth = enemy.healthSystem.GetHealth();
            if (newHealth < health && newDist < maxDistance)
            {
                returnEnemy = enemy;
            }

        }
        return returnEnemy;
    }
}
