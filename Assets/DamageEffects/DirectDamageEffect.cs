using Unity.VisualScripting;
using UnityEngine;

public class DirectDamageEffect : MonoBehaviour
{
    IDamageAble damageable;
    /// <summary>
    /// Timer until next triger
    /// </summary>
    float timer;
    /// <summary>
    /// the amount of times it will be exec
    /// </summary>
    int timesleft;
    /// <summary>
    /// damage amount
    /// </summary>
    int amount;
    /// <summary>
    /// how long between each trigger
    /// </summary>
    float delay;
    /// <summary>
    /// Creates a damage effect with a certail delay and amount of times
    /// </summary>
    /// <param name="target">the target of the damage</param>
    /// <param name="_amount">the amount of dagamge</param>
    /// <param name="_delay">time before executed</param>
    /// <param name="_numberOfTimes">amount of times to exectute</param>
    public static bool Create(SceneObject target, int _amount, float _delay = 0.01f, int _numberOfTimes =1)
    {
        if (target is not IDamageAble)
        {
            return false;

        }
        DirectDamageEffect directDamageEffect = target.transform.AddComponent<DirectDamageEffect>();
        directDamageEffect.damageable = target as IDamageAble;
        directDamageEffect.amount = _amount;
        directDamageEffect.delay = _delay;
        directDamageEffect.timer = 0f;
        directDamageEffect.timesleft = _numberOfTimes;
        if (directDamageEffect.delay < 0.01)
        {
            directDamageEffect.Execute();
        }

        return true;
    }
    private void Update()
    {
        timer += BattleClock.Instance.deltaValue;
        if (timer > delay)
        {
            Execute();

        }
    }

    private void Execute()
    {
        damageable.iDamageableComponent.TakeDamage(amount);
        timesleft--;
        timer = 0f;
        if (timesleft < 1)
        {
            Destroy(this);
        }
    }
}
