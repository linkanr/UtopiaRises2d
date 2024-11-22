using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class SpringColorEditorObject : SpringEditorObject
	{
		public SpringColorEditorObject(SerializedProperty spParentProperty) : base(spParentProperty, SpringColor.SPRING_SIZE)
		{}

		public Color InitialValue
		{
			get
			{
				Color res = new Color(
					springValuesEditorObjects[R].GetInitialValue(),
					springValuesEditorObjects[G].GetInitialValue(),
					springValuesEditorObjects[B].GetInitialValue(),
					springValuesEditorObjects[A].GetInitialValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetInitialValue(value.r);
				springValuesEditorObjects[G].SetInitialValue(value.g);
				springValuesEditorObjects[B].SetInitialValue(value.b);
				springValuesEditorObjects[A].SetInitialValue(value.a);
			}
		}

		public Vector4 MinValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[R].GetMinValue(),
					springValuesEditorObjects[G].GetMinValue(),
					springValuesEditorObjects[B].GetMinValue(),
					springValuesEditorObjects[A].GetMinValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetMinValue(value.x);
				springValuesEditorObjects[G].SetMinValue(value.y);
				springValuesEditorObjects[B].SetMinValue(value.z);
				springValuesEditorObjects[A].SetMinValue(value.w);
			}
		}

		public Vector4 MaxValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[R].GetMaxValue(),
					springValuesEditorObjects[G].GetMaxValue(),
					springValuesEditorObjects[B].GetMaxValue(),
					springValuesEditorObjects[A].GetMaxValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetMaxValue(value.x);
				springValuesEditorObjects[G].SetMaxValue(value.y);
				springValuesEditorObjects[B].SetMaxValue(value.z);
				springValuesEditorObjects[A].SetMaxValue(value.w);
			}
		}

		public Color Target
		{
			get
			{
				Color res = new Color(
					springValuesEditorObjects[R].spTarget.floatValue,
					springValuesEditorObjects[G].spTarget.floatValue,
					springValuesEditorObjects[B].spTarget.floatValue,
					springValuesEditorObjects[A].spTarget.floatValue);

				return res;
			}
			set
			{
				springValuesEditorObjects[R].spTarget.floatValue = value.r;
				springValuesEditorObjects[G].spTarget.floatValue = value.g;
				springValuesEditorObjects[B].spTarget.floatValue = value.b;
				springValuesEditorObjects[A].spTarget.floatValue = value.a;
			}
		}

		public Vector4Bool Update
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[R].GetUpdate(),
					springValuesEditorObjects[G].GetUpdate(),
					springValuesEditorObjects[B].GetUpdate(),
					springValuesEditorObjects[A].GetUpdate());

				return res;
			}

			set
			{
				springValuesEditorObjects[R].SetUpdate(value.x);
				springValuesEditorObjects[G].SetUpdate(value.y);
				springValuesEditorObjects[B].SetUpdate(value.z);
				springValuesEditorObjects[A].SetUpdate(value.w);
			}
		}

		public Vector4 Force
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[R].GetForce(),
					springValuesEditorObjects[G].GetForce(),
					springValuesEditorObjects[B].GetForce(),
					springValuesEditorObjects[A].GetForce());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetForce(value.x);
				springValuesEditorObjects[G].SetForce(value.y);
				springValuesEditorObjects[B].SetForce(value.z);
				springValuesEditorObjects[A].SetForce(value.w);
			}
		}

		public Vector4 Drag
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[R].GetDrag(),
					springValuesEditorObjects[G].GetDrag(),
					springValuesEditorObjects[B].GetDrag(),
					springValuesEditorObjects[A].GetDrag());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetDrag(value.x);
				springValuesEditorObjects[G].SetDrag(value.y);
				springValuesEditorObjects[B].SetDrag(value.z);
				springValuesEditorObjects[A].SetDrag(value.w);
			}
		}

		public Color CurrentValue
		{
			get
			{
				Color res = new Color(
					springValuesEditorObjects[R].GetCurrentValue(),
					springValuesEditorObjects[G].GetCurrentValue(),
					springValuesEditorObjects[B].GetCurrentValue(),
					springValuesEditorObjects[A].GetCurrentValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetCurrentValue(value.r);
				springValuesEditorObjects[G].SetCurrentValue(value.g);
				springValuesEditorObjects[B].SetCurrentValue(value.b);
				springValuesEditorObjects[A].SetCurrentValue(value.a);
			}
		}

		public Vector4 Velocity
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[R].GetVelocity(),
					springValuesEditorObjects[G].GetVelocity(),
					springValuesEditorObjects[B].GetVelocity(),
					springValuesEditorObjects[A].GetVelocity());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetVelocity(value.x);
				springValuesEditorObjects[G].SetVelocity(value.y);
				springValuesEditorObjects[B].SetVelocity(value.z);
				springValuesEditorObjects[A].SetVelocity(value.w);
			}
		}

		public Vector4 OperationValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[R].GetOperationValue(),
					springValuesEditorObjects[G].GetOperationValue(),
					springValuesEditorObjects[B].GetOperationValue(),
					springValuesEditorObjects[A].GetOperationValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetOperationValue(value.x);
				springValuesEditorObjects[G].SetOperationValue(value.y);
				springValuesEditorObjects[B].SetOperationValue(value.z);
				springValuesEditorObjects[A].SetOperationValue(value.w);
			}
		}

		public Vector4Bool ClampTarget
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[R].GetClampTarget(),
					springValuesEditorObjects[G].GetClampTarget(),
					springValuesEditorObjects[B].GetClampTarget(),
					springValuesEditorObjects[A].GetClampTarget());

				return res;
			}

			set
			{
				springValuesEditorObjects[R].SetClampTarget(value.x);
				springValuesEditorObjects[G].SetClampTarget(value.y);
				springValuesEditorObjects[B].SetClampTarget(value.z);
				springValuesEditorObjects[A].SetClampTarget(value.w);
			}
		}

		public Vector4Bool ClampCurrentValue
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[R].GetClampCurrentValue(),
					springValuesEditorObjects[G].GetClampCurrentValue(),
					springValuesEditorObjects[B].GetClampCurrentValue(),
					springValuesEditorObjects[A].GetClampCurrentValue());

				return res;
			}

			set
			{
				springValuesEditorObjects[R].SetClampCurrentValue(value.x);
				springValuesEditorObjects[G].SetClampCurrentValue(value.y);
				springValuesEditorObjects[B].SetClampCurrentValue(value.z);
				springValuesEditorObjects[A].SetClampCurrentValue(value.w);
			}
		}

		public Vector4Bool StopSpringOnCurrentValueClamp
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[R].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[G].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[B].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[A].GetStopSpringOnCurrentValueClamp());

				return res;
			}
			set
			{
				springValuesEditorObjects[R].SetStopSpringOnCurrentValueClamp(value.x);
				springValuesEditorObjects[G].SetStopSpringOnCurrentValueClamp(value.y);
				springValuesEditorObjects[B].SetStopSpringOnCurrentValueClamp(value.z);
				springValuesEditorObjects[A].SetStopSpringOnCurrentValueClamp(value.w);
			}
		}

		public override void AddVelocityNudge()
		{
			Velocity += OperationValue;
		}

		public override void SetCurrentValuesNudge()
		{
			CurrentValue = OperationValue;	
		}

		public override void SetTargetNudge()
		{
			Target = OperationValue;
		}
	}
}