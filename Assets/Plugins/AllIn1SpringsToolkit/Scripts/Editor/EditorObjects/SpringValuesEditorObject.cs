using UnityEditor;

namespace AllIn1SpringsToolkit
{
	public class SpringValuesEditorObject
	{
		public SerializedProperty spInitialValue;

		public SerializedProperty spClampTarget;
		public SerializedProperty spClampCurrentValue;
		public SerializedProperty spStopSpringOnCurrentValueClamp;

		public SerializedProperty spMinValue;
		public SerializedProperty spMaxValue;

		public SerializedProperty spUpdate;
		public SerializedProperty spForce;
		public SerializedProperty spDrag;

		public SerializedProperty spTarget;

		public SerializedProperty spCurrentValue;
		public SerializedProperty spVelocity;

		public SerializedProperty spCandidateValue;

		public SerializedProperty spOperationValue;

		public SpringValuesEditorObject(
			SerializedProperty spInitialValue,
			SerializedProperty spClampTarget, SerializedProperty spClampCurrentValue, SerializedProperty spStopSpringOnCurrentValueClamp,
			SerializedProperty spMinValue, SerializedProperty spMaxValue,
			SerializedProperty spUpdate, SerializedProperty spForce, SerializedProperty spDrag,
			SerializedProperty spTarget,
			SerializedProperty spCurrentValue, SerializedProperty spVelocity,
			SerializedProperty spCandidateValue,
			SerializedProperty spOperationValue)
		{
			this.spInitialValue = spInitialValue;

			this.spClampTarget = spClampTarget;
			this.spClampCurrentValue = spClampCurrentValue;
			this.spStopSpringOnCurrentValueClamp = spStopSpringOnCurrentValueClamp;

			this.spMinValue = spMinValue;
			this.spMaxValue = spMaxValue;

			this.spUpdate = spUpdate;
			this.spForce = spForce;
			this.spDrag = spDrag;

			this.spTarget = spTarget;

			this.spCurrentValue = spCurrentValue;
			this.spVelocity = spVelocity;

			this.spCandidateValue = spCandidateValue;

			this.spOperationValue = spOperationValue;
		}

		public void SetInitialValue(float initialValue)
		{
			this.spInitialValue.floatValue = initialValue;
		}

		public float GetInitialValue()
		{
			float res = spInitialValue.floatValue;
			return res;
		}

		public bool GetUpdate()
		{
			bool res = spUpdate.boolValue;
			return res;
		}

		public void SetUpdate(bool update)
		{
			spUpdate.boolValue = update;
		}

		public float GetForce()
		{
			float res = spForce.floatValue;
			return res;
		}

		public void SetForce(float force)
		{
			spForce.floatValue = force;
		}

		public float GetDrag()
		{
			float res = spDrag.floatValue;
			return res;
		}

		public void SetDrag(float drag)
		{
			spDrag.floatValue = drag;
		}

		public bool GetClampTarget()
		{
			bool res = spClampTarget.boolValue;
			return res;
		}

		public void SetClampTarget(bool value)
		{
			spClampTarget.boolValue = value;
		}

		public bool GetClampCurrentValue()
		{
			bool res = spClampCurrentValue.boolValue;
			return res;
		}

		public void SetClampCurrentValue(bool value)
		{
			spClampCurrentValue.boolValue = value;
		}

		public bool GetStopSpringOnCurrentValueClamp()
		{
			bool res = spStopSpringOnCurrentValueClamp.boolValue;
			return res;
		}

		public void SetStopSpringOnCurrentValueClamp(bool value)
		{
			spStopSpringOnCurrentValueClamp.boolValue = value;
		}

		public float GetMinValue()
		{
			float res = spMinValue.floatValue;
			return res;
		}

		public void SetMinValue(float minValue)
		{
			spMinValue.floatValue = minValue;
		}

		public float GetMaxValue()
		{
			float res = spMaxValue.floatValue;
			return res;
		}

		public void SetMaxValue(float maxValue)
		{
			spMaxValue.floatValue = maxValue;
		}

		public float GetTarget()
		{
			float res = spTarget.floatValue;
			return res;
		}

		public void SetTarget(float target)
		{
			spTarget.floatValue = target;
		}

		public float GetCandidateValue()
		{
			float res = spCandidateValue.floatValue;
			return res;
		}

		public void SetCandidateValue(float value)
		{
			spCandidateValue.floatValue = value;
		}

		public float GetCurrentValue()
		{
			float res = spCurrentValue.floatValue;
			return res;
		}

		public void SetCurrentValue(float currentValue)
		{
			if (spUpdate.boolValue)
			{
				spCurrentValue.floatValue = currentValue;
			}
		}

		public float GetVelocity()
		{
			float res = spVelocity.floatValue;
			return res;
		}

		public void SetVelocity(float velocity)
		{
			spVelocity.floatValue = velocity;
		}

		public float GetOperationValue()
		{
			float res = spOperationValue.floatValue;
			return res;
		}

		public void SetOperationValue(float value)
		{
			spOperationValue.floatValue = value;
		}
	}
}