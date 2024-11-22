using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class CamFovOrSizeSpringComponent
	{
		#region PUBLIC_FOV_SPRING_METHODS
		
		public SpringEvents Events => fovSpring.springEvents;
		public float GetTarget() => fovSpring.GetTarget();
		public void SetTarget(float target) => fovSpring.SetTarget(target);
		public float GetCurrentValue() => fovSpring.GetCurrentValue();
		public void SetCurrentValue(float currentValues) => fovSpring.SetCurrentValue(currentValues);
		public float GetVelocity() => fovSpring.GetVelocity();
		public void SetVelocity(float velocity) => fovSpring.SetVelocity(velocity);
		public void AddVelocity(float velocityToAdd) =>	fovSpring.AddVelocity(velocityToAdd);
		public float GetForce() => fovSpring.GetForce();
		public void SetForce(float force) => fovSpring.SetForce(force);
		public float GetDrag() => fovSpring.GetDrag();
		public void SetDrag(float drag) => fovSpring.SetDrag(drag);
		public void SetMinValues(float minValue) => fovSpring.SetMinValues(minValue);
		public void SetMaxValues(float maxValue) => fovSpring.SetMaxValues(maxValue);
		public void SetClampCurrentValues(bool clamp) => fovSpring.SetClampCurrentValues(clamp);
		public void SetClampTarget(bool clamp) => fovSpring.SetClampTarget(clamp);
		public void StopSpringOnClamp(bool stop) => fovSpring.StopSpringOnClamp(stop);
		public float GetUnifiedForce() => fovSpring.GetUnifiedForce();
		public float GetUnifiedDrag() => fovSpring.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			fovSpring.SetUnifiedForceAndDragEnabled(true);
			fovSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			fovSpring.SetUnifiedForceAndDragEnabled(true);
			fovSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		
		#endregion
	}
}