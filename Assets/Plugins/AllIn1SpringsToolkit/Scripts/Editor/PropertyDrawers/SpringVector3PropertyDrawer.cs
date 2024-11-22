using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomPropertyDrawer(typeof(SpringVector3))]
	public class SpringVector3PropertyDrawer : SpringPropertyDrawer
	{
		public override SpringDrawer GetSpringDrawer(SerializedProperty property, bool isFoldout, bool isDebugger)
		{
			SpringDrawer res = new SpringVector3Drawer(property, isFoldout, isDebugger);
			return res;
		}
	}
}