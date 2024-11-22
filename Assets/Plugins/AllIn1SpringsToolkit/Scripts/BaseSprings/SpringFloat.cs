namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringFloat : Spring
	{
		public const int SPRING_SIZE = 1;
		public const int X = 0;

		public SpringFloat() : base(SPRING_SIZE)
		{

		}

		public override bool HasValidSize()
		{
			bool res = springValues.Length == SPRING_SIZE;
			return res;
		}

		public override int GetSpringSize()
		{
			return SPRING_SIZE;
		}

		

		#region TARGET
		public float GetTarget()
		{
			float res = springValues[X].GetTarget();
			return res;
		}
		
		public void SetTarget(float target)
		{
			springValues[X].SetTarget(target);
		}
		#endregion

		#region CURRENT VALUES
		public float GetCurrentValue()
		{
			float res = springValues[X].GetCurrentValue();
			return res;
		}

		public void SetCurrentValue(float newCurrentValue)
		{
			springValues[X].SetCurrentValue(newCurrentValue);
		}
		#endregion

		#region VELOCITY
		public float GetVelocity()
		{
			float res = springValues[X].GetVelocity();
			return res;
		}

		public void AddVelocity(float velocity)
		{
			springValues[X].AddVelocity(velocity);
		}

		public void SetVelocity(float velocity)
		{
			springValues[X].SetVelocity(velocity);
		}
		#endregion

		#region CLAMPING

		public void SetMinValues(float minValue)
		{
			SetMinValueByIndex(X, minValue);
		}

		public void SetMaxValues(float maxValue)
		{
			SetMaxValueByIndex(X, maxValue);
		}

		public void SetClampTarget(bool clampTarget)
		{
			SetClampTargetByIndex(X, clampTarget);
		}

		public void SetClampCurrentValues(bool clampCurrentValue)
		{
			SetClampCurrentValueByIndex(X, clampCurrentValue);
		}

		public void StopSpringOnClamp(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stop);
		}

		#endregion

		public float GetForce()
		{
			float res = GetForceByIndex(X);
			return res;
		}

		public void SetForce(float force)
		{
			SetForceByIndex(X, force);
		}

		public float GetDrag()
		{
			float res = GetDragByIndex(X);
			return res;
		}

		public void SetDrag(float force)
		{
			SetForceByIndex(X, force);
		}
	}
}