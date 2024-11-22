using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class FloatSpringComponent
	{
		#region PUBLIC_FLOAT_SPRING_METHODS
		
		public SpringEvents Events => springFloat.springEvents;
		public float GetTarget() => springFloat.GetTarget();
		public void SetTarget(float target) => springFloat.SetTarget(target);
		public float GetCurrentValue() => springFloat.GetCurrentValue();
		public void SetCurrentValue(float currentValues) => springFloat.SetCurrentValue(currentValues);
		public float GetVelocity() => springFloat.GetVelocity();
		public void SetVelocity(float velocity) => springFloat.SetVelocity(velocity);
		public void AddVelocity(float velocityToAdd) =>	springFloat.AddVelocity(velocityToAdd);
		public float GetForce() => springFloat.GetForce();
		public void SetForce(float force) => springFloat.SetForce(force);
		public float GetDrag() => springFloat.GetDrag();
		public void SetDrag(float drag) => springFloat.SetDrag(drag);
		public void SetMinValues(float minValue) => springFloat.SetMinValues(minValue);
		public void SetMaxValues(float maxValue) => springFloat.SetMaxValues(maxValue);
		public void SetClampCurrentValues(bool clamp) => springFloat.SetClampCurrentValues(clamp);
		public void SetClampTarget(bool clamp) => springFloat.SetClampTarget(clamp);
		public void StopSpringOnClamp(bool stop) => springFloat.StopSpringOnClamp(stop);
		public float GetUnifiedForce() => springFloat.GetUnifiedForce();
		public float GetUnifiedDrag() => springFloat.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			springFloat.SetUnifiedForceAndDragEnabled(true);
			springFloat.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			springFloat.SetUnifiedForceAndDragEnabled(true);
			springFloat.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		
		#endregion
	}
}