#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringVector2Drawer : SpringDrawer
	{
		private SpringVector2EditorObject springVector2EditorObject;

		public SpringVector2Drawer(SerializedProperty property, bool isFoldout, bool isDebugger) : 
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger) 
		{}

		public SpringVector2Drawer(bool isFoldout) : 
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: false)
		{}

		public SpringVector2Drawer(bool isFoldout, bool isDebugger) :
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger)
		{}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringVector2EditorObject(parentProperty);
			return res;
		}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springVector2EditorObject = (SpringVector2EditorObject)this.springEditorObject;
		}

		protected override void DrawInitialValues(ref Rect currentRect)
		{
			springVector2EditorObject.InitialValues = DrawCustomVector2(
				position: currentRect,
				label: FIELD_NAME_INITIAL_VALUE,
				labelWidth: LabelWidth,
				componentsLabels: COMPONENTS_LABELS_XY,
				vector2: springVector2EditorObject.InitialValues, threeDecimalsOnly: true);
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springVector2EditorObject.Drag = DrawCustomVector2(currentRect, FIELD_NAME_DRAG, LabelWidth, COMPONENTS_LABELS_XY, springVector2EditorObject.Drag);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springVector2EditorObject.Force = DrawCustomVector2(currentRect, FIELD_NAME_FORCE, LabelWidth, COMPONENTS_LABELS_XY, springVector2EditorObject.Force);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springVector2EditorObject.Update = DrawVector2Bool(currentRect, FIELD_NAME_UPDATE_AXIS, COMPONENTS_LABELS_XY, LabelWidth, springVector2EditorObject.Update);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			DrawCustomVector2(
				position: currentRect, 
				label: label, 
				labelWidth: labelWidth,
				componentsLabels: COMPONENTS_LABELS_XY, 
				vector2: springVector2EditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			DrawCustomVector2(currentRect, FIELD_NAME_VELOCITY, LabelWidth, COMPONENTS_LABELS_XY, springVector2EditorObject.Velocity, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springVector2EditorObject.Target = DrawCustomVector2(
				position: currentRect, 
				label: label, 
				labelWidth: labelWidth, 
				componentsLabels: COMPONENTS_LABELS_XY, 
				vector2: springVector2EditorObject.Target);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springVector2EditorObject.ClampTarget = DrawVector2Bool(currentRect, FIELD_NAME_CLAMP_TARGET, TOOLTIP_CLAMP_TARGET, COMPONENTS_LABELS_XY, LabelWidth, springVector2EditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springVector2EditorObject.ClampCurrentValue = DrawVector2Bool(currentRect, FIELD_NAME_CLAMP_CURRENT_VALUE, TOOLTIP_CLAMP_CURRENT_VALUE, COMPONENTS_LABELS_XY, LabelWidth, springVector2EditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springVector2EditorObject.StopSpringOnCurrentValueClamp = DrawVector2Bool(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, TOOLTIP_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, COMPONENTS_LABELS_XY, LabelWidth, springVector2EditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springVector2EditorObject.MinValue = DrawCustomVector2(currentRect, FIELD_NAME_MIN_VALUES, LabelWidth, COMPONENTS_LABELS_XY, springVector2EditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springVector2EditorObject.MaxValue = DrawCustomVector2(currentRect, FIELD_NAME_MAX_VALUES, LabelWidth, COMPONENTS_LABELS_XY, springVector2EditorObject.MaxValue);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springVector2EditorObject.OperationValue = DrawCustomVector2(currentRect, FIELD_NAME_OPERATION_VALUE, LabelWidth, COMPONENTS_LABELS_XY, springVector2EditorObject.OperationValue);
		}
	}
}

#endif