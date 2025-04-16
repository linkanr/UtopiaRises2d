public class PoliticalSystem
{
    private PolicalCompassOrientation compassOrientation = new PolicalCompassOrientation();

    public void SetCompass(int galTan, int leftRight, int spritual = 0)
    {
        compassOrientation = new PolicalCompassOrientation(galTan, leftRight, spritual);
    }

    public PolicalCompassOrientation GetCompass() => compassOrientation;

    public PoliticalAlignment GetAlignment()
    {
        var demonation = new PoliticalCompasDemonation(
            compassOrientation.galTan,
            compassOrientation.leftRigt,
            compassOrientation.sprituality
        );
        return new PoliticalAlignment(demonation.galTan, demonation.leftRigt, demonation.sprituality);
    }

    public IdeolgicalAlignment GetIdeologicalAlignment() => GetAlignment().ideolgicalAlignment;

    public bool IsLeft() => GetAlignment().isLeft();
    public bool IsRight() => GetAlignment().isRigt();
    public bool IsGal() => GetAlignment().isGal();
    public bool IsTan() => GetAlignment().isTan();
    public bool IsSpiritual() => GetAlignment().isSpiritual();
    public bool IsMaterialistic() => GetAlignment().isMaterialistic();
}
