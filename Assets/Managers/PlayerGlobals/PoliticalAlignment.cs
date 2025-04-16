
[System.Serializable]
public struct PoliticalAlignment
{
    public PoliticalAlignment(galTan _galTan, leftRigt _leftRigt, sprituality _sprituality)
    {
        galTan = _galTan;
        leftRight = _leftRigt;
        sprituality = _sprituality;
    }

    public galTan galTan;
    public leftRigt leftRight;
    public sprituality sprituality;

    public bool isLeft() => leftRight == leftRigt.Left;
    public bool isRigt() => leftRight == leftRigt.Right;
    public bool isGal() => galTan == galTan.Gal;
    public bool isTan() => galTan == galTan.Tan;
    public bool isSpiritual() => sprituality == sprituality.Spiritual;
    public bool isMaterialistic() => sprituality == sprituality.Materialistic;

    public bool isGalTanNeutral() => galTan == galTan.Neutral;
    public bool isLeftRigtNeutral() => leftRight == leftRigt.Centrist;
    public bool isSpritualityNeutral() => sprituality == sprituality.Agnostic;

    public IdeolgicalAlignment ideolgicalAlignment
    {
        get
        {
            if (isGalTanNeutral() && isLeftRigtNeutral())
                return IdeolgicalAlignment.MiddleCentrist;
            if (isGalTanNeutral() && isLeft())
                return IdeolgicalAlignment.MiddleLeft;
            if (isGalTanNeutral() && isRigt())
                return IdeolgicalAlignment.MiddleRight;
            if (isTan() && isLeftRigtNeutral())
                return IdeolgicalAlignment.AutoritarianCentrist;
            if (isGal() && isLeftRigtNeutral())
                return IdeolgicalAlignment.LibertarianCentrist;
            if (isTan() && isLeft())
                return IdeolgicalAlignment.AutoritarianLeft;
            if (isTan() && isRigt())
                return IdeolgicalAlignment.AutoritarianRight;
            if (isGal() && isLeft())
                return IdeolgicalAlignment.LibertarianLeft;
            if (isGal() && isRigt())
                return IdeolgicalAlignment.LibertarianRight;

            throw new System.Exception("ideolgicalAlignment not found");
        }
    }
}


public struct PolicalCompassOrientation
{
    public int galTan;
    public int leftRigt;
    public int sprituality;

    public PolicalCompassOrientation(int _galTan, int _leftRigt, int _sprituality)
    {
        galTan = _galTan;
        leftRigt = _leftRigt;
        sprituality = _sprituality;
    }
}
public struct PoliticalCompasDemonation
{
    public galTan galTan;
    public leftRigt leftRigt;
    public sprituality sprituality;

    public PoliticalCompasDemonation(int _galTan, int _leftRigt, int _sprituality)
    {
        galTan = _galTan switch
        {
            <= -2 => galTan.Gal,
            >= 2 => galTan.Tan,
            _ => galTan.Neutral
        };

        leftRigt = _leftRigt switch
        {
            <= -2 => leftRigt.Left,
            >= 2 => leftRigt.Right,
            _ => leftRigt.Centrist
        };

        sprituality = _sprituality switch
        {
            <= -2 => sprituality.Spiritual,
            >= 2 => sprituality.Materialistic,
            _ => sprituality.Agnostic
        };
    }
}

public enum galTan
{
    Neutral,
    Gal,
    Tan
}
public enum leftRigt
{
    Centrist,
    Left,
    Right
}
public enum sprituality
{
    Agnostic,
    Spiritual,
    Materialistic
}
public enum IdeolgicalAlignment
{
    MiddleCentrist,
    LibertarianCentrist,
    AutoritarianCentrist,
    AutoritarianLeft,
    MiddleLeft,
    LibertarianLeft,
    AutoritarianRight,
    MiddleRight,
    LibertarianRight
}
public enum PoliticalAxisTag
{
    Left,
    Right,
    Gal,
    Tan,
    Middle,
    Centrist
}