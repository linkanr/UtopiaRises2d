using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DogmaUiVisualizerObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SpriteRenderer spriteRenderer;
    private string displayName;
    private Dogma dogma;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Init(Dogma _dogma)
    {
        dogma = _dogma;
        VisualizeDisplay();
    }

    private void VisualizeDisplay()
    {
        spriteRenderer.sprite = dogma.sprite;
        displayName = dogma.displayName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GlobalActions.ShowTooltip?.Invoke(new ToolTipArgs { Tooltip=displayName,time = 2f});
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
