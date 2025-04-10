using UnityEngine;
public class SpriteSorter:MonoBehaviour

{

    SpriteRenderer spriteRenderer;
    private bool moving = false;
    public void Init(SpriteRenderer _spriteRenderer, bool _moving)
    {

        spriteRenderer = _spriteRenderer;
        this.moving = _moving;
    }
    public void SortSprite()
    {
        float y = transform.position.y * -100;
        spriteRenderer.sortingOrder = (int)y;
    }
    private void Update()
    {
        if (moving)
        {
            SortSprite();
        }
    }
}