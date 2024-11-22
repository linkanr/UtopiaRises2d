using UnityEngine;
namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringColor : Spring
	{
		public const int SPRING_SIZE = 4;

		private const int R = 0;
		private const int G = 1;
		private const int B = 2;
		private const int A = 3;

		public SpringColor() : base(SPRING_SIZE)
		{
			SetDefaultValues();
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

		private void SetDefaultValues()
		{
			SetCurrentValue(Color.white);
			SetTarget(Color.white);
		}

		#region TARGET
		public Vector4 GetTarget()
		{
			Vector4 res = new Vector4(
				springValues[R].GetTarget(),
				springValues[G].GetTarget(),
				springValues[B].GetTarget(),
				springValues[A].GetTarget());

			return res;
		}
		
		public Color GetTargetColor()
		{
			Color res = new Color(
				springValues[R].GetTarget(),
				springValues[G].GetTarget(),
				springValues[B].GetTarget(),
				springValues[A].GetTarget());

			return res;
		}
		
		public void SetTarget(Color color)
		{
			springValues[R].SetTarget(color.r);
			springValues[G].SetTarget(color.g);
			springValues[B].SetTarget(color.b);
			springValues[A].SetTarget(color.a);
		}
		#endregion

		#region CURRENT VALUES
		public Vector4 GetCurrentValue()
		{
			Vector4 res = new Vector4(springValues[R].GetCurrentValue(), springValues[G].GetCurrentValue(), springValues[B].GetCurrentValue(), springValues[A].GetCurrentValue());
			return res;
		}
		
		public Color GetCurrentColor()
		{
			Color res = new Color(springValues[R].GetCurrentValue(), springValues[G].GetCurrentValue(), springValues[B].GetCurrentValue(), springValues[A].GetCurrentValue());
			return res;
		}

		public void SetCurrentValue(Color color)
		{
			springValues[R].SetCurrentValue(color.r);
			springValues[G].SetCurrentValue(color.g);
			springValues[B].SetCurrentValue(color.b);
			springValues[A].SetCurrentValue(color.a);
		}
		#endregion

		#region VELOCITY
		public Vector4 GetVelocity()
		{
			Vector4 res = new Vector4(springValues[R].GetVelocity(), springValues[G].GetVelocity(), springValues[B].GetVelocity(), springValues[A].GetVelocity());
			return res;
		}

		public void AddVelocity(Vector4 velocity)
		{
			springValues[R].AddVelocity(velocity.x);
			springValues[G].AddVelocity(velocity.y);
			springValues[B].AddVelocity(velocity.z);
			springValues[A].AddVelocity(velocity.w);
		}

		public void SetVelocity(Vector4 velocity)
		{
			springValues[R].SetVelocity(velocity.x);
			springValues[G].SetVelocity(velocity.y);
			springValues[B].SetVelocity(velocity.z);
			springValues[A].SetVelocity(velocity.w);

		}
		#endregion

		#region FORCE AND DRAG
		public Vector4 GetForce()
		{
			Vector4 res = new Vector4(
				GetForceByIndex(R),
				GetForceByIndex(G),
				GetForceByIndex(B),
				GetForceByIndex(A));

			return res;
		}

		public void SetForce(Vector4 force)
		{
			SetForceByIndex(R, force.x);
			SetForceByIndex(G, force.y);
			SetForceByIndex(B, force.z);
			SetForceByIndex(A, force.w);
		}

		public void SetForce(float force)
		{
			SetForce(Vector4.one * force);
		}

		public Vector4 GetDrag()
		{
			Vector4 res = new Vector4(
				GetDragByIndex(R),
				GetDragByIndex(G),
				GetDragByIndex(B),
				GetDragByIndex(A));

			return res;
		}

		public void SetDrag(Vector4 drag)
		{
			SetDragByIndex(R, drag.x);
			SetDragByIndex(G, drag.y);
			SetDragByIndex(B, drag.z);
			SetDragByIndex(A, drag.w);
		}

		public void SetDrag(float drag)
		{
			SetDrag(Vector4.one * drag);
		}

		#endregion

		#region CLAMPING

		public void SetMinValues(Vector4 minValues)
		{
			SetMinValueByIndex(R, minValues.x);
			SetMinValueByIndex(G, minValues.y);
			SetMinValueByIndex(B, minValues.z);
			SetMinValueByIndex(A, minValues.w);
		}

		public void SetMinValueR(float minValue)
		{
			SetMinValueByIndex(R, minValue);
		}

		public void SetMinValueG(float minValue)
		{
			SetMinValueByIndex(G, minValue);
		}

		public void SetMinValueB(float minValue)
		{
			SetMinValueByIndex(B, minValue);
		}

		public void SetMinValueA(float minValue)
		{
			SetMinValueByIndex(A, minValue);
		}

		public void SetMaxValues(Vector4 maxValues)
		{
			SetMaxValueByIndex(R, maxValues.x);
			SetMaxValueByIndex(G, maxValues.y);
			SetMaxValueByIndex(B, maxValues.z);
			SetMaxValueByIndex(A, maxValues.w);
		}

		public void SetMaxValueR(float maxValue)
		{
			SetMaxValueByIndex(R, maxValue);
		}

		public void SetMaxValueG(float maxValue)
		{
			SetMaxValueByIndex(G, maxValue);
		}

		public void SetMaxValueB(float maxValue)
		{
			SetMaxValueByIndex(B, maxValue);
		}

		public void SetMaxValueA(float maxValue)
		{
			SetMaxValueByIndex(A, maxValue);
		}

		public void SetClampCurrentValues(bool clampR, bool clampG, bool clampB, bool clampA)
		{
			SetClampCurrentValueByIndex(R, clampR);
			SetClampCurrentValueByIndex(G, clampG);
			SetClampCurrentValueByIndex(B, clampB);
			SetClampCurrentValueByIndex(A, clampA);
		}

		public void SetClampTarget(bool clampR, bool clampG, bool clampB, bool clampA)
		{
			SetClampTargetByIndex(R, clampR);
			SetClampTargetByIndex(G, clampG);
			SetClampTargetByIndex(B, clampB);
			SetClampTargetByIndex(A, clampA);
		}

		public void StopSpringOnClamp(bool stopR, bool stopG, bool stopB, bool stopA)
		{
			SetStopSpringOnCurrentValueClampByIndex(R, stopR);
			SetStopSpringOnCurrentValueClampByIndex(G, stopG);
			SetStopSpringOnCurrentValueClampByIndex(B, stopB);
			SetStopSpringOnCurrentValueClampByIndex(A, stopA);
		}

		public void StopSpringOnClampR(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(R, stop);
		}

		public void StopSpringOnClampG(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(G, stop);
		}

		public void StopSpringOnClampB(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(B, stop);
		}

		public void StopSpringOnClampA(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(A, stop);
		}
		#endregion
	}
}