using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(Vector3SpringComponent))]
	[CanEditMultipleObjects]
	public class Vector3SpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringVector3Drawer springVector3Drawer;


		protected override void CreateDrawers()
		{
			springVector3Drawer = new SpringVector3Drawer(serializedObject.FindProperty("springVector3"), false, false);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Value",
				width: LabelWidth,
				springDrawer: springVector3Drawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target",
				width: LabelWidth,
				springDrawer: springVector3Drawer
			);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springVector3Drawer);
		}
	}
}