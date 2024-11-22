using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class Vector3SpringComponent
	{
		#region PUBLIC_VECTOR3_SPRING_METHODS
		
		public SpringEvents Events => springVector3.springEvents;
		public Vector3 GetTarget() => springVector3.GetTarget();
		public void SetTarget(Vector3 target) => springVector3.SetTarget(target);
		public void SetTarget(float target) => SetTarget(Vector3.one * target);
		public Vector3 GetCurrentValue() => springVector3.GetCurrentValue();
		public void SetCurrentValue(Vector3 currentValues) => springVector3.SetCurrentValue(currentValues);
		public void SetCurrentValue(float currentValues) => SetCurrentValue(Vector3.one * currentValues);
		public Vector3 GetVelocity() => springVector3.GetVelocity();
		public void SetVelocity(Vector3 velocity) => springVector3.SetVelocity(velocity);
		public void SetVelocity(float velocity) => SetVelocity(Vector3.one * velocity);
		public void AddVelocity(Vector3 velocityToAdd) =>	springVector3.AddVelocity(velocityToAdd);
		public Vector3 GetForce() => springVector3.GetForce();
		public void SetForce(Vector3 force) => springVector3.SetForce(force);
		public void SetForce(float force) => SetForce(Vector3.one * force);
		public Vector3 GetDrag() => springVector3.GetDrag();
		public void SetDrag(Vector3 drag) => springVector3.SetDrag(drag);
		public void SetDrag(float drag) => SetDrag(Vector3.one * drag);
		public float GetUnifiedForce() => springVector3.GetUnifiedForce();
		public float GetUnifiedDrag() => springVector3.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			springVector3.SetUnifiedForceAndDragEnabled(true);
			springVector3.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			springVector3.SetUnifiedForceAndDragEnabled(true);
			springVector3.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		public void SetMinValues(Vector3 minValue) => springVector3.SetMinValues(minValue);
		public void SetMinValues(float minValue) => SetMinValues(Vector3.one * minValue);
		public void SetMaxValues(Vector3 maxValue) => springVector3.SetMaxValues(maxValue);
		public void SetMaxValues(float maxValue) => SetMaxValues(Vector3.one * maxValue);
		public void SetClampCurrentValues(bool clampTargetX, bool clampTargetY, bool clampTargetZ) => springVector3.SetClampCurrentValues(clampTargetX, clampTargetY, clampTargetZ);
		public void SetClampTarget(bool clampTargetX, bool clampTargetY, bool clampTargetZ) => springVector3.SetClampTarget(clampTargetX, clampTargetY, clampTargetZ);
		public void StopSpringOnClamp(bool stopX, bool stopY, bool stopZ) => springVector3.StopSpringOnClamp(stopX, stopY, stopZ);
		
		#endregion
	}
}