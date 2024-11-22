using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class ColorSpringComponent
	{
		#region PUBLIC_COLOR_SPRING_METHODS
		
		public SpringEvents Events => colorSpring.springEvents;
		public Color GetTarget() => colorSpring.GetTarget();
		public void SetTarget(Color target) => colorSpring.SetTarget(target);
		public Color GetCurrentValue() => colorSpring.GetCurrentValue();
		public void SetCurrentValue(Color currentValues)
		{
			colorSpring.SetCurrentValue(currentValues);
			SetCurrentValueInternal(currentValues);
		}
		public Vector4 GetVelocity() => colorSpring.GetVelocity();
		public void SetVelocity(Vector4 velocity) => colorSpring.SetVelocity(velocity);
		public void SetVelocity(float velocity) => SetVelocity(Vector4.one * velocity);
		public void AddVelocity(Vector4 velocityToAdd) => colorSpring.AddVelocity(velocityToAdd);
		public override void ReachEquilibrium()
		{
			base.ReachEquilibrium();
			ReachEquilibriumInternal();
		}
		public Vector4 GetForce() => colorSpring.GetForce();
		public void SetForce(Vector4 force) => colorSpring.SetForce(force);
		public void SetForce(float force) => SetForce(Vector4.one * force);
		public Vector4 GetDrag() => colorSpring.GetDrag();
		public void SetDrag(Vector4 drag) => colorSpring.SetDrag(drag);
		public void SetDrag(float drag) => SetDrag(Vector4.one * drag);
		public float GetUnifiedForce() => colorSpring.GetUnifiedForce();
		public float GetUnifiedDrag() => colorSpring.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			colorSpring.SetUnifiedForceAndDragEnabled(true);
			colorSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			colorSpring.SetUnifiedForceAndDragEnabled(true);
			colorSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		public void SetMinValues(Vector4 minValue) => colorSpring.SetMinValues(minValue);
		public void SetMaxValues(Vector4 maxValue) => colorSpring.SetMaxValues(maxValue);
		public void SetClampCurrentValues(bool clampR, bool clampG, bool clampB, bool clampA) => colorSpring.SetClampCurrentValues(clampR, clampG, clampB, clampA);
		public void SetClampTarget(bool clampR, bool clampG, bool clampB, bool clampA) => colorSpring.SetClampTarget(clampR, clampG, clampB, clampA);
		public void StopSpringOnClamp(bool stopR, bool stopG, bool stopB, bool stopA) => colorSpring.StopSpringOnClamp(stopR, stopG, stopB, stopA);
		
		#endregion
	}
}