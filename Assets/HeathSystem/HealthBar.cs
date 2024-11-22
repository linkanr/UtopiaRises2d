using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthBar : MonoBehaviour
{
    private BasicHealthSystem healthSystem;
    [SerializeField]    private Transform barTransform;
    [SerializeField] private Transform healthBarTransform;
    [SerializeField] private Transform baseTransform;// this is the main subjects transform
    private bool active;

    private void OnEnable()
    {
        healthBarTransform.gameObject.SetActive(false);

        active = false;
        healthSystem = GetComponentInParent<BasicHealthSystem>();
        baseTransform = healthSystem.GetTransformPosition();
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }
    private void OnDisable()
    {
 
        healthSystem.OnDamaged -= HealthSystem_OnDamaged;
    }
    private void HealthSystem_OnDamaged(object sender, EventArgs e)
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
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1f, 1f);
    }
    private void Update()
    {
        transform.position = baseTransform.position + new Vector3(0,1,0);
        transform.localRotation = Quaternion.Inverse(baseTransform.rotation);
    }
}
