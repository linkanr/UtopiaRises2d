using System.Numerics;
using UnityEditor.PackageManager;
using UnityEngine.Diagnostics;
[System.Serializable]
public struct PoliticalAlignment
{
    public galTan galTan;
    public leftRigt leftRigt;

    public bool isLeft()
    {
        return leftRigt == leftRigt.left;
    }
    public bool isRigt()
    {
        return leftRigt == leftRigt.rigt;
    }
    public bool isGal()
    {
        return galTan == galTan.liberterian;
    }
    public bool isTan()
    {
        return galTan == galTan.authoriterian;
    }
    public bool isGalTanNeutral()
    {
        return galTan == galTan.neutral;
    }
    public bool isLeftRigtNeutral()
    {
        return leftRigt == leftRigt.neutral;
    }
    public IdeolgicalAlignment ideolgicalAlignment
    {
        get
        {
            if (isGalTanNeutral() && isLeftRigtNeutral())
            {
                return IdeolgicalAlignment.Centrist;
            }
            if (isGalTanNeutral() && isLeft())
            {
                return IdeolgicalAlignment.Left;
            }
            if (isGalTanNeutral() && isRigt())
            {
                return IdeolgicalAlignment.Rigth;
            }
            if (isTan() && isLeftRigtNeutral())
            {
                return IdeolgicalAlignment.AutoritarianCentrist;
            }
            if (isGal() && isLeftRigtNeutral())
            {
                return IdeolgicalAlignment.LibertarianCentrist;
            }
            if (isTan() && isLeft())
            {
                return IdeolgicalAlignment.AutoritarianLeft;
            }
            if (isTan() && isRigt())
            {
                return IdeolgicalAlignment.AutoritarianRigt;
            }
            if (isGal() && isLeft())
            {
                return IdeolgicalAlignment.LibertarianLeft;
            }
            if (isGal() && isRigt())
            {
                return IdeolgicalAlignment.LibertarianRigt;
            }
            throw new System.Exception("ideolgicalAlignment not found");
        }
    }
}

public enum galTan
{
    neutral,
    liberterian,
    authoriterian
}
public enum leftRigt
{
    neutral,
    left,
    rigt
}
public enum IdeolgicalAlignment
{
    Centrist,
    LibertarianCentrist,
    AutoritarianCentrist,
    AutoritarianLeft,
    Left,
    LibertarianLeft,
    AutoritarianRigt,
    Rigth,
    LibertarianRigt
}
