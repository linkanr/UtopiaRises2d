using UnityEngine;

internal interface IHasClickEffect
{
    public Sprite GetSprite();

    /// <summary>
    /// sprite size as diameter
    /// </summary>
    /// <returns></returns>
    public float GetSpriteSize();
}