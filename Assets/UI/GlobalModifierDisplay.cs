using TMPro;
using UnityEngine;
using DG.Tweening;

public class GlobalModifierDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI affectsText;

    private BasicGlobalVariableModifier trackedMod;

    private void Start()
    {
        PlayEntryAnimation();
    }

    private void Update()
    {
        if (trackedMod != null && trackedMod.lifetime == ModifierLifetime.Timed)
        {
            float timeLeft = trackedMod.RemainingTime;
            string label = timeLeft > 0f ? $"{Mathf.CeilToInt(timeLeft)}s" : "Expired";
            timeLeftText.text = $"Time Left: {label}";
        }
    }

    public void SetDisplay(GlobalVariableModifier modifier)
    {
        if (modifier is not BasicGlobalVariableModifier basic)
        {
            timeLeftText.text = "Time Left: --";
            valueText.text = "Value: (Unknown)";
            affectsText.text = "Affects: (Invalid)";
            trackedMod = null;
            return;
        }

        trackedMod = basic;

        string timeLabel = basic.lifetime == ModifierLifetime.Timed
            ? $"{Mathf.CeilToInt(basic.RemainingTime)}s"
            : "∞";
        timeLeftText.text = $"Time Left: {timeLabel}";

        string valueLabel = basic.modType switch
        {
            GlobalModificationType.Add => $"+{basic.amount} {GetVarTypeDisplayName(basic.varType)}",
            GlobalModificationType.Multiply => $"x{basic.amount} {GetVarTypeDisplayName(basic.varType)}",
            _ => $"{basic.amount} {GetVarTypeDisplayName(basic.varType)}"
        };
        valueText.text = $"Value: {valueLabel}";

        affectsText.text = $"Affects: {GameStringParser.Parse(modifier)}";
    }

    private string GetVarTypeDisplayName(PlayerGlobalVarTypeEnum type)
    {
        return type switch
        {
            PlayerGlobalVarTypeEnum.ExtraLifetime => "Extra Lifetime",
            PlayerGlobalVarTypeEnum.ExtraHeal => "Healing",
            PlayerGlobalVarTypeEnum.DamageModifier => "Damage",
            PlayerGlobalVarTypeEnum.RangeModifier => "Range",
            _ => type.ToString()
        };
    }

    private void PlayEntryAnimation()
    {
        transform.localScale = Vector3.zero;
        CanvasGroup cg = GetOrAddCanvasGroup();
        cg.alpha = 0;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));
        s.Join(cg.DOFade(1f, 0.25f));
    }

    private CanvasGroup GetOrAddCanvasGroup()
    {
        var cg = GetComponent<CanvasGroup>();
        if (cg == null)
            cg = gameObject.AddComponent<CanvasGroup>();
        return cg;
    }
}
