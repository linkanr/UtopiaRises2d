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
    private float burnChance;
    private bool init = false;
    public static void Create(Vector3 position, float diameter, VisualEffect visualEffect, int damage, float delay, float burnChance = 0f)
    {
        GameObject prefab = Resources.Load("areaDamage") as GameObject;
        position.z = -0.05f;
        AreaDamage areaDamage = Instantiate(prefab, position, Quaternion.identity).GetComponent<AreaDamage>();
        areaDamage.damage = damage;
        areaDamage.delay = delay;
        areaDamage.effect = Instantiate(visualEffect, areaDamage.transform);
        areaDamage.effect.SetFloat("size", diameter);
        areaDamage.diameter = diameter;
        areaDamage.burnChance = burnChance;
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

       
        Destroy(gameObject);

    }

    private void AddFire()
    {
 
        if (burnChance > 0)
        {
            Debug.Log("adding fire burn chance over zero");
            Cell[] cells = GridCellManager.instance.gridConstrution.GetCellListByWorldPosition(transform.position, (int)diameter / 2, (int)diameter / 2).ToArray();
            foreach (Cell cell in cells)
            {
                Debug.Log("Looping over cells");
                if (UnityEngine.Random.Range(0f, 1f) < burnChance)
                {
                    Debug.Log("adding fire to cell");
                    cell.CreateCellEffect(CellEffectEnum.Fire);
                }
            }
        }
    }

    private void DealDamage(int damage)
    {
        //  Debug.Log("dealing damage");
        List<SceneObject> list = new List<SceneObject>();
        Debug.Log("dealing damage" + transform.position);
        list = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(transform.position, maxDistance: diameter / 2, onlyDamageables: true);//Get IDamages uses radius
        foreach (SceneObject a in list)
        {
            Debug.Log(a.GetStats().GetString(StatsInfoTypeEnum.name));
            
            a.healthSystem.TakeDamage(damage, null);
        }

    }

    IEnumerator PlayEffect()
    {

        effect.Play();

        yield return new WaitForSeconds(1f);
        AddFire();
        DealDamage(damage);
        while (effect.aliveParticleCount > 0)
        {
            yield return new WaitForSeconds(.1f);
        }


    }
}