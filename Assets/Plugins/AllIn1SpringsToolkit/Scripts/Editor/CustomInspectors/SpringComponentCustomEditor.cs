using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringComponentCustomEditor : Editor
	{
		protected static Color AREA_COLOR_01 = new Color(0.3f, 0.3f, 0.3f, 1.0f);

		//Serialized Properties
		protected SerializedProperty spHasCustomInitialValues;
		protected SerializedProperty spHasCustomTarget;
		protected SerializedProperty spUseScaledTime;
		protected SerializedProperty spDoesAutoInitialize;

		protected SerializedProperty spGeneralPropertiesUnfolded;
		protected SerializedProperty spInitialValuesUnfolded;

		//Styles
		protected GUIStyle guiStyleLabelTitle;

		protected virtual void RefreshSerializedProperties()
		{
			spHasCustomInitialValues = serializedObject.FindProperty("hasCustomInitialValues");
			spHasCustomTarget = serializedObject.FindProperty("hasCustomTarget");
			spUseScaledTime = serializedObject.FindProperty("useScaledTime");
			spDoesAutoInitialize = serializedObject.FindProperty("doesAutoInitialize");

			spGeneralPropertiesUnfolded = serializedObject.FindProperty("generalPropertiesUnfolded");
			spInitialValuesUnfolded = serializedObject.FindProperty("initialValuesUnfolded");
		}

		protected void RefreshStyles()
		{
			guiStyleLabelTitle = new GUIStyle(EditorStyles.boldLabel); 
			guiStyleLabelTitle.normal.textColor = Color.white;
		}

		protected virtual void OnEnable()
		{
			RefreshSerializedProperties();
			CreateDrawers();
		}

		protected abstract void CreateDrawers();

		protected void DrawMainArea()
		{
			spGeneralPropertiesUnfolded.boolValue = DrawRectangleArea("General Properties", spGeneralPropertiesUnfolded.boolValue);
			
			if (spGeneralPropertiesUnfolded.boolValue)
			{
				DrawMainAreaUnfolded();

				DrawSerializedProperty(spUseScaledTime, LabelWidth);
				DrawSerializedProperty(spDoesAutoInitialize, LabelWidth);
			}
		}

		protected virtual void DrawMainAreaUnfolded()
		{

		}

		protected void DrawInitiaValues()
		{
			spInitialValuesUnfolded.boolValue = DrawRectangleArea("Initial Values", spInitialValuesUnfolded.boolValue);
			
			if (spInitialValuesUnfolded.boolValue)
			{
				DrawInitialValuesSection();
			}
		}

		protected virtual void DrawInitialValuesSection()
		{
			EditorGUILayout.BeginVertical();

			spHasCustomInitialValues.boolValue = DrawToggleLayout("Has Custom Initial Values", spHasCustomInitialValues.tooltip, LabelWidth, spHasCustomInitialValues.boolValue);

			if (spHasCustomInitialValues.boolValue)
			{
				DrawCustomInitialValuesSection();
				SpringsEditorUtility.Space();
			}

			if (!spHasCustomInitialValues.boolValue && spHasCustomTarget.boolValue)
			{
				SpringsEditorUtility.Space();
			}
			DrawSerializedProperty(spHasCustomTarget, LabelWidth);
		
			if (spHasCustomTarget.boolValue)
			{
				DrawCustomInitialTarget();
			}

			EditorGUILayout.EndVertical();
		}

		protected abstract void DrawSprings();

		protected abstract void DrawCustomInitialValuesSection();

		protected abstract void DrawCustomInitialTarget();

		protected bool DrawRectangleArea(string areaName, bool foldout)
		{
			return DrawRectangleArea(height: GetTitleAreaHeight(), areaName: areaName, spToggle: null, areaEnabled: true, foldout: foldout);
		}

		protected bool DrawRectangleArea(string areaName, SerializedProperty spToggle, bool foldout)
		{
			return DrawRectangleArea(height: GetTitleAreaHeight(), areaName: areaName, spToggle: spToggle, areaEnabled: true, foldout: foldout);
		}

		protected bool DrawRectangleArea(float height, string areaName, bool foldout)
		{
			return DrawRectangleArea(height: height, areaName: areaName, spToggle: null, areaEnabled: true, foldout: foldout);
		}

		protected bool DrawRectangleArea(float height, string areaName, SerializedProperty spToggle, bool areaEnabled, bool foldout)
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

		protected void DrawSimpleRectangleArea(float height, string areaName)
		{
			EditorGUILayout.BeginHorizontal();

			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: height);
			EditorGUI.DrawRect(rect, AREA_COLOR_01);

			Rect labelRect = new Rect(rect);
			labelRect.x = rect.width * 0.5f;
			EditorGUIUtility.labelWidth = 100f;
			labelRect.width = EditorGUIUtility.labelWidth;
			EditorGUI.LabelField(labelRect, areaName, guiStyleLabelTitle);

			EditorGUIUtility.labelWidth = 0f;

			EditorGUILayout.EndHorizontal();
		}

		protected float GetTitleAreaHeight()
		{
			float res = EditorGUIUtility.singleLineHeight * 1.5f;
			return res;
		}

		protected void DrawInitialValuesBySpring(string labelUseInitialValues, string labelInitialValues, float width, bool springEnabled, SpringDrawer springDrawer)
		{
			if (springEnabled)
			{
				Rect rectUseInitialValues = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
				springDrawer.DrawUseInitialValues(ref rectUseInitialValues, labelUseInitialValues, width);

				if (springDrawer.springEditorObject.UseInitialValues)
				{
					DrawInitialValuesBySpring(labelInitialValues, width, springDrawer);

					Space();
				}
			}
		}

		protected void DrawInitialValuesBySpring(string labelInitialValues, float width, SpringDrawer springDrawer)
		{
			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
			springDrawer.DrawInitialValues(ref rect, labelInitialValues, width);
		}

		protected void DrawCustomTargetBySpring(string labelUseCustomTarget, string labelCustomTarget, float width, bool springEnabled, SpringDrawer springDrawer)
		{
			if (springEnabled)
			{
				Rect rectUseInitialValues = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
				springDrawer.DrawUseCustomTarget(ref rectUseInitialValues, labelUseCustomTarget, width);

				if (springDrawer.springEditorObject.UseCustomTarget)
				{
					Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
					springDrawer.DrawTarget(ref rect, labelCustomTarget, width);

					Space();
				}
			}
		}

		protected void DrawCustomTargetBySpring(string labelCustomTarget, float width, SpringDrawer springDrawer)
		{
			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
			springDrawer.DrawTarget(ref rect, labelCustomTarget, width);
		}

		protected void DrawSpring(SpringDrawer springDrawer)
		{
			DrawSpring(springDrawer.springEditorObject.spParentroperty.displayName, springDrawer, null);
		}

		protected void DrawSpringWithEnableToggle(SpringDrawer springDrawer)
		{
			DrawSpring(springDrawer.springEditorObject.spParentroperty.displayName, springDrawer, springDrawer.springEditorObject.spSpringEnabled);
		}

		protected void DrawSpring(string springName, SpringDrawer springDrawer, SerializedProperty spToggle)
		{
			springDrawer.springEditorObject.Unfolded = DrawRectangleArea(
				height: GetTitleAreaHeight(),
				areaName: springName,
				areaEnabled: true,
				spToggle: spToggle,
				foldout: springDrawer.springEditorObject.Unfolded);


			if (springDrawer.springEditorObject.Unfolded)
			{
				Rect propertyRect = EditorGUILayout.GetControlRect(hasLabel: false, height: springDrawer.GetPropertyHeight());

				springDrawer.OnGUI(propertyRect);
			}
		}

		protected virtual void DrawInfoArea()
		{

		}

		public override void OnInspectorGUI()
		{
			RefreshStyles();

			serializedObject.Update();

			DrawMainArea();
			DrawInitiaValues();
			DrawSprings();

			DrawInfoArea();

			serializedObject.ApplyModifiedProperties();
		}
	}
}