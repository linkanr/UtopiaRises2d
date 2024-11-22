using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
    [CustomEditor(typeof(ShaderFloatSpringComponent))]
    [CanEditMultipleObjects]
    public class ShaderFloatSpringComponentCustomEditor : SpringComponentCustomEditor
    {
        private SpringFloatDrawer shaderValueSpringDrawer;

        private SerializedProperty spShaderPropertyName;
        private SerializedProperty spTargetIsRenderer;
        private SerializedProperty spTargetRenderer;
        private SerializedProperty spTargetGraphic;
        private SerializedProperty spGetAutoUpdatedMaterialFromTarget;
        private SerializedProperty spAutoUpdatedMaterial;

        protected override void RefreshSerializedProperties()
        {
            base.RefreshSerializedProperties();

            spShaderPropertyName = serializedObject.FindProperty("shaderPropertyName");
            spTargetIsRenderer = serializedObject.FindProperty("targetIsRenderer");
            spTargetRenderer = serializedObject.FindProperty("targetRenderer");
            spTargetGraphic = serializedObject.FindProperty("targetGraphic");
            spGetAutoUpdatedMaterialFromTarget = serializedObject.FindProperty("getAutoUpdatedMaterialFromTarget");
            spAutoUpdatedMaterial = serializedObject.FindProperty("autoUpdatedMaterial");
        }

        protected override void CreateDrawers()
        {
            shaderValueSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("shaderValueSpring"), false, false);
        }

        protected override void DrawMainAreaUnfolded()
        {
            DrawSerializedProperty(spShaderPropertyName, LabelWidth);
            DrawSerializedProperty(spTargetIsRenderer, LabelWidth);

            if (spTargetIsRenderer.boolValue)
            {
                DrawSerializedProperty(spTargetRenderer, LabelWidth);
            }
            else
            {
                DrawSerializedProperty(spTargetGraphic, LabelWidth);
            }

            DrawSerializedProperty(spGetAutoUpdatedMaterialFromTarget, LabelWidth);
            
            if (!spGetAutoUpdatedMaterialFromTarget.boolValue)
            {
                DrawSerializedProperty(spAutoUpdatedMaterial, LabelWidth);
            }
        }

        protected override void DrawCustomInitialValuesSection()
        {
            DrawInitialValuesBySpring("Initial Values", LabelWidth, shaderValueSpringDrawer);
        }

        protected override void DrawCustomInitialTarget()
        {
            DrawCustomTargetBySpring("Target", LabelWidth, shaderValueSpringDrawer);
        }

        protected override void DrawSprings()
        {
            DrawSpring(shaderValueSpringDrawer);
        }
        
        protected override void DrawInfoArea()
        {
            EditorGUILayout.Space(2);

            if (string.IsNullOrEmpty(spShaderPropertyName.stringValue))
            {
                EditorGUILayout.HelpBox("The ShaderPropertyName isn't valid!", MessageType.Error);
            }
            
            if (spTargetIsRenderer.boolValue && spTargetRenderer.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("The TargetRenderer isn't assigned!", MessageType.Error);
            }
            
            if (!spTargetIsRenderer.boolValue && spTargetGraphic.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("The TargetGraphic isn't assigned!", MessageType.Error);
            }
        }
    }
}