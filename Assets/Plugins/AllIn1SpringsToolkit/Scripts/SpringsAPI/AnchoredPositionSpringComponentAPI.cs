using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class AnchoredPositionSpringComponent
	{
		#region PUBLIC_ANCHOREDPOSITION_SPRING_METHODS
		
		public SpringEvents Events => anchoredPositionSpring.springEvents;
		public Vector2 GetTarget() => anchoredPositionSpring.GetTarget();
		public void SetTarget(Vector2 target)
		{
			anchoredPositionSpring.SetTarget(target);
			SetTargetInternal(target);
		}
		public void SetTarget(float target) => SetTarget(Vector2.one * target);
		public Vector2 GetCurrentValue() => anchoredPositionSpring.GetCurrentValue();
		public void SetCurrentValue(Vector2 currentValues)
		{
			anchoredPositionSpring.SetCurrentValue(currentValues);
			SetCurrentValueInternal(currentValues);
		}
		public void SetCurrentValue(float currentValues) => SetCurrentValue(Vector2.one * currentValues);
		public Vector2 GetVelocity() => anchoredPositionSpring.GetVelocity();
		public void SetVelocity(Vector2 velocity) => anchoredPositionSpring.SetVelocity(velocity);
		public void SetVelocity(float velocity) => SetVelocity(Vector2.one * velocity);
		public void AddVelocity(Vector2 velocityToAdd) =>	anchoredPositionSpring.AddVelocity(velocityToAdd);
		public override void ReachEquilibrium()
		{
			base.ReachEquilibrium();
			ReachEquilibriumInternal();
		}
		public Vector2 GetForce() => anchoredPositionSpring.GetForce();
		public void SetForce(Vector2 force) => anchoredPositionSpring.SetForce(force);
		public void SetForce(float force) => SetForce(Vector2.one * force);
		public Vector2 GetDrag() => anchoredPositionSpring.GetDrag();
		public void SetDrag(Vector2 drag) => anchoredPositionSpring.SetDrag(drag);
		public void SetDrag(float drag) => SetDrag(Vector2.one * drag);
		public float GetUnifiedForce() => anchoredPositionSpring.GetUnifiedForce();
		public float GetUnifiedDrag() => anchoredPositionSpring.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			anchoredPositionSpring.SetUnifiedForceAndDragEnabled(true);
			anchoredPositionSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			anchoredPositionSpring.SetUnifiedForceAndDragEnabled(true);
			anchoredPositionSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		public void SetMinValues(Vector2 minValue) => anchoredPositionSpring.SetMinValues(minValue);
		public void SetMinValues(float minValue) => SetMinValues(Vector2.one * minValue);
		public void SetMaxValues(Vector2 maxValue) => anchoredPositionSpring.SetMaxValues(maxValue);
		public void SetMaxValues(float maxValue) => SetMaxValues(Vector2.one * maxValue);
		public void SetClampCurrentValues(bool clampTargetX, bool clampTargetY) => anchoredPositionSpring.SetClampCurrentValues(clampTargetX, clampTargetY);
		public void SetClampTarget(bool clampTargetX, bool clampTargetY) => anchoredPositionSpring.SetClampTarget(clampTargetX, clampTargetY);
		public void StopSpringOnClamp(bool stopX, bool stopY) => anchoredPositionSpring.StopSpringOnClamp(stopX, stopY);
		
		#endregion
	}
}