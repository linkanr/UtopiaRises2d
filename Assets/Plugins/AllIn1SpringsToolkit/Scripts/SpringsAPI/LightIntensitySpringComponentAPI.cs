using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class LightIntensitySpringComponent
	{
		#region PUBLIC_LIGHTINTENSITY_SPRING_METHODS
		
		public SpringEvents Events => lightIntensitySpring.springEvents;
		public float GetTarget() => lightIntensitySpring.GetTarget();
		public void SetTarget(float target) => lightIntensitySpring.SetTarget(target);
		public float GetCurrentValue() => lightIntensitySpring.GetCurrentValue();
		public void SetCurrentValue(float currentValues) => lightIntensitySpring.SetCurrentValue(currentValues);
		public float GetVelocity() => lightIntensitySpring.GetVelocity();
		public void SetVelocity(float velocity) => lightIntensitySpring.SetVelocity(velocity);
		public void AddVelocity(float velocityToAdd) =>	lightIntensitySpring.AddVelocity(velocityToAdd);
		public float GetForce() => lightIntensitySpring.GetForce();
		public void SetForce(float force) => lightIntensitySpring.SetForce(force);
		public float GetDrag() => lightIntensitySpring.GetDrag();
		public void SetDrag(float drag) => lightIntensitySpring.SetDrag(drag);
		public void SetMinValues(float minValue) => lightIntensitySpring.SetMinValues(minValue);
		public void SetMaxValues(float maxValue) => lightIntensitySpring.SetMaxValues(maxValue);
		public void SetClampCurrentValues(bool clamp) => lightIntensitySpring.SetClampCurrentValues(clamp);
		public void SetClampTarget(bool clamp) => lightIntensitySpring.SetClampTarget(clamp);
		public void StopSpringOnClamp(bool stop) => lightIntensitySpring.StopSpringOnClamp(stop);
		public float GetUnifiedForce() => lightIntensitySpring.GetUnifiedForce();
		public float GetUnifiedDrag() => lightIntensitySpring.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			lightIntensitySpring.SetUnifiedForceAndDragEnabled(true);
			lightIntensitySpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			lightIntensitySpring.SetUnifiedForceAndDragEnabled(true);
			lightIntensitySpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		
		#endregion
	}
}