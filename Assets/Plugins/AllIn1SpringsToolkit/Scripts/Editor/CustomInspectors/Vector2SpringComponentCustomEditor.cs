using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(Vector2SpringComponent))]
	[CanEditMultipleObjects]
	public class Vector2SpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SerializedProperty spSpringVector2;

		private SpringVector2Drawer springVector2Drawer;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spSpringVector2 = serializedObject.FindProperty("springVector2");
		}

		protected override void CreateDrawers()
		{
			springVector2Drawer = new SpringVector2Drawer(spSpringVector2, false, false);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Value",
				width: LabelWidth,
				springDrawer: springVector2Drawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target",
				width: LabelWidth,
				springDrawer: springVector2Drawer);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springVector2Drawer);
		}
	}
}