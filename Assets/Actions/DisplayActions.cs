using System;
using UnityEngine;

public static class DisplayActions
{
    public static Action OnMouseOverCard;
    public static Action OnMouseNotOverCard;
    public static Action<OnDisplayCellArgs> OnDisplayCell;
    public static Action<bool> OnHighligtSceneObject;

    // New actions for handling mouse sprites
    public static Action<OnDisplaySpriteArgs> OnSetMouseSprite;
    public static Action OnRemoveMouseSprite;

}

public class OnDisplayCellArgs
{
    public OnDisplayCellArgs(bool _setDisplay, Color _color, int _sizeX = 1, int _sizeY = 1)
    {
        setDisplay = _setDisplay;
        sizeX = _sizeX;
        sizeY = _sizeY;
        color = _color;
    }
    public Color color;
    public bool setDisplay;
    public int sizeX;
    public int sizeY;
}

// New class for mouse sprite display arguments
public class OnDisplaySpriteArgs
{
    public OnDisplaySpriteArgs(Sprite _sprite, float _size = 1f)
    {
        sprite = _sprite;
        size = _size;
    }

    public Sprite sprite;

    public float size;
}
