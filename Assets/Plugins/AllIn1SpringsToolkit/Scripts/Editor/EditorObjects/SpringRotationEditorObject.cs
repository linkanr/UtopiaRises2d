using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class SpringRotationEditorObject : SpringEditorObject
	{
		public const int SPRING_SIZE = 10;

		private const int GLOBAL_AXIS_X = 0;
		private const int GLOBAL_AXIS_Y = 1;
		private const int GLOBAL_AXIS_Z = 2;

		private const int LOCAL_AXIS_X = 3;
		private const int LOCAL_AXIS_Y = 4;
		private const int LOCAL_AXIS_Z = 5;

		private const int ROTATION_AXIS_X = 6;
		private const int ROTATION_AXIS_Y = 7;
		private const int ROTATION_AXIS_Z = 8;
		private const int ANGLE = 9;

		public SerializedProperty spAxisRestriction;

		public SpringRotationEditorObject(SerializedProperty spParentProperty) : base(spParentProperty, SpringRotation.SPRING_SIZE)
		{
			this.spAxisRestriction = spParentProperty.FindPropertyRelative("axisRestriction");
		}

		public Vector3 InitialEuler
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[GLOBAL_AXIS_X].GetInitialValue(),
					springValuesEditorObjects[GLOBAL_AXIS_Y].GetInitialValue(),
					springValuesEditorObjects[GLOBAL_AXIS_Z].GetInitialValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[GLOBAL_AXIS_X].SetInitialValue(value.x);
				springValuesEditorObjects[GLOBAL_AXIS_Y].SetInitialValue(value.y);
				springValuesEditorObjects[GLOBAL_AXIS_Z].SetInitialValue(value.z);
			}
		}

		public Vector3 CurrentEuler
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[GLOBAL_AXIS_X].GetCurrentValue(),
					springValuesEditorObjects[GLOBAL_AXIS_Y].GetCurrentValue(),
					springValuesEditorObjects[GLOBAL_AXIS_Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[GLOBAL_AXIS_X].SetCurrentValue(value.x);
				springValuesEditorObjects[GLOBAL_AXIS_Y].SetCurrentValue(value.y);
				springValuesEditorObjects[GLOBAL_AXIS_Z].SetCurrentValue(value.z);
			}
		}

		public Vector3 TargetEuler
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[GLOBAL_AXIS_X].GetTarget(),
					springValuesEditorObjects[GLOBAL_AXIS_Y].GetTarget(),
					springValuesEditorObjects[GLOBAL_AXIS_Z].GetTarget());

				return res;
			}
			set
			{
				springValuesEditorObjects[GLOBAL_AXIS_X].SetTarget(value.x);
				springValuesEditorObjects[GLOBAL_AXIS_Y].SetTarget(value.y);
				springValuesEditorObjects[GLOBAL_AXIS_Z].SetTarget(value.z);
			}
		}

		public Vector3Bool Update
		{
			get
			{
				Vector3Bool res = new Vector3Bool(
					springValuesEditorObjects[GLOBAL_AXIS_X].GetUpdate(),
					springValuesEditorObjects[GLOBAL_AXIS_Y].GetUpdate(),
					springValuesEditorObjects[GLOBAL_AXIS_Z].GetUpdate());

				return res;
			}
			set
			{
				springValuesEditorObjects[GLOBAL_AXIS_X].SetUpdate(value.x);
				springValuesEditorObjects[GLOBAL_AXIS_Y].SetUpdate(value.y);
				springValuesEditorObjects[GLOBAL_AXIS_Z].SetUpdate(value.z);
			}
		}

		public Vector3 ForceEuler
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[GLOBAL_AXIS_X].GetForce(),
					springValuesEditorObjects[GLOBAL_AXIS_Y].GetForce(),
					springValuesEditorObjects[GLOBAL_AXIS_Z].GetForce());

				return res;
			}
			set
			{
				springValuesEditorObjects[GLOBAL_AXIS_X].SetForce(value.x);
				springValuesEditorObjects[GLOBAL_AXIS_Y].SetForce(value.y);
				springValuesEditorObjects[GLOBAL_AXIS_Z].SetForce(value.z);
			}
		}

		public Vector3 DragEuler
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[GLOBAL_AXIS_X].GetDrag(),
					springValuesEditorObjects[GLOBAL_AXIS_Y].GetDrag(),
					springValuesEditorObjects[GLOBAL_AXIS_Z].GetDrag());

				return res;
			}
			set
			{
				springValuesEditorObjects[GLOBAL_AXIS_X].SetDrag(value.x);
				springValuesEditorObjects[GLOBAL_AXIS_Y].SetDrag(value.y);
				springValuesEditorObjects[GLOBAL_AXIS_Z].SetDrag(value.z);
			}
		}

		public Vector3 VelocityEuler
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[LOCAL_AXIS_X].GetVelocity(),
					springValuesEditorObjects[LOCAL_AXIS_Y].GetVelocity(),
					springValuesEditorObjects[LOCAL_AXIS_Z].GetVelocity());

				return res;
			}
			set
			{
				springValuesEditorObjects[LOCAL_AXIS_X].SetVelocity(value.x);
				springValuesEditorObjects[LOCAL_AXIS_Y].SetVelocity(value.y);
				springValuesEditorObjects[LOCAL_AXIS_Z].SetVelocity(value.z);
			}
		}

		public Vector3 OperationValue
		{
			get
			{
				Vector3 res = new Vector3(
					springValuesEditorObjects[0].GetOperationValue(),
					springValuesEditorObjects[1].GetOperationValue(),
					springValuesEditorObjects[2].GetOperationValue());

				return res;
			}
			set
			{
				springValuesEditorObjects[0].SetOperationValue(value.x);
				springValuesEditorObjects[1].SetOperationValue(value.y);
				springValuesEditorObjects[2].SetOperationValue(value.z);
			}
		}

		public override void AddVelocityNudge()
		{
			const float velocityFactor = 150;

			VelocityEuler += OperationValue * velocityFactor;
		}

		public override void SetCurrentValuesNudge()
		{
			CurrentEuler = OperationValue;
		}

		public override void SetTargetNudge()
		{
			TargetEuler = OperationValue;
		}

		public SpringRotation.AxisRestriction AxisRestriction
		{
			get
			{
				SpringRotation.AxisRestriction res = (SpringRotation.AxisRestriction)spAxisRestriction.enumValueIndex;
				return res;
			}
		}

		public override float GetExtraFieldsHeight()
		{
			float res = 0f;

			return res;
		}
	}
}