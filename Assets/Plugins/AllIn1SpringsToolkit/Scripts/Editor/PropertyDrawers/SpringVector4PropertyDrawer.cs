using UnityEditor;

namespace AllIn1SpringsToolkit
{
	[CustomPropertyDrawer(typeof(SpringVector4))]
	public class SpringVector4PropertyDrawer : SpringPropertyDrawer
	{
		public override SpringDrawer GetSpringDrawer(SerializedProperty property, bool isFoldout, bool isDebugger)
		{
			SpringDrawer res = new SpringVector4Drawer(property, isFoldout, isDebugger);
			return res;
		}
	}
}