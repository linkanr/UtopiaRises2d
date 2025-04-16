using UnityEngine;

[CreateAssetMenu(menuName = "Artifacts/Artifact Modifier")]
public class SoDogmaBase : ScriptableObject
{
    public BasicGlobalVariableModifier modifierData;
    public Sprite sprite;
    public string displayName;
    public static Color displayColor = new Color(1f, 0.5f, 0f); // Default color (orange)
}
