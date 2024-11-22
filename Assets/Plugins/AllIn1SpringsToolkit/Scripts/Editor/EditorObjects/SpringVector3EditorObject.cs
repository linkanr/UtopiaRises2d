using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class SpringVector3EditorObject : SpringEditorObject
	{
		public Vector3 InitialValues
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetInitialValue(),
					springValuesEditorObjects[Y].GetInitialValue(),
					springValuesEditorObjects[Z].GetInitialValue());

				return res;
			}

			set
			{
				springValuesEditorObjects[X].SetInitialValue(value.x);
				springValuesEditorObjects[Y].SetInitialValue(value.y);
				springValuesEditorObjects[Z].SetInitialValue(value.z);
			}
		}

		public Vector3Bool Update
		{
			set
			{
				SetUpdate(value);
			}
			get
			{
				Vector3Bool res = GetUpdate();
				return res;
			}
		}

		public Vector3Bool ClampTarget
		{
			get
			{
				Vector3Bool res = new Vector3Bool(
					springValuesEditorObjects[X].GetClampTarget(),
					springValuesEditorObjects[Y].GetClampTarget(),
					springValuesEditorObjects[Z].GetClampTarget());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampTarget(value.x);
				springValuesEditorObjects[Y].SetClampTarget(value.y);
				springValuesEditorObjects[Z].SetClampTarget(value.z);
			}
		}

		public Vector3Bool ClampCurrentValue
		{
			get
			{
				Vector3Bool res = new Vector3Bool(
					springValuesEditorObjects[X].GetClampCurrentValue(),
					springValuesEditorObjects[Y].GetClampCurrentValue(),
					springValuesEditorObjects[Z].GetClampCurrentValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampCurrentValue(value.x);
				springValuesEditorObjects[Y].SetClampCurrentValue(value.y);
				springValuesEditorObjects[Z].SetClampCurrentValue(value.z);
			}
		}

		public Vector3Bool StopSpringOnCurrentValueClamp
		{
			get
			{
				Vector3Bool res = new Vector3Bool(
					springValuesEditorObjects[X].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[Y].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[Z].GetStopSpringOnCurrentValueClamp());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetStopSpringOnCurrentValueClamp(value.x);
				springValuesEditorObjects[Y].SetStopSpringOnCurrentValueClamp(value.y);
				springValuesEditorObjects[Z].SetStopSpringOnCurrentValueClamp(value.z);
			}
		}

		public Vector3 Force
		{
			set
			{
				SetForce(value);
			}
			get
			{
				Vector3 res = GetForce();
				return res;
			}
		}

		public Vector3 Drag
		{
			set
			{
				SetDrag(value);
			}
			get
			{
				Vector3 res = GetDrag();
				return res;
			}
		}

		public Vector3 MinValue
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetMinValue(),
					springValuesEditorObjects[Y].GetMinValue(),
					springValuesEditorObjects[Z].GetMinValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMinValue(value.x);
				springValuesEditorObjects[Y].SetMinValue(value.y);
				springValuesEditorObjects[Z].SetMinValue(value.z);
			}
		}

		public Vector3 MaxValue
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetMaxValue(),
					springValuesEditorObjects[Y].GetMaxValue(),
					springValuesEditorObjects[Z].GetMaxValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMaxValue(value.x);
				springValuesEditorObjects[Y].SetMaxValue(value.y);
				springValuesEditorObjects[Z].SetMaxValue(value.z);
			}
		}

		public Vector3 Target
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetTarget(),
					springValuesEditorObjects[Y].GetTarget(),
					springValuesEditorObjects[Z].GetTarget());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetTarget(value.x);
				springValuesEditorObjects[Y].SetTarget(value.y);
				springValuesEditorObjects[Z].SetTarget(value.z);
			}
		}

		public Vector3 CandidateValue
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetCandidateValue(),
					springValuesEditorObjects[Y].GetCandidateValue(),
					springValuesEditorObjects[Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetCandidateValue(value.x);
				springValuesEditorObjects[Y].SetCandidateValue(value.y);
				springValuesEditorObjects[Z].SetCandidateValue(value.z);
			}
		}

		public Vector3 CurrentValue
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetCurrentValue(),
					springValuesEditorObjects[Y].GetCurrentValue(),
					springValuesEditorObjects[Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetCurrentValue(value.x);
				springValuesEditorObjects[Y].SetCurrentValue(value.y);
				springValuesEditorObjects[Z].SetCurrentValue(value.z);
			}
		}

		public Vector3 Velocity
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetVelocity(),
					springValuesEditorObjects[Y].GetVelocity(),
					springValuesEditorObjects[Z].GetVelocity());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetVelocity(value.x);
				springValuesEditorObjects[Y].SetVelocity(value.y);
				springValuesEditorObjects[Z].SetVelocity(value.z);
			}
		}

		public Vector3 OperationValue
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[X].GetOperationValue(),
					springValuesEditorObjects[Y].GetOperationValue(),
					springValuesEditorObjects[Z].GetOperationValue());
				
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetOperationValue(value.x);
				springValuesEditorObjects[Y].SetOperationValue(value.y);
				springValuesEditorObjects[Z].SetOperationValue(value.z);
			}
		}

		public SpringVector3EditorObject(SerializedProperty spParentProperty) : base(spParentProperty, SpringVector3.SPRING_SIZE)
		{}

		public void SetUpdate(Vector3Bool update)
		{
			springValuesEditorObjects[X].SetUpdate(update.x);
			springValuesEditorObjects[Y].SetUpdate(update.y);
			springValuesEditorObjects[Z].SetUpdate(update.z);
		}

		public Vector3Bool GetUpdate()
		{
			Vector3Bool res = new Vector3Bool(
				springValuesEditorObjects[X].spUpdate.boolValue, 
				springValuesEditorObjects[Y].spUpdate.boolValue, 
				springValuesEditorObjects[Z].spUpdate.boolValue);

			return res;
		}

		public void SetForce(Vector3 force)
		{
			springValuesEditorObjects[X].SetForce(force.x);
			springValuesEditorObjects[Y].SetForce(force.y);
			springValuesEditorObjects[Z].SetForce(force.z);
		}

		public Vector3 GetForce()
		{
			Vector3 res = new Vector3(springValuesEditorObjects[X].spForce.floatValue, springValuesEditorObjects[Y].spForce.floatValue, springValuesEditorObjects[Z].spForce.floatValue);
			return res;
		}

		public void SetDrag(Vector3 drag)
		{
			springValuesEditorObjects[X].SetDrag(drag.x);
			springValuesEditorObjects[Y].SetDrag(drag.y);
			springValuesEditorObjects[Z].SetDrag(drag.z);
		}

		public Vector3 GetDrag()
		{
			Vector3 res = new Vector3(springValuesEditorObjects[X].spDrag.floatValue, springValuesEditorObjects[Y].spDrag.floatValue, springValuesEditorObjects[Z].spDrag.floatValue);
			return res;
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