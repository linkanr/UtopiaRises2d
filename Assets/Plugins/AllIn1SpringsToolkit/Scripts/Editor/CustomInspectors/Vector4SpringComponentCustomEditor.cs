using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(Vector4SpringComponent))]
	[CanEditMultipleObjects]
	public class Vector4SpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SerializedProperty spSpringVector4;
		private SpringVector4Drawer springVector4Drawer;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spSpringVector4 = serializedObject.FindProperty("springVector4");
		}

		protected override void CreateDrawers()
		{
			springVector4Drawer = new SpringVector4Drawer(spSpringVector4, false, false);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springVector4Drawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Value",
				width: LabelWidth,
				springDrawer: springVector4Drawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target",
				width: LabelWidth,
				springDrawer: springVector4Drawer);
		}
	}
}