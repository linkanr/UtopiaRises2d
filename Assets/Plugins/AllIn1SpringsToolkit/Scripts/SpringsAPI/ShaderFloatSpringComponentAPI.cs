using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class ShaderFloatSpringComponent
	{
		#region PUBLIC_SHADERVALUE_SPRING_METHODS
		
		public SpringEvents Events => shaderValueSpring.springEvents;
		public float GetTarget() => shaderValueSpring.GetTarget();
		public void SetTarget(float target) => shaderValueSpring.SetTarget(target);
		public float GetCurrentValue() => shaderValueSpring.GetCurrentValue();
		public void SetCurrentValue(float currentValues) => shaderValueSpring.SetCurrentValue(currentValues);
		public float GetVelocity() => shaderValueSpring.GetVelocity();
		public void SetVelocity(float velocity) => shaderValueSpring.SetVelocity(velocity);
		public void AddVelocity(float velocityToAdd) =>	shaderValueSpring.AddVelocity(velocityToAdd);
		public float GetForce() => shaderValueSpring.GetForce();
		public void SetForce(float force) => shaderValueSpring.SetForce(force);
		public float GetDrag() => shaderValueSpring.GetDrag();
		public void SetDrag(float drag) => shaderValueSpring.SetDrag(drag);
		public void SetMinValues(float minValue) => shaderValueSpring.SetMinValues(minValue);
		public void SetMaxValues(float maxValue) => shaderValueSpring.SetMaxValues(maxValue);
		public void SetClampCurrentValues(bool clamp) => shaderValueSpring.SetClampCurrentValues(clamp);
		public void SetClampTarget(bool clamp) => shaderValueSpring.SetClampTarget(clamp);
		public void StopSpringOnClamp(bool stop) => shaderValueSpring.StopSpringOnClamp(stop);
		public float GetUnifiedForce() => shaderValueSpring.GetUnifiedForce();
		public float GetUnifiedDrag() => shaderValueSpring.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			shaderValueSpring.SetUnifiedForceAndDragEnabled(true);
			shaderValueSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			shaderValueSpring.SetUnifiedForceAndDragEnabled(true);
			shaderValueSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		
		#endregion
	}
}