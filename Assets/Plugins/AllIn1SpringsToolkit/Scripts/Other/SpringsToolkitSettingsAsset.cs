using UnityEngine;

namespace AllIn1SpringsToolkit
{
    [CreateAssetMenu(fileName = "SpringsToolkitSettings", menuName = "AllIn1SpringsToolkit/Settings")]
    public class SpringsToolkitSettingsAsset : ScriptableObject
    {
        public bool doFixedUpdateRate = true;
        public float springFixedTimeStep = 0.02f;
    }
}