using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingSceneObject, IDamageable
{

    [HideInInspector]

    private BasicHealthSystem protectedHealthsystem;
    private bool isDead;
    private AIDestinationSetter destinationSetter;
    [HideInInspector]
    public float lookForNewMTargetMaxTime;
    [HideInInspector]
    public float lookForNewTargetTime;
    [HideInInspector]
    public float attackTimer;
    [HideInInspector]
    public SceneObject sceneObject { get { return this; } }
    public NonStaticSeekSystem enemySeekSystem;
    public SoAttackSystem enemyAttackSystem;
    public Transform currentTarget;
    [HideInInspector]
    private AIPath aIPath;
    [HideInInspector]
    private Sprite displaySprite;
    [HideInInspector]
    public List<iDamageableTypeEnum> possibleTargetTypes;
    public HealthSystem healthSystem { get { return protectedHealthsystem; } set { } }

    public iDamageableTypeEnum damageableType { get { return iDamageableTypeEnum.enemy; } }

    public event EventHandler<IdamageAbleArgs> OnDeath;


    public static Enemy Create(SoEnemyObject soEnemyInformationPackage, Vector3 pos, Quaternion rotation)
    {
        SceneObject newEnemyInstance = soEnemyInformationPackage.Init(pos);
        newEnemyInstance.SetStats(soEnemyInformationPackage.GetStats());
        newEnemyInstance.transform.parent = GameSceneRef.instance.enemyParent;

        Enemy newEnemy = newEnemyInstance.GetComponent<Enemy>();
        SetEnemyHealthSystem(soEnemyInformationPackage, newEnemyInstance.gameObject, newEnemy);

        SetAiPathSeek(soEnemyInformationPackage, newEnemyInstance.gameObject, newEnemy);
        SetSpriteRenders(soEnemyInformationPackage, newEnemyInstance.gameObject);

        return newEnemy;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        enemySeekSystem.OnNewTarget -= SetNewTarget;
        BattleSceneActions.OnDamagableDestroyed -= CheckIfTargetIsDead;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        BattleSceneActions.OnDamagableDestroyed += CheckIfTargetIsDead;
    }
    protected override void Start()
    {
        base.Start();
        EnemyManager.Instance.spawnedEnemiesList.Add(this);
        lookForNewMTargetMaxTime = enemySeekSystem.lookForNewTargetTime;
        enemySeekSystem.Seek(transform.position, possibleTargetTypes);

    }

    #region Enemy Setter funcitons
    private static void SetSpriteRenders(SoEnemyObject soEnemyObj, GameObject newEnemyTransform)
    {
        SpriteRenderer[] spriteRenderers = newEnemyTransform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = soEnemyObj.sprite;
        }
    }
    private static void SetAiPathSeek(SoEnemyObject soEnemyInformationPackage, GameObject newEnemyTransform, Enemy newEnemy)
    {
        newEnemy.destinationSetter = newEnemyTransform.GetComponent<AIDestinationSetter>();
        newEnemy.aIPath = newEnemyTransform.GetComponent<AIPath>();
        newEnemy.enemyAttackSystem = Instantiate(soEnemyInformationPackage.attackSystem);
        newEnemy.possibleTargetTypes = soEnemyInformationPackage.possibleTargetTypes;
        newEnemy.aIPath.endReachedDistance = newEnemy.enemyAttackSystem.maxRange * .9f;
        newEnemy.aIPath.maxSpeed = soEnemyInformationPackage.seekSystem.speed;
        newEnemy.enemySeekSystem = Instantiate(soEnemyInformationPackage.seekSystem);
        newEnemy.enemySeekSystem.OnNewTarget += newEnemy.SetNewTarget;
    }

    private static void SetEnemyHealthSystem(SoEnemyObject soEnemyInformationPackage, GameObject newEnemyTransform, Enemy newEnemy)
    {
        newEnemy.SetHealthSystem(newEnemyTransform.GetComponent<HealthSystem>());
        newEnemy.healthSystem.SetInitialHealth(soEnemyInformationPackage.health);
        newEnemy.healthSystem.damageEffect = soEnemyInformationPackage.damageEffect;
    }

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        protectedHealthsystem = healthSystem as BasicHealthSystem;

    }
    #endregion

    private void CheckIfTargetIsDead(IDamageable target)
    {
        if (target == null || target.GetTransform() == null)
        {
            currentTarget = null;
        }
    }



    public void Die()
    {
        if (IsDead())
        {
            return;
        }


        DestroySceneObject();


    }
    private void SetNewTarget(Transform iDamageableTransform)
    {
        IDamageable damageable = iDamageableTransform.GetComponent<IDamageable>();
        damageable.OnDeath += RemomveTarget;
        currentTarget = iDamageableTransform;
        destinationSetter.target = iDamageableTransform;
    }

    private void RemomveTarget(object sender, IdamageAbleArgs e)
    {
        currentTarget=null;
        e.damageable.OnDeath -= RemomveTarget;
    }

    public Transform GetTransform()
    {
        if (!IsDead())
        {
            return transform;
        }
        else
        {
            return null;
        }

    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead())
        {
            return;
        }

        bool hasDied = healthSystem.TakeDamage(amount);
        if (hasDied)
        {
            Die();
        }
    }
    protected override void OnObjectDestroyed()
    {
        isDead = true;
        OnDeath?.Invoke(this, new IdamageAbleArgs { damageable = this });
        EnemyManager.Instance.spawnedEnemiesList.Remove(this);

        Destroy(gameObject);
    }

    internal bool CheckIfTargetIsInDistance()
    {
        if (Vector2.Distance(currentTarget.position, transform.position) < enemyAttackSystem.maxRange)
        {
            return true;
        }
        return false;
    }
    internal bool CheckIfHasTarget()
    {
        if (currentTarget != null)
        {
            return true;
        }
        return false;
    }

    public void OnCreated()
    {
        BattleSceneActions.OnDamagableCreated(this);
    }
}
