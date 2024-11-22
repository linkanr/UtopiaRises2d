#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringDrawer
	{
		public static string[] COMPONENTS_LABELS_XYZW =
		{
			"X",
			"Y",
			"Z",
			"W"
		};

		public static string[] COMPONENTS_LABELS_RGBA =
		{
			"R",
			"G",
			"B",
			"A"
		};

		public static string[] COMPONENTS_LABELS_XYZ =
		{
			"X",
			"Y",
			"Z"
		};

		public static string[] COMPONENTS_LABELS_XY =
{
			"X",
			"Y"
		};


		protected SerializedProperty parentProperty;
		public SpringEditorObject springEditorObject;

		protected bool isFoldout;
		protected bool hasToggle;
		protected bool isDebugger;

		protected bool drawClampingArea;
		protected bool drawUpdateArea;

		protected abstract SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty);


		public SpringDrawer(SerializedProperty parentProperty, bool isFoldout, bool drawClampingArea, bool drawUpdateArea, bool isDebugger)
		{
			if (parentProperty != null)
			{
				SetParentProperty(parentProperty);
			}

			this.isFoldout = isFoldout;
			this.drawClampingArea = drawClampingArea;
			this.drawUpdateArea = drawUpdateArea;
			this.isDebugger = isDebugger;
		}

		public void SetParentProperty(SerializedProperty parentProperty)
		{
			this.parentProperty = parentProperty;
			RefreshSerializedProperties(parentProperty);
		}

		public virtual void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			springEditorObject = CreateSpringEditorObjectInstance(parentProperty);
		}

		public void OnGUI(Rect position)
		{
			OnGUI(position, parentProperty, GUIContent.none);
		}

		public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			LabelWidth = EditorGUIUtility.currentViewWidth * 0.4f;
			LabelWidth = Mathf.Clamp(LabelWidth, 120f, 220f);
			
			RefreshSerializedProperties(property);
			
			if (isFoldout)
			{
				Rect foldoutRect = new Rect(position.x, position.y, position.width, SpringsEditorUtility.FOLDOUT_HEIGHT);
				property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

				if (hasToggle)
				{
					Rect rectToggle = foldoutRect;
					rectToggle.x += EditorGUIUtility.labelWidth;
					EditorGUI.PropertyField(rectToggle, springEditorObject.spSpringEnabled, GUIContent.none);
				}

				if (property.isExpanded)
				{
					Rect currentRect = foldoutRect;
					currentRect.x += 20f;
					currentRect.width -= 20f;

					UpdateCurrentRect(ref currentRect);
					DrawGUI(ref currentRect);
				}
			}
			else
			{
				Rect foldoutRect = new Rect(position.x, position.y, position.width, SpringsEditorUtility.FOLDOUT_HEIGHT);
				property.isExpanded = true;
				Rect currentRect = foldoutRect;

				DrawGUI(ref currentRect);
			}

			EditorGUI.EndProperty();
		}

		private void DrawGUI(ref Rect currentRect)
		{
			if (isDebugger)
			{
				springEditorObject.spShowDebugFields.boolValue = DrawToggle(currentRect, springEditorObject.spShowDebugFields.displayName, LabelWidth, springEditorObject.spShowDebugFields.boolValue);
				UpdateCurrentRect(ref currentRect);

				if (springEditorObject.IsDebugShowEnabled())
				{
					DrawDebugGUI(ref currentRect);
					UpdateCurrentRect(ref currentRect);
				}
			}

			DrawUnifiedForceAndDrag(ref currentRect);
			UpdateCurrentRect(ref currentRect);

			if (drawClampingArea)
			{
				UpdateCurrentRect(ref currentRect);
				DrawClampingEnabled(currentRect);

				if (springEditorObject.ClampingEnabled)
				{
					EditorGUI.BeginChangeCheck();
					UpdateCurrentRect(ref currentRect);
					DrawClampTarget(ref currentRect);
					if (EditorGUI.EndChangeCheck())
					{
						if (!springEditorObject.IsClampTargetEnabled())
						{
							springEditorObject.ClampingEnabled = false;
						}
					}

					UpdateCurrentRect(ref currentRect);
					DrawClampCurrentValue(ref currentRect);

					if (springEditorObject.IsClampCurrentValueEnabled())
					{
						UpdateCurrentRect(ref currentRect);
						DrawStopSpringOnCurrentValueClamp(ref currentRect);
					}

					UpdateCurrentRect(ref currentRect);
					DrawClampingRange(ref currentRect);
				}

				UpdateCurrentRect(ref currentRect);
			}

			UpdateCurrentRect(ref currentRect);
			springEditorObject.spEventsEnabled.boolValue = DrawToggle(currentRect, FIELD_NAME_EVENTS_ENABLED, LabelWidth, springEditorObject.spEventsEnabled.boolValue);
			UpdateCurrentRect(ref currentRect);

			if (Application.isPlaying)
			{
				UpdateCurrentRect(ref currentRect);
				EditorGUI.DropShadowLabel(currentRect, "Playmode Operations");

				Rect gampleyValuesAreaRect = currentRect;
				gampleyValuesAreaRect.height = EditorGUIUtility.singleLineHeight * 7f;
				gampleyValuesAreaRect.height += 5f;
				EditorGUI.DrawRect(gampleyValuesAreaRect, new Color(0f, 0f, 0f, 0.25f));

				EditorGUI.BeginDisabledGroup(true);

				currentRect.x += 15f;
				currentRect.width -= 50f;

				UpdateCurrentRect(ref currentRect);
				DrawCurrentValue(ref currentRect);
				UpdateCurrentRect(ref currentRect);
				DrawTarget(ref currentRect);
				UpdateCurrentRect(ref currentRect);
				DrawVelocity(ref currentRect);
				EditorGUI.EndDisabledGroup();

				EditorGUI.BeginDisabledGroup(!Application.isPlaying);
				UpdateCurrentRect(ref currentRect);
				DrawNudgeOperationValues(ref currentRect);

				UpdateCurrentRect(ref currentRect, 2);
				DrawNudgeButtons(ref currentRect);
				EditorGUI.EndDisabledGroup();
			}
		}

		private void DrawClampingEnabled(Rect currentRect)
		{
			EditorGUI.BeginChangeCheck();
			springEditorObject.ClampingEnabled = DrawToggle(currentRect, "Clamping Enabled", TOOLTIP_CLAMPING, LabelWidth, springEditorObject.ClampingEnabled);
			bool changes = EditorGUI.EndChangeCheck();
			if(changes)
			{
				if (springEditorObject.ClampingEnabled)
				{
					if (!springEditorObject.IsClampTargetEnabled())
					{
						springEditorObject.SetAllClampTarget(true);
						springEditorObject.SetAllClampCurrentValue(true);
					}
				}
			}
		}

		private void DrawDebugGUI(ref Rect currentRect)
		{
			EditorGUI.PropertyField(currentRect, springEditorObject.spSpringValues, true);

			float squareYOffset = EditorGUIUtility.singleLineHeight;
			if (springEditorObject.spSpringValues.isExpanded)
			{
				int numFieldsExpanded = GetNumDebugFieldsExpanded();
				
				squareYOffset += EditorGUIUtility.singleLineHeight * numFieldsExpanded * 15.0f;
				squareYOffset += 10.0f * numFieldsExpanded;
				squareYOffset += (EditorGUIUtility.singleLineHeight + 5f) * springEditorObject.spSpringValues.arraySize;
				squareYOffset += EditorGUIUtility.singleLineHeight;
			}

			SpringsEditorUtility.UpdateCurrentRect(ref currentRect, squareYOffset);
		}

		protected abstract void DrawInitialValues(ref Rect currentRect);

		protected abstract void DrawUpdate(ref Rect currentRect);

		protected abstract void DrawForce(ref Rect currentRect);

		protected abstract void DrawDrag(ref Rect currentRect);

		protected void DrawCurrentValue(ref Rect currentRect)
		{
			DrawCurrentValue(ref currentRect, "Current Value", LabelWidth);
		}

		public void DrawCurrentValue(ref Rect currentRect, string label)
		{
			DrawCurrentValue(ref currentRect, label, LabelWidth);
		}

		public abstract void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth);

		protected abstract void DrawVelocity(ref Rect currentRect);

		protected void DrawTarget(ref Rect currentRect)
		{
			DrawTarget(ref currentRect, "Target", LabelWidth);
		}

		protected void DrawTarget(ref Rect currentRect, string label)
		{
			DrawTarget(ref currentRect, label, LabelWidth);
		}

		public abstract void DrawTarget(ref Rect currentRect, string label, float labelWidth);

		protected abstract void DrawClampingRange(ref Rect currentRect);

		protected abstract void DrawClampTarget(ref Rect currentRect);

		protected abstract void DrawClampCurrentValue(ref Rect currentRect);

		protected abstract void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect);


		protected virtual void DrawNudgeButtons(ref Rect currentRect)
		{
			float padding = 20f;
			float halfPadding = padding * 0.5f;

			float widthButton = currentRect.width / 3f;
			widthButton -= padding;

			Rect buttonRect = currentRect;
			buttonRect.width = widthButton;

			buttonRect.x += padding;
			if (GUI.Button(buttonRect, "Set Current Values"))
			{
				springEditorObject.SetCurrentValuesNudge();
			}

			buttonRect.x += widthButton;
			buttonRect.x += padding;
			if (GUI.Button(buttonRect, "Set Target"))
			{
				springEditorObject.SetTargetNudge();
			}

			buttonRect.x += widthButton;
			buttonRect.x += padding;
			if (GUI.Button(buttonRect, "Add Velocity"))
			{
				springEditorObject.AddVelocityNudge();
			}
		}

		protected abstract void DrawNudgeOperationValues(ref Rect currentRect);

		public virtual void DrawUnifiedForceAndDrag(ref Rect currentRect)
		{
			springEditorObject.unifiedForceAndDrag = DrawToggle(currentRect, "Unified force & drag", springEditorObject.spUnifiedForceAndDrag.tooltip, LabelWidth, springEditorObject.unifiedForceAndDrag);

			if (springEditorObject.unifiedForceAndDrag)
			{
				EditorGUI.BeginDisabledGroup(!springEditorObject.unifiedForceAndDrag);

				SpringsEditorUtility.UpdateCurrentRect(ref currentRect);

				DrawSerializedPropertyWithRect(currentRect, LabelWidth, springEditorObject.spUnifiedForce);
				SpringsEditorUtility.UpdateCurrentRect(ref currentRect);

				DrawSerializedPropertyWithRect(currentRect, LabelWidth, springEditorObject.spUnifiedDrag);

				EditorGUI.EndDisabledGroup();
			}
			else
			{
				EditorGUI.BeginDisabledGroup(springEditorObject.unifiedForceAndDrag);
				UpdateCurrentRect(ref currentRect);
				DrawForce(ref currentRect);

				UpdateCurrentRect(ref currentRect);
				DrawDrag(ref currentRect);
				EditorGUI.EndDisabledGroup();
			}

			if (drawUpdateArea)
			{
				UpdateCurrentRect(ref currentRect);
				DrawUpdate(ref currentRect);
			}

			DrawExtraFields(ref currentRect);
		}

		protected virtual void DrawExtraFields(ref Rect rect)
		{

		}

		public void DrawUseInitialValues(ref Rect currentRect, string label, float width)
		{
			DrawSerializedPropertyWithRect(currentRect, label, width, springEditorObject.spUseInitialValues);
		}

		public void DrawInitialValues(ref Rect currentRect, string label, float width)
		{
			DrawInitialValues(ref currentRect);
		}

		public void DrawUseCustomTarget(ref Rect currentRect, string label, float width)
		{
			DrawSerializedPropertyWithRect(currentRect, label, width, springEditorObject.spUseCustomTarget);
		}

		public float GetPropertyHeight()
		{
			float res = SpringsEditorUtility.FOLDOUT_HEIGHT;

			if (springEditorObject == null)
			{
				RefreshSerializedProperties(parentProperty);
			}

			if (parentProperty.isExpanded)
			{
				res = EditorGUIUtility.singleLineHeight * 6f;
				if (drawClampingArea)
				{
					res += EditorGUIUtility.singleLineHeight * 2f;
				}
				if (springEditorObject.ClampingEnabled)
				{
					res += EditorGUIUtility.singleLineHeight * 4f;
					if (springEditorObject.IsClampCurrentValueEnabled())
					{
						res += EditorGUIUtility.singleLineHeight;
					}
				}
				if (Application.isPlaying)
				{
					res += EditorGUIUtility.singleLineHeight * 9f;
				}
				if (isDebugger)
				{
					res += EditorGUIUtility.singleLineHeight * 2f;
				}

				if (isFoldout)
				{
					res += EditorGUIUtility.singleLineHeight * 2f;
				}

				res += springEditorObject.GetExtraFieldsHeight();

				if (springEditorObject.IsDebugShowEnabled())
				{
					float debugOffset = EditorGUIUtility.singleLineHeight;
					if (springEditorObject.spSpringValues.isExpanded)
					{
						int numFieldsExpanded = GetNumDebugFieldsExpanded();

						debugOffset += EditorGUIUtility.singleLineHeight * numFieldsExpanded * 15.0f;
						debugOffset += 10.0f * numFieldsExpanded;
						debugOffset += (EditorGUIUtility.singleLineHeight + 5f) * springEditorObject.spSpringValues.arraySize;
						debugOffset += EditorGUIUtility.singleLineHeight;
					}

					res += debugOffset;
				}
			}

			return res;
		}

		private int GetNumDebugFieldsExpanded()
		{
			int res = 0;

			for (int i = 0; i < springEditorObject.spSpringValues.arraySize; i++)
			{
				if (springEditorObject.spSpringValues.GetArrayElementAtIndex(i).isExpanded)
				{
					res++;
				}
			}

			return res;
		}
	}
}

#endif