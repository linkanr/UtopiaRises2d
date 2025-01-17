using UnityEngine.UI;
using UnityEngine;

public class PanelRebuildLayout : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        BattleSceneActions.OnCardsBeginDrawn += Rebuild;
        BattleSceneActions.OnCardsEndDrawn += StopRebuild;
    }

    private void OnDisable()
    {
        BattleSceneActions.OnCardsBeginDrawn -= Rebuild;
        BattleSceneActions.OnCardsEndDrawn -= StopRebuild;
    }

    private void Rebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        Canvas.ForceUpdateCanvases();
    }

    private void StopRebuild()
    {
        // Optionally handle any cleanup if needed
    }
}
