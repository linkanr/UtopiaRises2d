using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(RotationSpringComponent))]
	[CanEditMultipleObjects]
	public class RotationSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringRotationDrawer springRotationDrawer;


		protected override void CreateDrawers()
		{
			springRotationDrawer = new SpringRotationDrawer(serializedObject.FindProperty("springRotation"), false, false);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springRotationDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Value",
				width: LabelWidth,
				springDrawer: springRotationDrawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target",
				width: LabelWidth,
				springDrawer: springRotationDrawer);
		}
	}
}