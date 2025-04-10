using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Represents a visual projectile effect (sprite or VFX).
/// Moves toward a target if sprite, plays at position if VFX.
/// </summary>
public class ShotEffectObject : MonoBehaviour
{
    public ShotEffectTypeEnum effectType;

    [Header("Optional Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private VisualEffect visualEffect;

    private Vector3 targetPosition;
    private float moveSpeed = 10f;
    private bool isSpriteActive;
    private bool isVFXActive;
    private bool vfxHasPlayed;
    private float spriteLifetime = 0.18f;
    private float timer = 0f;

    public void Initialize(Vector3 from, Vector3 to, float speed)
    {
   

        Vector3 offset = new Vector3(0, 0, -0.2f);
        gameObject.SetActive(true);
        moveSpeed = speed;

        isSpriteActive = spriteRenderer != null && spriteRenderer.enabled;
        isVFXActive = visualEffect != null && visualEffect.enabled;
        vfxHasPlayed = false;

        if (isSpriteActive)
        {
            transform.position = from + offset;
            targetPosition = to + offset;

            Vector3 dir = to - from;
            if (dir.sqrMagnitude > 0.001f)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        if (isVFXActive && visualEffect != null)
        {
            transform.position = from + offset;

            Vector3 dir = to - from;
            if (dir.sqrMagnitude < 0.001f)
            {
                dir = Vector3.forward; // default direction
            }
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            visualEffect.SetVector3("Direction", dir);

           // Debug.Log("VFX Direction: " + dir + " to " + to + " from " + from);
            visualEffect.Stop();
            visualEffect.Play();
        }
        else
        {
            Debug.Log("VFX is " + isVFXActive + " active, is null? " + (visualEffect != null));
        }
    }

    void Update()
    {
        if (isSpriteActive)
        {
            float step = moveSpeed * Time.deltaTime;

            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, step);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (timer < spriteLifetime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
                ShotEffectManager.Instance.ReturnToPool(this);
                transform.localScale = Vector3.one;
            }

            if ((transform.position - targetPosition).sqrMagnitude < 0.05f)
            {
                ShotEffectManager.Instance.ReturnToPool(this);
                transform.localScale = Vector3.one;
            }
        }

        if (isVFXActive && visualEffect != null)
        {
            if (!vfxHasPlayed && visualEffect.aliveParticleCount > 0)
            {
                vfxHasPlayed = true;
            }
            else if (vfxHasPlayed && visualEffect.aliveParticleCount == 0)
            {
                ShotEffectManager.Instance.ReturnToPool(this);
            }
        }
    }
}
