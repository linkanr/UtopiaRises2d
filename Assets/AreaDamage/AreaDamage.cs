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
    private float diameter;
    private float burnChance;

    public static void Create(Vector3 position, float diameter, visualEffectsEnum visualEffect, int damage, float delay, float burnChance = 0f)
    {
        GameObject prefab = Resources.Load("areaDamage") as GameObject;
        position.z = -0.05f;
        AreaDamage areaDamage = Instantiate(prefab, position, Quaternion.identity).GetComponent<AreaDamage>();
        areaDamage.damage = damage;
        areaDamage.effect = VisualEffectManager.PlayVisualEffect(visualEffect,position);
        areaDamage.effect.SetFloat("size", diameter);
        areaDamage.diameter = diameter;
        areaDamage.burnChance = burnChance;
        Debug.Log("Created area damage" + areaDamage);
        
        areaDamage.Init();


    }



    public void Init()
    {
        StartCoroutine(StartAreaDamage());
    }
    IEnumerator StartAreaDamage()
    {
        Debug.Log("starting area damage");
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
        Debug.Log("dealing damage");
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




        AddFire();
        DealDamage(damage);

        yield return null;


    }
}