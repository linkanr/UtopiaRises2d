#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringVector3Drawer : SpringDrawer
	{
		private SpringVector3EditorObject springVector3EditorObject;

		public SpringVector3Drawer(SerializedProperty property, bool isFoldout, bool isDebugger) : 
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger)	{}

		public SpringVector3Drawer(bool isFoldout, bool isDebugger) :
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, drawUpdateArea: true, isDebugger: isDebugger)	{}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springVector3EditorObject = (SpringVector3EditorObject)this.springEditorObject;
		}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringVector3EditorObject(parentProperty);
			return res;
		}

		protected override void DrawInitialValues(ref Rect currentRect)
		{
			springVector3EditorObject.InitialValues = DrawCustomVector3(currentRect, FIELD_NAME_INITIAL_VALUE, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.InitialValues, threeDecimalsOnly: true);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springVector3EditorObject.MinValue = DrawCustomVector3(currentRect, FIELD_NAME_MIN_VALUES, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springVector3EditorObject.MaxValue = DrawCustomVector3(currentRect, FIELD_NAME_MAX_VALUES, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.MaxValue);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springVector3EditorObject.ClampTarget = DrawVector3Bool(currentRect, FIELD_NAME_CLAMP_TARGET, TOOLTIP_CLAMP_TARGET, COMPONENTS_LABELS_XYZ, LabelWidth, springVector3EditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springVector3EditorObject.ClampCurrentValue = DrawVector3Bool(currentRect, FIELD_NAME_CLAMP_CURRENT_VALUE,  TOOLTIP_CLAMP_CURRENT_VALUE, COMPONENTS_LABELS_XYZ, LabelWidth, springVector3EditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springVector3EditorObject.StopSpringOnCurrentValueClamp = DrawVector3Bool(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, TOOLTIP_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, COMPONENTS_LABELS_XYZ,
				LabelWidth, springVector3EditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			int indexChanged = -1;
			springVector3EditorObject.Update = DrawVector3Bool(currentRect, FIELD_NAME_UPDATE_AXIS, COMPONENTS_LABELS_XYZ, LabelWidth, springVector3EditorObject.Update, ref indexChanged);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springVector3EditorObject.Force = DrawCustomVector3(currentRect, FIELD_NAME_FORCE, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Force);
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springVector3EditorObject.Drag = DrawCustomVector3(currentRect, FIELD_NAME_DRAG, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Drag);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			DrawCustomVector3(currentRect, label, labelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springVector3EditorObject.Target = DrawCustomVector3(currentRect, label, labelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Target);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springVector3EditorObject.OperationValue = DrawCustomVector3(currentRect, FIELD_NAME_OPERATION_VALUE, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.OperationValue);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			DrawCustomVector3(currentRect, FIELD_NAME_VELOCITY, LabelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Velocity, threeDecimalsOnly: true);
		}
	}
}

#endif