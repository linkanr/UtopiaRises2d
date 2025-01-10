using System;

public static class DisplayActions
{
    public static Action OnMouseOverCard;
    public static Action OnMouseNotOverCard;
    public static Action<OnDisplayCellArgs> OnDisplayCell;
    public static Action<bool> OnHighligtSceneObject;
}
public class OnDisplayCellArgs
{
    public OnDisplayCellArgs(bool _setDisplay, int _sizeX = 1, int _sizeY = 1)
    {
        setDisplay = _setDisplay;
        sizeX = _sizeX;
        sizeY = _sizeY;
    }

    public bool setDisplay;
    public int sizeX;
    public int sizeY;

}