using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class Vector2SpringComponent
	{
		#region PUBLIC_VECTOR2_SPRING_METHODS
		
		public SpringEvents Events => springVector2.springEvents;
		public Vector2 GetTarget() => springVector2.GetTarget();
		public void SetTarget(Vector2 target) => springVector2.SetTarget(target);
		public void SetTarget(float target) => SetTarget(Vector2.one * target);
		public Vector2 GetCurrentValue() => springVector2.GetCurrentValue();
		public void SetCurrentValue(Vector2 currentValues) => springVector2.SetCurrentValue(currentValues);
		public void SetCurrentValue(float currentValues) => SetCurrentValue(Vector2.one * currentValues);
		public Vector2 GetVelocity() => springVector2.GetVelocity();
		public void SetVelocity(Vector2 velocity) => springVector2.SetVelocity(velocity);
		public void SetVelocity(float velocity) => SetVelocity(Vector2.one * velocity);
		public void AddVelocity(Vector2 velocityToAdd) =>	springVector2.AddVelocity(velocityToAdd);
		public Vector2 GetForce() => springVector2.GetForce();
		public void SetForce(Vector2 force) => springVector2.SetForce(force);
		public void SetForce(float force) => SetForce(Vector2.one * force);
		public Vector2 GetDrag() => springVector2.GetDrag();
		public void SetDrag(Vector2 drag) => springVector2.SetDrag(drag);
		public void SetDrag(float drag) => SetDrag(Vector2.one * drag);
		public float GetUnifiedForce() => springVector2.GetUnifiedForce();
		public float GetUnifiedDrag() => springVector2.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			springVector2.SetUnifiedForceAndDragEnabled(true);
			springVector2.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			springVector2.SetUnifiedForceAndDragEnabled(true);
			springVector2.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		public void SetMinValues(Vector2 minValue) => springVector2.SetMinValues(minValue);
		public void SetMinValues(float minValue) => SetMinValues(Vector2.one * minValue);
		public void SetMaxValues(Vector2 maxValue) => springVector2.SetMaxValues(maxValue);
		public void SetMaxValues(float maxValue) => SetMaxValues(Vector2.one * maxValue);
		public void SetClampCurrentValues(bool clampTargetX, bool clampTargetY) => springVector2.SetClampCurrentValues(clampTargetX, clampTargetY);
		public void SetClampTarget(bool clampTargetX, bool clampTargetY) => springVector2.SetClampTarget(clampTargetX, clampTargetY);
		public void StopSpringOnClamp(bool stopX, bool stopY) => springVector2.StopSpringOnClamp(stopX, stopY);
		
		#endregion
	}
}