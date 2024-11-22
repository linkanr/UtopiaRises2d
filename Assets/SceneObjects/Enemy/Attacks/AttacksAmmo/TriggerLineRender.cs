using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Diagnostics;

[RequireComponent(typeof(LineRenderer))]
public class TriggerLineRender : MonoBehaviour, IIsAttackInstanciator
{
    private LineRenderer lineRenderer;
    private float timerMax = .3f;
    private float timer = 0f;
    private bool triggerd;
    private Vector3 start;
    private Vector3 end;
    private Vector3 currentStart;
    private Vector3 currentEnd;




    public void Trigger(Enemy enemy, IDamageable idamageableByEnememy)
    {
        lineRenderer = GetComponent<LineRenderer>();
        triggerd = true;
        timer = 0f;
        
        start = enemy.transform.position;
        end = idamageableByEnememy.GetTransform().position;
        float dist = Vector3.Distance(start, end);
        float multi = GeneralUtils.fit(dist, .5f, 10f, .3f, 1f);
        timerMax *= multi;
        currentStart = start;
        currentEnd = start;




        DOTween.To(() => currentEnd, x => currentEnd = x,end, timerMax/2f);

        lineRenderer.SetPosition(0, enemy.transform.position);
        lineRenderer.SetPosition(1, idamageableByEnememy.GetTransform().position);

    }
    private void Update()
    {
        if (triggerd) 
        {
            timer += Time.deltaTime;
            if (timer > timerMax) 
            {
                DOTween.To(() => currentStart, x => currentStart = x, end, timerMax / 2f).OnComplete(() => SetInactive());
                triggerd = false;
            }

        }
        lineRenderer.SetPosition(0, currentStart);
        lineRenderer.SetPosition(1, currentEnd);
    }

    private void SetInactive()
    {
        Destroy(gameObject);
    }

}
