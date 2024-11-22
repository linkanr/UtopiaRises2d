using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomPropertyDrawer(typeof(SpringColor))]
	public class SpringColorPropertyDrawer : SpringPropertyDrawer
	{
		public override SpringDrawer GetSpringDrawer(SerializedProperty property, bool isFoldout, bool isDebugger)
		{
			SpringDrawer res = new SpringColorDrawer(property, isFoldout, isDebugger);
			return res;
		}
	}
}