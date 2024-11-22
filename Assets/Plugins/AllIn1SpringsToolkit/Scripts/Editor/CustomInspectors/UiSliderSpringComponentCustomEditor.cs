using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
    [CustomEditor(typeof(UiSliderSpringComponent))]
    [CanEditMultipleObjects]
    public class UiSliderSpringComponentCustomEditor : SpringComponentCustomEditor
    {
        private SerializedProperty spAutoUpdatedSliderImage;
        private SpringFloatDrawer fillAmountSpringDrawer;
		
        protected override void RefreshSerializedProperties()
        {
            base.RefreshSerializedProperties();

            spAutoUpdatedSliderImage = serializedObject.FindProperty("autoUpdatedSliderImage");
        }

        protected override void CreateDrawers()
        {
            fillAmountSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("fillAmountSpring"), false, false);
        }

        protected override void DrawCustomInitialValuesSection()
        {
            DrawInitialValuesBySpring("Initial Value", LabelWidth, fillAmountSpringDrawer);
        }

        protected override void DrawCustomInitialTarget()
        {
            DrawCustomTargetBySpring("Target", LabelWidth, fillAmountSpringDrawer);
        }

        protected override void DrawMainAreaUnfolded()
        {
            DrawSerializedProperty(spAutoUpdatedSliderImage, LabelWidth);
        }

        protected override void DrawSprings()
        {
            DrawSpring(fillAmountSpringDrawer);
        }
		
        protected override void DrawInfoArea()
        {
            EditorGUILayout.Space(2);

            if (spAutoUpdatedSliderImage.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("AutoUpdatedSliderImage is not assigned!", MessageType.Error);
            }
        }
    }
}