#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringVector4Drawer : SpringDrawer
	{
		private SpringVector4EditorObject springVector4EditorObject;

		public SpringVector4Drawer(SerializedProperty property, bool isFoldout, bool isDebugger) :
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger)
		{}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springVector4EditorObject = (SpringVector4EditorObject)this.springEditorObject;
		}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringVector4EditorObject(parentProperty);
			return res;
		}

		protected override void DrawInitialValues(ref Rect currentRect)
		{
			springVector4EditorObject.InitialValues = DrawCustomVector4(currentRect, FIELD_NAME_INITIAL_VALUE, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.InitialValues, threeDecimalsOnly: true);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			DrawCustomVector4(currentRect, label, labelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springVector4EditorObject.Target = DrawCustomVector4(currentRect, label, labelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.Target);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springVector4EditorObject.MinValue = DrawCustomVector4(currentRect, FIELD_NAME_MIN_VALUES, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springVector4EditorObject.MaxValue = DrawCustomVector4(currentRect, FIELD_NAME_MAX_VALUES, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.MaxValue);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springVector4EditorObject.ClampTarget = DrawVector4Bool(currentRect, FIELD_NAME_CLAMP_TARGET, TOOLTIP_CLAMP_TARGET , COMPONENTS_LABELS_XYZW, LabelWidth, springVector4EditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springVector4EditorObject.ClampCurrentValue = DrawVector4Bool(currentRect, FIELD_NAME_CLAMP_CURRENT_VALUE, TOOLTIP_CLAMP_CURRENT_VALUE ,COMPONENTS_LABELS_XYZW, LabelWidth, springVector4EditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springVector4EditorObject.StopSpringOnCurrentValueClamp = DrawVector4Bool(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, TOOLTIP_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, COMPONENTS_LABELS_XYZW, LabelWidth, springVector4EditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springVector4EditorObject.Drag = DrawCustomVector4(currentRect, FIELD_NAME_DRAG, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.Drag);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springVector4EditorObject.Force = DrawCustomVector4(currentRect, FIELD_NAME_FORCE, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.Force);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springVector4EditorObject.OperationValue = DrawCustomVector4(currentRect, FIELD_NAME_OPERATION_VALUE, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.OperationValue);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springVector4EditorObject.Update = DrawVector4Bool(currentRect, FIELD_NAME_UPDATE_AXIS, COMPONENTS_LABELS_XYZW, LabelWidth, springVector4EditorObject.Update);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			DrawCustomVector4(currentRect, FIELD_NAME_VELOCITY, LabelWidth, COMPONENTS_LABELS_XYZW, springVector4EditorObject.Velocity, threeDecimalsOnly: true);
		}
	}
}

#endif