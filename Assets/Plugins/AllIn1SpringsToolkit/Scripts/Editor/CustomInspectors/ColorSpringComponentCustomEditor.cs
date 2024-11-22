using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(ColorSpringComponent))]
	[CanEditMultipleObjects]
	public class ColorSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringColorDrawer springColorDrawer;

		private SerializedProperty spAutoUpdate;
		private SerializedProperty spAutoUpdatedObjectIsRenderer;
		private SerializedProperty spAutoUpdatedRenderer;
		private SerializedProperty spAutoUpdatedUiGraphic;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spAutoUpdate = serializedObject.FindProperty("autoUpdate");
			spAutoUpdatedObjectIsRenderer = serializedObject.FindProperty("autoUpdatedObjectIsRenderer");
			spAutoUpdatedRenderer = serializedObject.FindProperty("autoUpdatedRenderer");
			spAutoUpdatedUiGraphic = serializedObject.FindProperty("autoUpdatedUiGraphic");
		}

		protected override void CreateDrawers()
		{
			springColorDrawer = new SpringColorDrawer(serializedObject.FindProperty("colorSpring"), false, false);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spAutoUpdate, LabelWidth);

			if (spAutoUpdate.boolValue)
			{
				EditorGUILayout.LabelField("Choose if the target is a Renderer(3D) or a Graphic(UI)");
				DrawSerializedProperty(spAutoUpdatedObjectIsRenderer, LabelWidth);

				if (spAutoUpdatedObjectIsRenderer.boolValue)
				{
					DrawSerializedProperty(spAutoUpdatedRenderer, LabelWidth);
				}
				else
				{
					DrawSerializedProperty(spAutoUpdatedUiGraphic, LabelWidth);
				}
			}
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Value",
				width: LabelWidth,
				springDrawer: springColorDrawer
				);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target",
				width: LabelWidth,
				springDrawer: springColorDrawer
				);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springColorDrawer);
		}
	}
}