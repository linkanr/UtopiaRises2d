using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField]    private Transform barTransform;
    [SerializeField] private Transform healthBarTransform;
    [SerializeField] private Transform baseTransform;// this is the main subjects transform
    [SerializeField] private bool active = false;
    [SerializeField] private bool showNumbers =false;
    [SerializeField] private TextMeshProUGUI healthText;



    protected virtual void OnEnable()
    {
        if (active)
        {
            healthBarTransform.gameObject.SetActive(true);
        }
        else
        {
            healthBarTransform.gameObject.SetActive(false);
        }
          

        


    }
    private void OnDisable()
    {
 
        healthSystem.OnDamaged -= HealthSystem_OnDamaged;
    }
    private void HealthSystem_OnDamaged(object sender, OnDamageArgs e)
    {
        if (!active)
        {
            healthBarTransform.gameObject.SetActive(true);

            active = true;
        }
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (showNumbers)
        {
            healthText.text = healthSystem.GetHealth().ToString();
        }
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1f, 1f);
    }
    private void Update()
    {
        if (healthSystem == null)//initializs
        {
            healthSystem = GetComponentInParent<HealthSystem>();
            baseTransform = healthSystem.gameObject.transform;
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            if (showNumbers)
            {
                healthText.text = healthSystem.GetHealth().ToString();
            }

        }
        else
        {
            transform.position = baseTransform.position + new Vector3(0, 1, 0);
            transform.localRotation = Quaternion.Inverse(baseTransform.rotation);
        }

    }

}
