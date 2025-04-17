using UnityEngine;

public class Dogma : MonoBehaviour
{
    private BasicGlobalVariableModifier appliedModifier;
    private SoDogmaBase dogmaBase;
    public string displayName { get { return dogmaBase.displayName; } }
    public Sprite sprite{ get {return dogmaBase.sprite; } } 
    public void Init(SoDogmaBase soArtifactBase)
    {
        appliedModifier = soArtifactBase.modifierData.Clone();
       PlayerGlobalsManager.instance.playerGlobalVariables.AddModifier(appliedModifier);

        dogmaBase = Instantiate(soArtifactBase);


    }


    private void OnDestroy()
    {
        if (appliedModifier?.lifetime == ModifierLifetime.Permanent)
        {
            appliedModifier.Dispose();
        }
    }
}
