using System.Numerics;
using UnityEngine.Diagnostics;
[System.Serializable]
public class PoliticalAlignment
{
    public int leftRigt;
    public int galTan;
    public Vector2 polticalVector { get { return new Vector2(leftRigt, galTan); } }
    public Vector2 GetNormalizedPolticalVector()
    {
        float normalizedX = GeneralUtils.fit(leftRigt, -5f, 5f, 0f, 1f);
        float normalizedY = GeneralUtils.fit(galTan, -5f, 5f, 0f, 1f);
        return new Vector2(normalizedX, normalizedY);
    }
}