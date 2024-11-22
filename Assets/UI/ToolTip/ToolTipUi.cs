using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ToolTipUi : MonoBehaviour
{
    public static ToolTipUi Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI textmeshPro;
    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;
    private ToolTipTimer toolTipTimer;

    private void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        Hide();
    }
    private void OnEnable()
    {
        GlobalActions.Tooltip += ShowToolTip;
    }



    private void OnDisable()
    {
        GlobalActions.Tooltip -= ShowToolTip;
    }
    private void Update()
    {

        FollowMouse();

        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }
    private void FollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }
    private void Start()
    {
        SetText("Yo mutherfucker");
    }
    private void ShowToolTip(ToolTipArgs args)
    {
        ToolTipTimer newToolTipTimer = new ToolTipTimer(args.time);
        Show(args.Tooltip, newToolTipTimer);
    }
    private void SetText(string toolTipText)
    {
        textmeshPro.SetText(toolTipText);
        textmeshPro.ForceMeshUpdate();
        Vector2 textSize = textmeshPro.GetRenderedValues(true);
        Vector2 padding = new Vector2(10, 10);

        backgroundRectTransform.sizeDelta = textSize + padding;
    }
    public void Show(string tooltipText, ToolTipTimer toolTipTimer = null)
    {
        this.toolTipTimer = toolTipTimer;
        backgroundRectTransform.gameObject.SetActive(true);
        textmeshPro.gameObject.SetActive(true);
        SetText(tooltipText);
        FollowMouse();
    }
    public void Hide()
    {
        backgroundRectTransform.gameObject.SetActive(false);
        textmeshPro.gameObject.SetActive(false);
    }


    public class ToolTipTimer
    {
        public ToolTipTimer (float time)
        {
            timer = time;
        }
        public float timer;
    }
}
