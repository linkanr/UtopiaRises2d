using AllIn1SpringsToolkit.Bones;
using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(SpringBone))]
	public class SpringBoneCustomEditor : UnityEditor.Editor
	{
		private SerializedProperty spBoneParent;
		private SerializedProperty spHierarchyLevel;
		private SerializedProperty spChildren;

		protected void RefreshSerializedProperties()
		{
			spBoneParent = serializedObject.FindProperty("boneParent");
			spHierarchyLevel = serializedObject.FindProperty("hierarchyLevel");
			spChildren = serializedObject.FindProperty("children");
		}

		private void DrawSerializedProperty(SerializedProperty serializedProperty)
		{
			Rect rect = EditorGUILayout.GetControlRect();
			EditorGUI.PropertyField(rect, serializedProperty);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			RefreshSerializedProperties();

			EditorGUI.BeginDisabledGroup(true);
			DrawSerializedProperty(spBoneParent);
			DrawSerializedProperty(spHierarchyLevel);
			EditorGUILayout.PropertyField(spChildren);
			EditorGUI.EndDisabledGroup();

			serializedObject.ApplyModifiedProperties();
		}
	}
}