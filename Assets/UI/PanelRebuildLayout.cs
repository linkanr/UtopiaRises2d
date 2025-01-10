using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
public class PanelRebuildLayout : MonoBehaviour
{
    private RectTransform rectTransform;
    private LayoutGroup group;
    private bool needsUpdate;
    private void Awake()
    {
        group = GetComponent<LayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        BattleSceneActions.OnCardsBeginDrawn += Rebuild;
        BattleSceneActions.OnCardsEndDrawn += StopRebuild;
    }

    private void StopRebuild()
    {
        needsUpdate = false;
    }

    private void Rebuild()
    {
        needsUpdate = true;
        StartCoroutine(UpdateLayoutGroup());
    }

    private void OnDisable()
    {
        BattleSceneActions.OnCardsBeginDrawn -= Rebuild;
        BattleSceneActions.OnCardsEndDrawn -= StopRebuild;
    }
    IEnumerator UpdateLayoutGroup()
    {
        while (needsUpdate)
        {
           LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
           yield return new WaitForEndOfFrame();
        }
        //Debug.Log("no longer updatering");
        yield return null;
        
    }
}
