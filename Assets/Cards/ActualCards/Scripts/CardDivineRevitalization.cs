using UnityEngine;
[CreateAssetMenu(menuName = "Cards/DivineRevitalization")]
public class CardDivineRevitalization : SoCardBase
{
    public int influenceAmount;
    public override bool ActualEffect(Vector3 position, out string failuerReason)
    {
        failuerReason = "";
        PlayerGlobalsManager.instance.AddInfluence(influenceAmount);
        return true;
    }
}