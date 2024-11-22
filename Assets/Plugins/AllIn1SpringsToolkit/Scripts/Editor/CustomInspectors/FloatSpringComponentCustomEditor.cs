using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(FloatSpringComponent))]
	[CanEditMultipleObjects]
	public class FloatSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringFloatDrawer springFloatDrawer;

		protected override void CreateDrawers()
		{
			springFloatDrawer = new SpringFloatDrawer(serializedObject.FindProperty("springFloat"), false, false);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springFloatDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring("Initial Value", LabelWidth, springFloatDrawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring("Target", LabelWidth, springFloatDrawer);
		}
	}
}