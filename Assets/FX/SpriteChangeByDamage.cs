using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SpriteShadersUltimate;
public class SpriteChangeByDamage : MonoBehaviour
{

        

    private ShaderFaderSSU shaderFader;
    float timer = 0f;
    public float timerMax;
    public bool triggerOnDeath;
    


    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = GetComponentInParent <HealthSystem>();
        if (triggerOnDeath) // If its a death effect then it should be disabled from start
        {
            
            GetComponent<SpriteRenderer>().enabled = false;
            healthSystem.OnKilled += OnDeathTrigger;
        }
        else
        {
            healthSystem.OnKilled += RemoveGameObject;
            if (healthSystem is PhysicalHealthSystem)
                healthSystem.OnDamaged += OnTriggered;
            
      
        }
        
        shaderFader = GetComponent<ShaderFaderSSU>();
        shaderFader.isFaded = false;

    }

    private void OnDeathTrigger(object sender, OnSceneObjectDestroyedArgs e)
    {
        transform.SetParent(null);
        GetComponent<SpriteRenderer>().enabled = true;
        shaderFader.isFaded = true;
    }

    private void RemoveGameObject(object sender, OnSceneObjectDestroyedArgs e) // This triggers for normal effects when dying
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggered(object sender, OnDamageArgs e) // Triggers each time it takes damage, sender is healthsystem
    {
        StartCoroutine("ShadeFaderDelay" ); 
        
    }
    private IEnumerator ShadeFaderDelay()
    {
        yield return new WaitForSeconds(0.3f);

        shaderFader.isFaded = true;
    }
    private void Update()
    {
        if (shaderFader.isFaded)
        {
            timer += BattleClock.Instance.deltaValue;
            if (timer > timerMax )//Dont go back to normal state if dead
            {
                if (!triggerOnDeath)
                {
                    timer = 0f;
                    shaderFader.isFaded = false;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
           
        }
    }
}
