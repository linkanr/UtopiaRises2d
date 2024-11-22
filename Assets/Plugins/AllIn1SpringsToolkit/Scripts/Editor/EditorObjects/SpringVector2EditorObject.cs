using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class SpringVector2EditorObject : SpringEditorObject
	{
		public SpringVector2EditorObject(SerializedProperty spParentProperty) : base(spParentProperty, SpringVector2.SPRING_SIZE)
		{}

		public Vector2 InitialValues
		{
			get
			{
				Vector2 res = new Vector3(
					springValuesEditorObjects[X].GetInitialValue(),
					springValuesEditorObjects[Y].GetInitialValue());

				return res;
			}

			set
			{
				springValuesEditorObjects[X].SetInitialValue(value.x);
				springValuesEditorObjects[Y].SetInitialValue(value.y);
			}
		}

		public Vector2Bool Update
		{
			get
			{
				Vector2Bool res = new Vector2Bool(
					springValuesEditorObjects[X].GetUpdate(),
					springValuesEditorObjects[Y].GetUpdate());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetUpdate(value.x);
				springValuesEditorObjects[Y].SetUpdate(value.y);
			}
		}

		public Vector2Bool ClampTarget
		{
			get
			{
				Vector2Bool res = new Vector2Bool(
					springValuesEditorObjects[X].GetClampTarget(),
					springValuesEditorObjects[Y].GetClampTarget());
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampTarget(value.x);
				springValuesEditorObjects[Y].SetClampTarget(value.y);
			}
		}

		public Vector2Bool ClampCurrentValue
		{
			get
			{
				Vector2Bool res = new Vector2Bool(
					springValuesEditorObjects[X].GetClampCurrentValue(),
					springValuesEditorObjects[Y].GetClampCurrentValue());
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampCurrentValue(value.x);
				springValuesEditorObjects[Y].SetClampCurrentValue(value.y);
			}
		}

		public Vector2Bool StopSpringOnCurrentValueClamp
		{
			get
			{
				Vector2Bool res = new Vector2Bool(
					springValuesEditorObjects[X].GetStopSpringOnCurrentValueClamp(),
					springValuesEditorObjects[Y].GetStopSpringOnCurrentValueClamp());
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetStopSpringOnCurrentValueClamp(value.x);
				springValuesEditorObjects[Y].SetStopSpringOnCurrentValueClamp(value.y);
			}
		}

		public Vector2 Force
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetForce(),
					springValuesEditorObjects[Y].GetForce());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetForce(value.x);
				springValuesEditorObjects[Y].SetForce(value.y);
			}
		}

		public Vector2 Drag
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetDrag(),
					springValuesEditorObjects[Y].GetDrag());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetDrag(value.x);
				springValuesEditorObjects[Y].SetDrag(value.y);
			}
		}

		public Vector2 MinValue
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetMinValue(),
					springValuesEditorObjects[Y].GetMinValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMinValue(value.x);
				springValuesEditorObjects[Y].SetMinValue(value.y);
			}
		}

		public Vector2 MaxValue
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetMaxValue(),
					springValuesEditorObjects[Y].GetMaxValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMaxValue(value.x);
				springValuesEditorObjects[Y].SetMaxValue(value.y);
			}
		}

		public Vector2 Target
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetTarget(),
					springValuesEditorObjects[Y].GetTarget());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetTarget(value.x);
				springValuesEditorObjects[Y].SetTarget(value.y);
			}
		}

		public Vector2 CurrentValue
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetCurrentValue(),
					springValuesEditorObjects[Y].GetCurrentValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetCurrentValue(value.x);
				springValuesEditorObjects[Y].SetCurrentValue(value.y);
			}
		}

		public Vector2 Velocity
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetVelocity(),
					springValuesEditorObjects[Y].GetVelocity());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetVelocity(value.x);
				springValuesEditorObjects[Y].SetVelocity(value.y);
			}
		}

		public Vector2 OperationValue
		{
			get
			{
				Vector2 res = new Vector2(
					springValuesEditorObjects[X].GetOperationValue(),
					springValuesEditorObjects[Y].GetOperationValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetOperationValue(value.x);
				springValuesEditorObjects[Y].SetOperationValue(value.y);
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