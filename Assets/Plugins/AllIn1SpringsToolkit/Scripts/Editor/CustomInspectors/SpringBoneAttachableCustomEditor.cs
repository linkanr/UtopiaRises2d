using UnityEditor;

namespace AllIn1SpringsToolkit.Bones
{
	[CustomEditor(typeof(SpringBoneAttachable))]
	public class SpringBoneAttachableCustomEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			serializedObject.ApplyModifiedProperties();
		}
	}
}