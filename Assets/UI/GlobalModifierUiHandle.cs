using System.Collections.Generic;
using UnityEngine;

public class GlobalModifierUIHandler : MonoBehaviour
{
    [SerializeField] private GlobalModifierDisplay displayPrefab;
    [SerializeField] private Transform displayParent;

    private readonly Dictionary<BasicGlobalVariableModifier, GlobalModifierDisplay> activeDisplays = new();

    private void OnEnable()
    {
        BattleSceneActions.OnGlobalModifersChanged += HandleModifierChange;
    }

    private void OnDisable()
    {
        BattleSceneActions.OnGlobalModifersChanged -= HandleModifierChange;
    }

    private void HandleModifierChange(GlobalVariableModifier mod)
    {
        if (mod is not BasicGlobalVariableModifier basic)
            return;
        if ((mod as BasicGlobalVariableModifier).isDogma == true)
        {
            return;
        }

        if (activeDisplays.ContainsKey(basic))
            return; // Already shown

        var instance = Instantiate(displayPrefab, displayParent ?? transform);
        instance.SetDisplay(basic);
        activeDisplays.Add(basic, instance);

        // Auto-remove UI when modifier expires
        basic.OnDispose += _ =>
        {
            if (activeDisplays.TryGetValue(basic, out var ui))
            {
                Destroy(ui.gameObject);
                activeDisplays.Remove(basic);
            }
        };
    }
}
