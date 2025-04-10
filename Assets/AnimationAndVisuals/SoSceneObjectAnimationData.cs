using UnityEngine;

[CreateAssetMenu(menuName = "AnimationData")]
public class SoSceneObjectAnimationData : ScriptableObject
{
    [Header("Sprite Sheet Keys (from Resources)")]
    public string birthAnimationKey;
    public string idleAnimationKey;
    public string actionAnimationKey;
    public string deathAnimationKey;
    public bool loopAction;
    public bool actionIs8Sliced;
    public float frameRate = 12f;

}
