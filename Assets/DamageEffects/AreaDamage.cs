using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AreaDamage : MonoBehaviour
{
    private VisualEffect effect;
    private int damage;
    private float delay;
    private float diameter;
    private bool init = false;
    public static void Create(Vector3 position, float diameter, VisualEffect visualEffect, int damage, float delay)
    {
        GameObject prefab = Resources.Load("areaDamage") as GameObject;
        AreaDamage areaDamage = Instantiate(prefab, position, Quaternion.identity).GetComponent<AreaDamage>();
        areaDamage.damage = damage;
        areaDamage.delay = delay;
        areaDamage.effect = Instantiate(visualEffect, areaDamage.transform);
        areaDamage.effect.SetFloat("size", diameter);
        areaDamage.diameter = diameter;
        if (areaDamage.delay > 0.01)
        {
            areaDamage.effect.Stop();
            TimeActions.GlobalTimeChanged += areaDamage.UpdateDelay;
        }
        else
        {

            areaDamage.UpdateDelay(new BattleSceneTimeArgs { deltaTime = 1f });
        }


    }

    private void UpdateDelay(BattleSceneTimeArgs args)
    {
        delay -= args.deltaTime;
        if (delay <= 0 && init == false)
        {
            init = true;
            StartCoroutine(StartAreaDamage());
        }
    }


    IEnumerator StartAreaDamage()
    {
        yield return PlayEffect();
        DealDamage(damage);
        Destroy(gameObject);

    }



    private void DealDamage(int damage)
    {
        Debug.Log("dealing damage");
        List<SceneObject> list = new List<SceneObject>();
        list = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(transform.position, maxDistance: diameter / 2, onlyDamageables: true);//Get IDamages uses radius
        foreach (SceneObject a in list)
        {
            Debug.Log(a.GetStats().GetString(StatsInfoTypeEnum.name));
            IDamageAble damageable = a as IDamageAble;
            damageable.idamageableComponent.TakeDamage(damage);
        }
    }

    IEnumerator PlayEffect()
    {
        Debug.Log("playing effect");
        effect.Play();
        yield return new WaitForSeconds(.5f);

        while (effect.aliveParticleCount > 0)
        {
            yield return null; // Wait for one frame and check again
        }


    }
}