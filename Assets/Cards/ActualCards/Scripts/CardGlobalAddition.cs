using UnityEngine;
[CreateAssetMenu(menuName = "Cards/GlobalAddition")]
public class CardGlobalAddition : SoCardBase
{
    public GlobalModificationType operators;
    public PlayerGlobalVarTypeEnum typeOfModifier;
    public ModifierLifetime lifetime;
    public float amount;
    public float duration;

    public override bool ActualEffect(Vector3 position, out string failuerReason)
    {
        PlayerGlobalsManager.instance.playerGlobalVariables.AddModifier(
        new BasicGlobalVariableModifier(
        typeOfModifier,
        operators, amount, lifetime,duration));
        failuerReason = "";
        return true;
    }
}
