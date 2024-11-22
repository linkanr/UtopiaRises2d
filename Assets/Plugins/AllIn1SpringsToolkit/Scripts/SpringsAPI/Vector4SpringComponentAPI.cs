using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class Vector4SpringComponent
	{
		#region PUBLIC_VECTOR4_SPRING_METHODS
		
		public SpringEvents Events => springVector4.springEvents;
		public Vector4 GetTarget() => springVector4.GetTarget();
		public void SetTarget(Vector4 target) => springVector4.SetTarget(target);
		public void SetTarget(float target) => SetTarget(Vector4.one * target);
		public Vector4 GetCurrentValue() => springVector4.GetCurrentValue();
		public void SetCurrentValue(Vector4 currentValues) => springVector4.SetCurrentValue(currentValues);
		public void SetCurrentValue(float currentValues) => SetCurrentValue(Vector4.one * currentValues);
		public Vector4 GetVelocity() => springVector4.GetVelocity();
		public void SetVelocity(Vector4 velocity) => springVector4.SetVelocity(velocity);
		public void SetVelocity(float velocity) => SetVelocity(Vector4.one * velocity);
		public void AddVelocity(Vector4 velocityToAdd) =>	springVector4.AddVelocity(velocityToAdd);
		public Vector4 GetForce() => springVector4.GetForce();
		public void SetForce(Vector4 force) => springVector4.SetForce(force);
		public void SetForce(float force) => SetForce(Vector4.one * force);
		public Vector4 GetDrag() => springVector4.GetDrag();
		public void SetDrag(Vector4 drag) => springVector4.SetDrag(drag);
		public void SetDrag(float drag) => SetDrag(Vector4.one * drag);
		public float GetUnifiedForce() => springVector4.GetUnifiedForce();
		public float GetUnifiedDrag() => springVector4.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			springVector4.SetUnifiedForceAndDragEnabled(true);
			springVector4.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			springVector4.SetUnifiedForceAndDragEnabled(true);
			springVector4.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		public void SetMinValues(Vector4 minValue) => springVector4.SetMinValues(minValue);
		public void SetMinValues(float minValue) => SetMinValues(Vector4.one * minValue);
		public void SetMaxValues(Vector4 maxValue) => springVector4.SetMaxValues(maxValue);
		public void SetMaxValues(float maxValue) => SetMaxValues(Vector4.one * maxValue);
		public void SetClampCurrentValues(bool clampTargetX, bool clampTargetY, bool clampTargetZ, bool clampTargetW) => springVector4.SetClampCurrentValues(clampTargetX, clampTargetY, clampTargetZ, clampTargetW);
		public void SetClampTarget(bool clampTargetX, bool clampTargetY, bool clampTargetZ, bool clampTargetW) => springVector4.SetClampTarget(clampTargetX, clampTargetY, clampTargetZ, clampTargetW);
		public void StopSpringOnClamp(bool stopX, bool stopY, bool stopZ, bool stopW) => springVector4.StopSpringOnClamp(stopX, stopY, stopZ, stopW);
		
		#endregion
	}
}