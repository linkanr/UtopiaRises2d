#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringFloatDrawer : SpringDrawer
	{
		private SpringFloatEditorObject springFloatEditorObject;

		public SpringFloatDrawer(SerializedProperty property, bool isFoldout, bool isDebugger) : 
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger) 
		{}

		public SpringFloatDrawer(bool isFoldout) : 
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: false)
		{}

		public SpringFloatDrawer(bool isFoldout, bool isDebugger) :
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger)
		{}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringFloatEditorObject(parentProperty);
			return res;
		}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springFloatEditorObject = (SpringFloatEditorObject)springEditorObject;
		}

		protected override void DrawInitialValues(ref Rect currentRect)
		{
			springFloatEditorObject.InitialValue = DrawCustomFloatLogic(currentRect, FIELD_NAME_INITIAL_VALUE, LabelWidth, springFloatEditorObject.InitialValue, threeDecimalsOnly: true);
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springFloatEditorObject.Drag = DrawCustomFloat(currentRect, FIELD_NAME_DRAG, springFloatEditorObject.Drag);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springFloatEditorObject.Force = DrawCustomFloat(currentRect, FIELD_NAME_FORCE, springFloatEditorObject.Force);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springFloatEditorObject.Update = DrawToggle(currentRect, FIELD_NAME_UPDATE, LabelWidth, springFloatEditorObject.Update);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			DrawCustomFloatLogic(currentRect, label, labelWidth, springFloatEditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			DrawCustomFloat(currentRect, FIELD_NAME_VELOCITY, springFloatEditorObject.Velocity, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springFloatEditorObject.Target = DrawCustomFloatLogic(currentRect, label, labelWidth, springFloatEditorObject.Target);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springFloatEditorObject.ClampTarget = DrawToggle(currentRect, FIELD_NAME_CLAMP_TARGET, TOOLTIP_CLAMP_TARGET, springFloatEditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springFloatEditorObject.ClampCurrentValue = DrawToggle(currentRect, FIELD_NAME_CLAMP_CURRENT_VALUE, TOOLTIP_CLAMP_CURRENT_VALUE, springFloatEditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springFloatEditorObject.StopSpringOnCurrentValueClamp = DrawToggle(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, TOOLTIP_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, springFloatEditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springFloatEditorObject.MinValue = DrawCustomFloat(currentRect, FIELD_NAME_MIN_VALUES, springFloatEditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springFloatEditorObject.MaxValue = DrawCustomFloat(currentRect, FIELD_NAME_MAX_VALUES, springFloatEditorObject.MaxValue);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springFloatEditorObject.OperationValue = DrawCustomFloat(currentRect, FIELD_NAME_OPERATION_VALUE, springFloatEditorObject.OperationValue);
		}
	}
}

#endif