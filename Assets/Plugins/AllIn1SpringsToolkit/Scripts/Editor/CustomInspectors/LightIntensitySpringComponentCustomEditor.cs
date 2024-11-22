using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(LightIntensitySpringComponent))]
	[CanEditMultipleObjects]
	public class LightIntensitySpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SerializedProperty spAutoUpdatedLight;

		private SpringFloatDrawer lightIntensitySpringDrawer;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spAutoUpdatedLight = serializedObject.FindProperty("autoUpdatedLight");
		}

		protected override void CreateDrawers()
		{
			lightIntensitySpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("lightIntensitySpring"), false, false);
		}

		protected override void DrawSprings()
		{
			DrawSpring(lightIntensitySpringDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring("Initial Value", LabelWidth, lightIntensitySpringDrawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring("Target", LabelWidth, lightIntensitySpringDrawer);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spAutoUpdatedLight, LabelWidth);
		}

		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);

			if (spAutoUpdatedLight.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("AutoUpdatedLight is not assigned!", MessageType.Error);
			}
		}
	}
}
