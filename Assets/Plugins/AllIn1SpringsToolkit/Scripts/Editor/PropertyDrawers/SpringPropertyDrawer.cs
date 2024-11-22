using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringPropertyDrawer : PropertyDrawer
	{
		private SpringDrawer springDrawer;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			RefreshSpringDrawer(property, true, false);

			this.springDrawer.SetParentProperty(property);
			springDrawer.OnGUI(position, property, label);
		}

		private void RefreshSpringDrawer(SerializedProperty property, bool isFoldout, bool isDebugger)
		{
			if(springDrawer == null)
			{
				springDrawer = GetSpringDrawer(property, isFoldout, isDebugger);
			}
		}

		public abstract SpringDrawer GetSpringDrawer(SerializedProperty property, bool isFoldout, bool isDebugger);

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			RefreshSpringDrawer(property, true, false);

			float res = springDrawer.GetPropertyHeight();
			return res;
		}
	}
}