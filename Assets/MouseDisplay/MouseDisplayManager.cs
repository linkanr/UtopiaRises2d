using System;
using UnityEngine;

public class MouseDisplayManager : MonoBehaviour
{
    public static MouseDisplayManager instance;
    public MouseDisplayStateMachine stateMachine;

    public BaseState<MouseDisplayStateMachine> prevState;
    public bool mouseOverCard;
    public bool highligtSceneObjects;
    public bool displayCellChange;
    public int displaySizeX;
    public int displaySizeY;

    [Header("Mouse Display Components")]
    [SerializeField] private SpriteRenderer mouseSpriteRenderer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("MouseDisplayManager instance already exists!");
    }

    private void OnEnable()
    {
        DisplayActions.OnDisplayCell += OnHandleCellchange;
        DisplayActions.OnMouseOverCard += OnMouseOverCard;
        DisplayActions.OnMouseNotOverCard += OnMouseNotOverCard;
        DisplayActions.OnHighligtSceneObject += OnHighlightSceneObject;

        DisplayActions.OnSetMouseSprite += HandleSetMouseSprite;
        DisplayActions.OnRemoveMouseSprite += HandleRemoveMouseSprite;
    }

    private void OnDisable()
    {
        DisplayActions.OnDisplayCell -= OnHandleCellchange;
        DisplayActions.OnMouseOverCard -= OnMouseOverCard;
        DisplayActions.OnMouseNotOverCard -= OnMouseNotOverCard;
        DisplayActions.OnHighligtSceneObject -= OnHighlightSceneObject;

        DisplayActions.OnSetMouseSprite -= HandleSetMouseSprite;
        DisplayActions.OnRemoveMouseSprite -= HandleRemoveMouseSprite;
    }

    private void OnHighlightSceneObject(bool obj)
    {
        highligtSceneObjects = obj;
    }

    private void OnHandleCellchange(OnDisplayCellArgs obj)
    {
        displayCellChange = obj.setDisplay;
        displaySizeX = obj.sizeX;
        displaySizeY = obj.sizeY;
    }

    private void OnMouseNotOverCard()
    {
        mouseOverCard = false;
    }

    private void OnMouseOverCard()
    {
        mouseOverCard = true;
    }

    /// <summary>
    /// Handles setting a new sprite for the mouse display.
    /// </summary>
    private void HandleSetMouseSprite(OnDisplaySpriteArgs args)
    {
        if (mouseSpriteRenderer == null)
        {
          //  Debug.LogError("Mouse sprite renderer is not assigned.");
            return;
        }

        mouseSpriteRenderer.sprite = args.sprite;
        mouseSpriteRenderer.gameObject.SetActive(true);
        mouseSpriteRenderer.transform.localScale = Vector3.one * args.size;
        //Debug.Log($"Mouse sprite set to: {args.sprite?.name ?? "None"}, Size: {args.size}");
    }

    /// <summary>
    /// Handles removing the sprite from the mouse display.
    /// </summary>
    private void HandleRemoveMouseSprite()
    {
        if (mouseSpriteRenderer == null)
        {
          //  Debug.LogError("Mouse sprite renderer is not assigned.");
            return;
        }

        mouseSpriteRenderer.sprite = null;
        mouseSpriteRenderer.gameObject.SetActive(false);
        //Debug.Log("Mouse sprite removed.");
    }
}
