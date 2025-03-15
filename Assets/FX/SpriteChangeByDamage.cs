using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SpriteShadersUltimate;
public class SpriteChangeByDamage : MonoBehaviour
{
    private IDamageAble damageable;
        

    private ShaderFaderSSU shaderFader;
    float timer = 0f;
    public float timerMax;
    public bool triggerOnDeath;
    


    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponentInParent<IDamageAble>();
        if (triggerOnDeath) // If its a death effect then it should be disabled from start
        {
            
            GetComponent<SpriteRenderer>().enabled = false;
            damageable.iDamageableComponent.OnDeath += OnDeathTrigger;
        }
        else
        {
            damageable.iDamageableComponent.OnDeath += RemoveGameObject;
            if (damageable.iDamageableComponent is IdamagablePhysicalComponent)
            {
                (damageable.iDamageableComponent as IdamagablePhysicalComponent).healthSystem.OnDamaged += OnTriggered;
            }
      
        }
        
        shaderFader = GetComponent<ShaderFaderSSU>();
        shaderFader.isFaded = false;

    }

    private void OnDeathTrigger(object sender, IdamageAbleArgs e)
    {
        transform.SetParent(null);
        GetComponent<SpriteRenderer>().enabled = true;
        shaderFader.isFaded = true;
    }

    private void RemoveGameObject(object sender, IdamageAbleArgs e) // This triggers for normal effects when dying
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggered(object sender, EventArgs e) // Triggers each time it takes damage, sender is healthsystem
    {

        
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
