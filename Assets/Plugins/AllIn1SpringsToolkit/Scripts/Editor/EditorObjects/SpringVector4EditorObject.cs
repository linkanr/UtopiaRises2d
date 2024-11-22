using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class SpringVector4EditorObject : SpringEditorObject
	{
		public Vector4 InitialValues
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetInitialValue(),
					springValuesEditorObjects[Y].GetInitialValue(),
					springValuesEditorObjects[Z].GetInitialValue(),
					springValuesEditorObjects[W].GetInitialValue());

				return res;
			}

			set
			{
				springValuesEditorObjects[X].SetInitialValue(value.x);
				springValuesEditorObjects[Y].SetInitialValue(value.y);
				springValuesEditorObjects[Z].SetInitialValue(value.z);
				springValuesEditorObjects[W].SetInitialValue(value.w);
			}
		}

		public Vector4Bool Update
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[X].GetUpdate(),
					springValuesEditorObjects[Y].GetUpdate(),
					springValuesEditorObjects[Z].GetUpdate(),
					springValuesEditorObjects[W].GetUpdate()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetUpdate(value.x);
				springValuesEditorObjects[Y].SetUpdate(value.y);
				springValuesEditorObjects[Z].SetUpdate(value.z);
				springValuesEditorObjects[W].SetUpdate(value.w);
			}
		}

		public Vector4Bool ClampTarget
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[X].GetClampTarget(),
					springValuesEditorObjects[Y].GetClampTarget(),
					springValuesEditorObjects[Z].GetClampTarget(),
					springValuesEditorObjects[W].GetClampTarget());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampTarget(value.x);
				springValuesEditorObjects[Y].SetClampTarget(value.y);
				springValuesEditorObjects[Z].SetClampTarget(value.z);
				springValuesEditorObjects[W].SetClampTarget(value.w);
			}
		}

		public Vector4Bool ClampCurrentValue
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[X].GetClampCurrentValue(),
					springValuesEditorObjects[Y].GetClampCurrentValue(),
					springValuesEditorObjects[Z].GetClampCurrentValue(),
					springValuesEditorObjects[W].GetClampCurrentValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampCurrentValue(value.x);
				springValuesEditorObjects[Y].SetClampCurrentValue(value.y);
				springValuesEditorObjects[Z].SetClampCurrentValue(value.z);
				springValuesEditorObjects[W].SetClampCurrentValue(value.w);
			}
		}

		public Vector4Bool StopSpringOnCurrentValueClamp
		{
			get
			{
				Vector4Bool res = new Vector4Bool(
					springValuesEditorObjects[X].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[Y].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[Z].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[W].GetStopSpringOnCurrentValueClamp());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetStopSpringOnCurrentValueClamp(value.x);
				springValuesEditorObjects[Y].SetStopSpringOnCurrentValueClamp(value.y);
				springValuesEditorObjects[Z].SetStopSpringOnCurrentValueClamp(value.z);
				springValuesEditorObjects[W].SetStopSpringOnCurrentValueClamp(value.w);
			}
		}

		public Vector4 Force
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetForce(),
					springValuesEditorObjects[Y].GetForce(),
					springValuesEditorObjects[Z].GetForce(),
					springValuesEditorObjects[W].GetForce()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetForce(value.x);
				springValuesEditorObjects[Y].SetForce(value.y);
				springValuesEditorObjects[Z].SetForce(value.z);
				springValuesEditorObjects[W].SetForce(value.w);
			}
		}

		public Vector4 Drag
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetDrag(),
					springValuesEditorObjects[Y].GetDrag(),
					springValuesEditorObjects[Z].GetDrag(),
					springValuesEditorObjects[W].GetDrag()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetDrag(value.x);
				springValuesEditorObjects[Y].SetDrag(value.y);
				springValuesEditorObjects[Z].SetDrag(value.z);
				springValuesEditorObjects[W].SetDrag(value.w);
			}
		}

		public Vector4 MinValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetMinValue(),
					springValuesEditorObjects[Y].GetMinValue(),
					springValuesEditorObjects[Z].GetMinValue(),
					springValuesEditorObjects[W].GetMinValue()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMinValue(value.x);
				springValuesEditorObjects[Y].SetMinValue(value.y);
				springValuesEditorObjects[Z].SetMinValue(value.z);
				springValuesEditorObjects[W].SetMinValue(value.w);
			}
		}

		public Vector4 MaxValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetMaxValue(),
					springValuesEditorObjects[Y].GetMaxValue(),
					springValuesEditorObjects[Z].GetMaxValue(),
					springValuesEditorObjects[W].GetMaxValue()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMaxValue(value.x);
				springValuesEditorObjects[Y].SetMaxValue(value.y);
				springValuesEditorObjects[Z].SetMaxValue(value.z);
				springValuesEditorObjects[W].SetMaxValue(value.w);
			}
		}

		public Vector4 Target
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetTarget(),
					springValuesEditorObjects[Y].GetTarget(),
					springValuesEditorObjects[Z].GetTarget(),
					springValuesEditorObjects[W].GetTarget()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetTarget(value.x);
				springValuesEditorObjects[Y].SetTarget(value.y);
				springValuesEditorObjects[Z].SetTarget(value.z);
				springValuesEditorObjects[W].SetTarget(value.w);
			}
		}

		public Vector4 CandidateValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetCandidateValue(),
					springValuesEditorObjects[Y].GetCandidateValue(),
					springValuesEditorObjects[Z].GetCandidateValue(),
					springValuesEditorObjects[W].GetCandidateValue()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetCandidateValue(value.x);
				springValuesEditorObjects[Y].SetCandidateValue(value.y);
				springValuesEditorObjects[Z].SetCandidateValue(value.z);
				springValuesEditorObjects[W].SetCandidateValue(value.w);
			}
		}

		public Vector4 CurrentValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetCurrentValue(),
					springValuesEditorObjects[Y].GetCurrentValue(),
					springValuesEditorObjects[Z].GetCurrentValue(),
					springValuesEditorObjects[W].GetCurrentValue()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetCurrentValue(value.x);
				springValuesEditorObjects[Y].SetCurrentValue(value.y);
				springValuesEditorObjects[Z].SetCurrentValue(value.z);
				springValuesEditorObjects[W].SetCurrentValue(value.w);
			}
		}

		public Vector4 Velocity
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetVelocity(),
					springValuesEditorObjects[Y].GetVelocity(),
					springValuesEditorObjects[Z].GetVelocity(),
					springValuesEditorObjects[W].GetVelocity()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetVelocity(value.x);
				springValuesEditorObjects[Y].SetVelocity(value.y);
				springValuesEditorObjects[Z].SetVelocity(value.z);
				springValuesEditorObjects[W].SetVelocity(value.w);
			}
		}

		public Vector4 OperationValue
		{
			get
			{
				Vector4 res = new Vector4(
					springValuesEditorObjects[X].GetOperationValue(),
					springValuesEditorObjects[Y].GetOperationValue(),
					springValuesEditorObjects[Z].GetOperationValue(),
					springValuesEditorObjects[W].GetOperationValue()
				);

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetOperationValue(value.x);
				springValuesEditorObjects[Y].SetOperationValue(value.y);
				springValuesEditorObjects[Z].SetOperationValue(value.z);
				springValuesEditorObjects[W].SetOperationValue(value.w);
			}
		}

		public SpringVector4EditorObject(SerializedProperty spParentroperty) : base(spParentroperty, SpringVector4.SPRING_SIZE)
		{
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
