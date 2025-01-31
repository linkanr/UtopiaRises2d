using UnityEngine;
public class SpriteSorter

{
    Transform objTransform;
    SpriteRenderer spriteRenderer;
    public SpriteSorter(Transform _transform, SpriteRenderer _spriteRenderer)
    {
        SpriteSorterTransfrom t = _transform.GetComponentInChildren<SpriteSorterTransfrom>();
        if (t != null)
        {
            objTransform = t.transform;
        }
        else
        {
            objTransform = _transform;
        }

        spriteRenderer = _spriteRenderer;
    }
    public void SortSprite()
    {
        float y = objTransform.position.y *-100;
        spriteRenderer.sortingOrder = (int) y;
    }
}