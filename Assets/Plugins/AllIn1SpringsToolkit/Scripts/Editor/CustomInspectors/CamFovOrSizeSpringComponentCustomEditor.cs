using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(CamFovOrSizeSpringComponent))]
	[CanEditMultipleObjects]
	public class CamFovOrSizeSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringFloatDrawer fovSpringDrawer;

		private SerializedProperty spAutoUpdatedCamera;

		protected override void CreateDrawers()
		{
			fovSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("fovSpring"), false, false);
		}

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spAutoUpdatedCamera = serializedObject.FindProperty("autoUpdatedCamera");
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spAutoUpdatedCamera, LabelWidth);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target Fov",
				width: LabelWidth,
				springDrawer: fovSpringDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Fov",
				width: LabelWidth,
				springDrawer: fovSpringDrawer
			);
		}

		protected override void DrawSprings()
		{
			DrawSpring(fovSpringDrawer);
		}
		
		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);

			if (spAutoUpdatedCamera.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("AutoUpdatedCamera is not assigned!", MessageType.Error);
			}
		}
	}
}