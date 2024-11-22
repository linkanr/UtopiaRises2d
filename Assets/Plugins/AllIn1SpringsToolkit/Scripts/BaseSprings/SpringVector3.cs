using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringVector3 : Spring
	{
		public const int SPRING_SIZE = 3;

		private const int X = 0;
		private const int Y = 1;
		private const int Z = 2;

		public SpringVector3() : base(SPRING_SIZE)
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
		public Vector3 GetTarget()
		{
			Vector3 res = new Vector3(
				springValues[X].GetTarget(), 
				springValues[Y].GetTarget(),
				springValues[Z].GetTarget());

			return res;
		}
		
		public void SetTarget(Vector3 target)
		{
			springValues[X].SetTarget(target.x);
			springValues[Y].SetTarget(target.y);
			springValues[Z].SetTarget(target.z);
		}

		public void SetTarget(float value)
		{
			SetTarget(Vector3.one * value);
		}
		#endregion

		#region CURRENT VALUES
		public Vector3 GetCurrentValue()
		{
			Vector3 res = new Vector3(springValues[X].GetCurrentValue(), springValues[Y].GetCurrentValue(), springValues[Z].GetCurrentValue());
			return res;
		}
		
		public void SetCurrentValue(Vector3 value)
		{
			springValues[X].SetCurrentValue(value.x);
			springValues[Y].SetCurrentValue(value.y);
			springValues[Z].SetCurrentValue(value.z);
		}

		public void SetCurrentValue(float value)
		{
			SetCurrentValue(Vector3.one * value);
		}
		#endregion

		#region VELOCITY
		public Vector3 GetVelocity()
		{
			Vector3 res = new Vector3(springValues[X].GetVelocity(), springValues[Y].GetVelocity(), springValues[Z].GetVelocity());
			return res;
		}

		public void AddVelocity(Vector3 velocity)
		{
			springValues[X].AddVelocity(velocity.x);
			springValues[Y].AddVelocity(velocity.y);
			springValues[Z].AddVelocity(velocity.z);
		}

		public void SetVelocity(Vector3 velocity)
		{
			springValues[X].SetVelocity(velocity.x);
			springValues[Y].SetVelocity(velocity.y);
			springValues[Z].SetVelocity(velocity.z);
		}

		public void SetVelocity(float value)
		{
			SetVelocity(Vector3.one * value);
		}
		#endregion

		#region FORCE AND DRAG

		public Vector3 GetForce()
		{
			Vector3 res = new Vector3(GetForceByIndex(X), GetForceByIndex(Y), GetForceByIndex(Z));
			return res;
		}

		public void SetForce(Vector3 force)
		{
			SetForceByIndex(X, force.x);
			SetForceByIndex(Y, force.y);
			SetForceByIndex(Z, force.z);
		}

		public void SetForce(float value)
		{
			SetForce(Vector3.one * value);
		}

		public Vector3 GetDrag()
		{
			Vector3 res = new Vector3(GetDragByIndex(X), GetDragByIndex(Y), GetDragByIndex(Z));
			return res;
		}

		public void SetDrag(Vector3 drag)
		{
			SetDragByIndex(X, drag.x);
			SetDragByIndex(Y, drag.y);
			SetDragByIndex(Z, drag.z);
		}

		public void SetDrag(float value)
		{
			SetDrag(Vector3.one * value);
		}
		#endregion

		#region CLAMPING
		public void SetMinValues(Vector3 minValues)
		{
			SetMinValueByIndex(X, minValues.x);
			SetMinValueByIndex(Y, minValues.y);
			SetMinValueByIndex(Z, minValues.z);
		}

		public void SetMinValues(float value)
		{
			SetMinValues(Vector3.one * value);
		}

		public void SetMinValueX(float minValue)
		{
			SetMinValueByIndex(X, minValue);
		}

		public void SetMinValueY(float minValue)
		{
			SetMinValueByIndex(Y, minValue);
		}

		public void SetMinValueZ(float minValue)
		{
			SetMinValueByIndex(Z, minValue);
		}

		public void SetMaxValues(Vector3 maxValues)
		{
			SetMaxValueByIndex(X, maxValues.x);
			SetMaxValueByIndex(Y, maxValues.y);
			SetMaxValueByIndex(Z, maxValues.z);
		}

		public void SetMaxValues(float value)
		{
			SetMaxValues(Vector3.one * value);
		}

		public void SetMaxValueX(float maxValue)
		{
			SetMaxValueByIndex(X, maxValue);
		}

		public void SetMaxValueY(float maxValue)
		{
			SetMaxValueByIndex(Y, maxValue);
		}

		public void SetMaxValueZ(float maxValue)
		{
			SetMaxValueByIndex(Z, maxValue);
		}

		public void SetClampCurrentValues(bool clampX, bool clampY, bool clampZ)
		{
			SetClampCurrentValueByIndex(X, clampX);
			SetClampCurrentValueByIndex(Y, clampY);
			SetClampCurrentValueByIndex(Z, clampZ);
		}

		public void SetClampTarget(bool clampX, bool clampY, bool clampZ)
		{
			SetClampTargetByIndex(X, clampX);
			SetClampTargetByIndex(Y, clampY);
			SetClampTargetByIndex(Z, clampZ);
		}

		public void StopSpringOnClamp(bool stopX, bool stopY, bool stopZ)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stopX);
			SetStopSpringOnCurrentValueClampByIndex(Y, stopY);
			SetStopSpringOnCurrentValueClampByIndex(Z, stopZ);
		}

		public void StopSpringOnClampX(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stop);
		}

		public void StopSpringOnClampY(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(Y, stop);
		}

		public void StopSpringOnClampZ(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(Z, stop);
		}
		#endregion
	}
}