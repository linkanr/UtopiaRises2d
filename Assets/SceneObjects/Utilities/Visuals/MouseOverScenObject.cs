using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverScenObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SpriteRenderer spriteRenderer;
    private Color baseColor;
    private Color hoverColor;
    public void Init(SpriteRenderer _spriteRenderer)
    {
        spriteRenderer = _spriteRenderer;
        baseColor = _spriteRenderer.color;
        hoverColor = new Color(2f,2f,2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MouseDisplayManager.instance.highligtSceneObjects)
        spriteRenderer.color = hoverColor; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.color = baseColor;
    }
}