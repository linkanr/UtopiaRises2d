using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringColorDrawer : SpringDrawer
	{
		private SpringColorEditorObject springColorEditorObject;

		public SpringColorDrawer(SerializedProperty property, bool isFoldout, bool isDebugger) : base(property, isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger) 
		{}

		public SpringColorDrawer(bool isFoldout) : base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: false)
		{}

		public SpringColorDrawer(bool isFoldout, bool isDebugger) : base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger)
		{}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springColorEditorObject = (SpringColorEditorObject)springEditorObject;
		}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringColorEditorObject(parentProperty);
			return res;
		}

		protected override void DrawInitialValues(ref Rect currentRect)
		{
			springColorEditorObject.InitialValue = DrawColorField(currentRect, FIELD_NAME_INITIAL_VALUE, LabelWidth, springColorEditorObject.InitialValue);
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springColorEditorObject.Drag = DrawCustomVector4(currentRect, FIELD_NAME_DRAG, LabelWidth, COMPONENTS_LABELS_RGBA, springColorEditorObject.Drag);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springColorEditorObject.Force = DrawCustomVector4(currentRect, FIELD_NAME_FORCE, LabelWidth, COMPONENTS_LABELS_RGBA, springColorEditorObject.Force);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springColorEditorObject.Target = DrawColorField(currentRect, label, LabelWidth, springColorEditorObject.Target);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springColorEditorObject.Update = DrawVector4Bool(currentRect, FIELD_NAME_UPDATE_CHANNEL, COMPONENTS_LABELS_RGBA, LabelWidth, springColorEditorObject.Update);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			DrawColorField(currentRect, label, LabelWidth, springColorEditorObject.CurrentValue);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			DrawCustomVector4(currentRect, FIELD_NAME_VELOCITY, LabelWidth, COMPONENTS_LABELS_XYZW, springColorEditorObject.Velocity, threeDecimalsOnly: true);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springColorEditorObject.OperationValue = DrawCustomVector4(currentRect, FIELD_NAME_OPERATION_VALUE, LabelWidth, COMPONENTS_LABELS_RGBA, springColorEditorObject.OperationValue);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springColorEditorObject.MinValue = DrawCustomVector4(currentRect, FIELD_NAME_MIN_VALUES, LabelWidth, COMPONENTS_LABELS_RGBA, springColorEditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springColorEditorObject.MaxValue = DrawCustomVector4(currentRect, FIELD_NAME_MAX_VALUES, LabelWidth, COMPONENTS_LABELS_RGBA, springColorEditorObject.MaxValue);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springColorEditorObject.ClampTarget = DrawVector4Bool(currentRect, FIELD_NAME_CLAMP_TARGET, TOOLTIP_CLAMP_TARGET, COMPONENTS_LABELS_RGBA, LabelWidth, springColorEditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springColorEditorObject.ClampCurrentValue = DrawVector4Bool(currentRect, FIELD_NAME_CLAMP_CURRENT_VALUE, TOOLTIP_CLAMP_CURRENT_VALUE, COMPONENTS_LABELS_RGBA, LabelWidth, springColorEditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springColorEditorObject.StopSpringOnCurrentValueClamp = DrawVector4Bool(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, TOOLTIP_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, COMPONENTS_LABELS_RGBA, LabelWidth, springColorEditorObject.StopSpringOnCurrentValueClamp);
		}
	}
}