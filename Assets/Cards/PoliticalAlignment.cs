
[System.Serializable]
public struct PoliticalAlignment
{
    public PoliticalAlignment(galTan _galTan, leftRigt _leftRigt)
    {
        galTan = _galTan;
        leftRigt = _leftRigt;
    }
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

public struct PolicalCompassOrientation
{
    public PolicalCompassOrientation(int _galTan, int _leftRigt)
    {
        leftRigt = _leftRigt;
        galTan = _galTan;

    }
    public int  galTan;
    public int  leftRigt;

}
public struct PoliticalCompasDemonation
{
    public PoliticalCompasDemonation(int _galTan, int _leftRigt)
    {
        switch (_galTan)
        {
            case var x when x <= -2:
                galTan = galTan.liberterian;
                break;
            case var x when x >= 2:
                galTan = galTan.authoriterian;
                break;
            case var x when x > -2 && x < 2:
                galTan = galTan.neutral; // Or whatever value fits the middle case
                break;
            default:
                galTan = galTan.neutral;
                break;
        }
        switch (_leftRigt)
        {
            case var x when x <= -2:
                leftRigt = leftRigt.left;
                break;
            case var x when x >= 2:
                leftRigt = leftRigt.rigt;
                break;
            case var x when x > -2 && x < 2:
                leftRigt = leftRigt.neutral; // Or whatever value fits the middle case
                break;
            default:
                leftRigt = leftRigt.neutral;
                break;
        }


    }
    public galTan galTan;
    public leftRigt leftRigt;
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
