using UnityEngine;
public class SpriteSorter

{
    Transform objTransform;
    SpriteRenderer spriteRenderer;
    public SpriteSorter(Transform _transform, SpriteRenderer _spriteRenderer)
    {
        objTransform = _transform;
        spriteRenderer = _spriteRenderer;
    }
    public void SortSprite()
    {
        float y = objTransform.position.y *100;
        spriteRenderer.sortingOrder = (int) y;
    }
}