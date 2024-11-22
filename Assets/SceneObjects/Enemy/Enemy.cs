using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Enemy : MovingSceneObject,IDamageable
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

    public SoEnemySeekSystem enemySeekSystem;
    public SoEnemyAttackSystem enemyAttackSystem;
    public Transform currentTarget;
    [HideInInspector]
    private AIPath aIPath;
    [HideInInspector]
    private Sprite displaySprite;
    public HealthSystem healthSystem { get { return protectedHealthsystem; } set {  } }

 

    public event EventHandler<OnDeathArgs> OnDeath;


    public static Enemy Create(SoEnemyObject soEnemyInformationPackage, Vector3 pos, Quaternion rotation  )
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
        BattleSceneActions.OnTargetableDestroyed -= CheckIfTargetIsDead;
    }
    protected override void  OnEnable()
    {
        base.OnEnable();
        BattleSceneActions.OnTargetableDestroyed += CheckIfTargetIsDead;
    }
    protected override void Start()
    {
        base.Start();
        EnemyManager.Instance.spawnedEnemiesList.Add(this);
        lookForNewMTargetMaxTime = enemySeekSystem.lookForNewTargetTime;
        enemySeekSystem.Seek(transform.position, enemyAttackSystem.enemyAttackType);

        
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

    private void CheckIfTargetIsDead(ITargetableByEnemy target)
    {
        if (target==null || target.GetTransform() == null)
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
        currentTarget = iDamageableTransform;
        destinationSetter.target = iDamageableTransform;
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
        OnDeath?.Invoke(this, new OnDeathArgs { damageable = this });
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






}
