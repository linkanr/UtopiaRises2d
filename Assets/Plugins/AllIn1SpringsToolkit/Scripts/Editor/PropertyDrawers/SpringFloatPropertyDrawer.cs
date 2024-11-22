using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[CustomPropertyDrawer(typeof(SpringFloat))]
	public class SpringFloatPropertyDrawer : SpringPropertyDrawer
	{
		public override SpringDrawer GetSpringDrawer(SerializedProperty property, bool isFoldout, bool isDebugger)
		{
			SpringDrawer res = new SpringFloatDrawer(property, isFoldout, isDebugger);
			return res;
		}
	}
}