using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;
    public int health;

    private void OnEnable()
    {
        GlobalActions.DoBaseDamage += TakeDamage;
    }
    private void OnDisable()
    {
       GlobalActions.DoBaseDamage -= TakeDamage;
    }



    private void Awake()
    {
        SetLife(100);
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("double trounge");
    }
    public void TakeDamage(int damage)
    {
        
        health -= damage;
        GlobalActions.OnLifeChange(health);
        if (health < 0)
        {
            Debug.Log("dead");
            GlobalActions.Dead?.Invoke();
        }
    }
    private void SetLife(int _health)
    {
        health = _health;
    }
}
