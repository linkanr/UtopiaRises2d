using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringVector2 : Spring
	{
		public const int SPRING_SIZE = 2;

		private const int X = 0;
		private const int Y = 1;

		public SpringVector2() : base(SPRING_SIZE)
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
		public Vector2 GetTarget()
		{
			Vector2 res = new Vector2(
				springValues[X].GetTarget(),
				springValues[Y].GetTarget());

			return res;
		}
		
		public void SetTarget(Vector2 target)
		{
			springValues[X].SetTarget(target.x);
			springValues[Y].SetTarget(target.y);
		}

		public void SetTarget(float target)
		{
			SetTarget(Vector2.one * target);
		}
		#endregion

		#region CURRENT VALUES
		public Vector2 GetCurrentValue()
		{
			Vector2 res = new Vector2(springValues[X].GetCurrentValue(), springValues[Y].GetCurrentValue());
			return res;
		}
		
		public void SetCurrentValue(Vector2 value)
		{
			springValues[X].SetCurrentValue(value.x);
			springValues[Y].SetCurrentValue(value.y);
		}

		public void SetCurrentValue(float value)
		{
			SetCurrentValue(Vector2.one * value);
		}
		#endregion

		#region VELOCITY
		public Vector2 GetVelocity()
		{
			Vector2 res = new Vector2(springValues[X].GetVelocity(), springValues[Y].GetVelocity());
			return res;
		}

		public void AddVelocity(Vector2 velocity)
		{
			springValues[X].AddVelocity(velocity.x);
			springValues[Y].AddVelocity(velocity.y);
		}

		public void AddVelocity(float value)
		{
			AddVelocity(Vector2.one * value);
		}

		public void SetVelocity(Vector2 velocity)
		{
			springValues[X].SetVelocity(velocity.x);
			springValues[Y].SetVelocity(velocity.y);
		}

		public void SetVelocity(float value)
		{
			SetVelocity(Vector2.one * value);
		}
		#endregion

		#region FORCE AND DRAG
		public Vector2 GetForce()
		{
			Vector2 res = new Vector2(GetForceByIndex(X), GetForceByIndex(Y));
			return res;
		}

		public void SetForce(Vector2 force)
		{
			SetForceByIndex(X, force.x);
			SetForceByIndex(Y, force.y);
		}

		public void SetForce(float value)
		{
			SetForce(Vector2.one * value);
		}

		public Vector2 GetDrag()
		{
			Vector2 res = new Vector2(GetDragByIndex(X), GetDragByIndex(Y));
			return res;
		}

		public void SetDrag(Vector2 drag)
		{
			SetDragByIndex(X, drag.x);
			SetDragByIndex(Y, drag.y);
		}

		public void SetDrag(float value)
		{
			SetDrag(Vector2.one * value);
		}
		#endregion

		#region CLAMPING
		public void SetMinValues(Vector2 minValues)
		{
			SetMinValueByIndex(X, minValues.x);
			SetMinValueByIndex(Y, minValues.y);
		}

		public void SetMinValues(float value)
		{
			SetMinValues(Vector2.one * value);
		}

		public void SetMinValueX(float minValue)
		{
			SetMinValueByIndex(X, minValue);
		}

		public void SetMinValueY(float minValue)
		{
			SetMinValueByIndex(Y, minValue);
		}

		public void SetMaxValues(Vector2 maxValues)
		{
			SetMaxValueByIndex(X, maxValues.x);
			SetMaxValueByIndex(Y, maxValues.y);
		}

		public void SetMaxValues(float value)
		{
			SetMaxValues(Vector2.one * value);
		}

		public void SetMaxValueX(float maxValue)
		{
			SetMaxValueByIndex(X, maxValue);
		}

		public void SetMaxValueY(float maxValue)
		{
			SetMaxValueByIndex(Y, maxValue);
		}

		public void StopSpringOnClamp(bool stopX, bool stopY)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stopX);
			SetStopSpringOnCurrentValueClampByIndex(Y, stopY);
		}

		public void StopSpringOnClampX(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stop);
		}

		public void StopSpringOnClampY(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(Y, stop);
		}

		public void SetClampTarget(bool clampTargetX, bool clampTargetY)
		{
			SetClampTargetByIndex(X, clampTargetX);
			SetClampTargetByIndex(Y, clampTargetY);
		}

		public void SetClampCurrentValues(bool clampCurrentValueX, bool clampCurrentValueY)
		{
			SetClampCurrentValueByIndex(X, clampCurrentValueX);
			SetClampCurrentValueByIndex(Y, clampCurrentValueY);
		}
		#endregion
	}
}
