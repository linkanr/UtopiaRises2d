using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AllIn1SpringsToolkit
{
    public static class SpringsToolkitSettings
    {
        private static SpringsToolkitSettingsAsset instance;

        private static SpringsToolkitSettingsAsset Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = Resources.Load<SpringsToolkitSettingsAsset>("SpringsToolkitSettings");
                    if(instance == null)
                    {
                        Debug.LogWarning("SpringsToolkitSettings not found. Using default settings.");
                        instance = ScriptableObject.CreateInstance<SpringsToolkitSettingsAsset>();
                    }
                }

                return instance;
            }
        }

        // Convenience Properties
        public static bool DoFixedUpdateRate
        {
            get => Instance.doFixedUpdateRate;
            set => Instance.doFixedUpdateRate = value;
        }

        public static float SpringFixedTimeStep
        {
            get => Instance.springFixedTimeStep;
            set => Instance.springFixedTimeStep = value;
        }

        public static void SaveChanges()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}