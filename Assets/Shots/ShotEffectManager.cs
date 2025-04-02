using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using System.Collections.Generic;

public class ShotEffectManager : MonoBehaviour
{
    public static ShotEffectManager Instance { get; private set; }

    [System.Serializable]
    public class EffectConfig
    {
        public ShotEffectTypeEnum type;

        public Sprite sprite;                        // For projectile
        public VisualEffectAsset vfxMuzzle;         // VFX at start (tower)
        public VisualEffectAsset vfxImpact;         // VFX at target
        public float speed = 50f;
        public float impactDelay = 0.2f;
    }

    [Tooltip("Define settings for each shot type")]
    public EffectConfig[] effectConfigs;

    [Tooltip("Shared pooled prefab (must have SpriteRenderer + VisualEffect + ShotEffectObject)")]
    public ShotEffectObject sharedPrefab;

    private Dictionary<ShotEffectTypeEnum, EffectConfig> configMap;
    private Dictionary<ShotEffectTypeEnum, Queue<ShotEffectObject>> pool;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        configMap = new Dictionary<ShotEffectTypeEnum, EffectConfig>();
        pool = new Dictionary<ShotEffectTypeEnum, Queue<ShotEffectObject>>();

        foreach (var config in effectConfigs)
        {
            if (config != null)
            {
                configMap[config.type] = config;
                pool[config.type] = new Queue<ShotEffectObject>();
            }
        }
    }

    public void PlayShotEffect(ShotEffectTypeEnum type, Vector3 from, Vector3 to)
    {
        if (!configMap.TryGetValue(type, out var config))
        {
            Debug.LogWarning($"ShotEffectType {type} not configured.");
            return;
        }

        // 🔹 Muzzle VFX (at from)
        if (config.vfxMuzzle != null)
        {
            var muzzle = GetOrCreate(type);
            SetupEffectObject(muzzle, null, config.vfxMuzzle);
            muzzle.effectType = type;
            muzzle.Initialize(from, to, 0f);
        }

        // 🔹 Sprite projectile (from → to)
        if (config.sprite != null)
        {
            var projectile = GetOrCreate(type);
            SetupEffectObject(projectile, config.sprite, null);
            projectile.effectType = type;
            projectile.Initialize(from, to, config.speed);
        }

        // 🔹 Delayed impact VFX (at to)
        if (config.vfxImpact != null)
        {
            StartCoroutine(SpawnImpactVFX(type,from, to, config.vfxImpact, config.impactDelay));
        }
    }

    private IEnumerator SpawnImpactVFX(ShotEffectTypeEnum type,Vector3 from, Vector3 to, VisualEffectAsset vfxAsset, float delay)
    {
        yield return new WaitForSeconds(delay);

        var impact = GetOrCreate(type);
        SetupEffectObject(impact, null, vfxAsset);
        impact.effectType = type;
        impact.Initialize(to,from, 0f);
    }

    private void SetupEffectObject(ShotEffectObject obj, Sprite sprite, VisualEffectAsset vfxAsset)
    {
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = sprite;
            sr.enabled = sprite != null;
        }

        var vfx = obj.GetComponent<VisualEffect>();
        if (vfx != null)
        {
            vfx.visualEffectAsset = vfxAsset;
            vfx.enabled = vfxAsset != null;
        }
    }

    private ShotEffectObject GetOrCreate(ShotEffectTypeEnum type)
    {
        if (!pool.TryGetValue(type, out var queue))
        {
            queue = new Queue<ShotEffectObject>();
            pool[type] = queue;
        }

        if (queue.Count > 0)
        {
            return queue.Dequeue();
        }

        var instance = Instantiate(sharedPrefab);
        return instance;
    }

    public void ReturnToPool(ShotEffectObject obj)
    {
        obj.gameObject.SetActive(false);

        var type = obj.effectType;
        if (!pool.ContainsKey(type))
        {
            pool[type] = new Queue<ShotEffectObject>();
        }

        pool[type].Enqueue(obj);
    }
}
