using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public static class AllIn1SpringsEditorUtility
	{
		public static Color AREA_COLOR_01 = new Color(0.3f, 0.3f, 0.3f, 1.0f);
		public static float DEFAULT_TITLE_AREA_HEIGHT = EditorGUIUtility.singleLineHeight * 1.5f;

		public static bool DrawRectangleArea(string areaName, bool foldout, GUIStyle guiStyleLabelTitle, SerializedObject serializedObject)
		{
			return DrawRectangleArea(
				height: DEFAULT_TITLE_AREA_HEIGHT, 
				areaName: areaName, 
				spToggle: null, 
				areaEnabled: true, 
				foldout: foldout,
				guiStyleLabelTitle: guiStyleLabelTitle,
				serializedObject: serializedObject);
		}

		public static bool DrawRectangleArea(string areaName, SerializedProperty spToggle, bool foldout, GUIStyle guiStyleLabelTitle, SerializedObject serializedObject)
		{
			return DrawRectangleArea(
				height: DEFAULT_TITLE_AREA_HEIGHT, 
				areaName: areaName, 
				spToggle: spToggle, 
				areaEnabled: true, 
				foldout: foldout,
				guiStyleLabelTitle: guiStyleLabelTitle,
				serializedObject: serializedObject);
		}

		public static bool DrawRectangleArea(float height, string areaName, bool foldout, GUIStyle guiStyleLabelTitle, SerializedObject serializedObject)
		{
			return DrawRectangleArea(
				height: height, 
				areaName: areaName, 
				spToggle: null, 
				areaEnabled: true, 
				foldout: foldout,
				guiStyleLabelTitle: guiStyleLabelTitle,
				serializedObject: serializedObject);
		}

		public static bool DrawRectangleArea(float height, string areaName, SerializedProperty spToggle, bool areaEnabled, bool foldout, GUIStyle guiStyleLabelTitle, SerializedObject serializedObject)
		{
			bool res = foldout;

			bool toggleHasChanged = false;

			EditorGUILayout.BeginHorizontal();

			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: height);
			EditorGUI.DrawRect(rect, AREA_COLOR_01);

			Rect labelRect = new Rect(rect);
			labelRect.x = 25f;
			EditorGUIUtility.labelWidth = 100f;


			if (spToggle != null)
			{
				Rect toggleRect = new Rect(labelRect);
				toggleRect.width = 25f;

				EditorGUI.BeginChangeCheck();
				spToggle.boolValue = EditorGUI.Toggle(toggleRect, spToggle.boolValue);
				toggleHasChanged = EditorGUI.EndChangeCheck();
			}

			labelRect.width = areaName.Length * 10f;
			labelRect.x += 20f;

			EditorGUI.LabelField(labelRect, areaName, guiStyleLabelTitle);

			EditorGUIUtility.labelWidth = 0f;

			EditorGUILayout.EndHorizontal();

			if (Event.current.type == EventType.MouseDown)
			{
				if (rect.Contains(Event.current.mousePosition))
				{
					res = !res;
					Event.current.Use();
				}
			}

			if (toggleHasChanged)
			{
				res = true;
			}

			if (spToggle != null)
			{
				res = res && spToggle.boolValue;
			}

			return res;
		}
	}
}