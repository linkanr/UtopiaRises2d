using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AreaDamage:MonoBehaviour
{
    private VisualEffect effect;
    private int damage;
    private float delay;
    private float diameter;
    private bool init =false;
    public static void Create(Vector3 position,float diameter, VisualEffect visualEffect, int damage, float delay)
    {
        GameObject prefab = Resources.Load("areaDamage") as GameObject;
        AreaDamage areaDamage = Instantiate(prefab, position, Quaternion.identity).GetComponent<AreaDamage>();
        areaDamage.damage = damage;
        areaDamage.delay = delay;
        areaDamage.effect = Instantiate( visualEffect,areaDamage.transform);
        areaDamage.effect.SetFloat("size",diameter);
        areaDamage.effect.Stop();
        areaDamage.diameter = diameter;
        BattleSceneActions.GlobalTimeChanged += areaDamage.UpdateDelay;
    }

    private void UpdateDelay(BattleSceneTimeArgs args)
    {
        delay -= args.deltaTime;
        if (delay <= 0 && init ==false)
        {
            init = true;
            StartCoroutine(StartAreaDamage());
        }
    }

    private void Update()
    {

    }
    IEnumerator StartAreaDamage()
    {
        yield return PlayEffect();
        DealDamage(damage);
        //yield return WaitForEffectToFinish();
        Debug.Log("destroying object");
        //Destroy(gameObject);

    }

    private IEnumerator WaitForEffectToFinish()
    {
        Debug.Log("waiting for effect");

        Debug.Log("effect finnished");
        yield return null;
    }

    private void DealDamage(int damage)
    {
        Debug.Log("dealing damage");
        List<IDamageable> list = new List<IDamageable> ();
        list = SceneObjectManager.Instance.sceneObjectGetter.GetIdamagables(transform.position, diameter/2);//Get IDamages uses radius
        foreach (IDamageable a in list) 
        {
            Debug.Log(a.sceneObject.GetStats().GetString(StatsInfoTypeEnum.name));
            a.TakeDamage(damage);
        }
    }

    IEnumerator PlayEffect()
    {
        Debug.Log("playing effect");
        effect.Play();
        yield return null;
    }
}