using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class UiSliderSpringComponent
	{
		#region PUBLIC_FILLAMOUNT_SPRING_METHODS
		
		public SpringEvents Events => fillAmountSpring.springEvents;
		public float GetTarget() => fillAmountSpring.GetTarget();
		public void SetTarget(float target) => fillAmountSpring.SetTarget(target);
		public float GetCurrentValue() => fillAmountSpring.GetCurrentValue();
		public void SetCurrentValue(float currentValues)
		{
			fillAmountSpring.SetCurrentValue(currentValues);
			SetCurrentValueInternal(currentValues);
		}
		public float GetVelocity() => fillAmountSpring.GetVelocity();
		public void SetVelocity(float velocity) => fillAmountSpring.SetVelocity(velocity);
		public void AddVelocity(float velocityToAdd) =>	fillAmountSpring.AddVelocity(velocityToAdd);
		public override void ReachEquilibrium()
		{
			base.ReachEquilibrium();
			ReachEquilibriumInternal();
		}
		public float GetForce() => fillAmountSpring.GetForce();
		public void SetForce(float force) => fillAmountSpring.SetForce(force);
		public float GetDrag() => fillAmountSpring.GetDrag();
		public void SetDrag(float drag) => fillAmountSpring.SetDrag(drag);
		public void SetMinValues(float minValue) => fillAmountSpring.SetMinValues(minValue);
		public void SetMaxValues(float maxValue) => fillAmountSpring.SetMaxValues(maxValue);
		public void SetClampCurrentValues(bool clamp) => fillAmountSpring.SetClampCurrentValues(clamp);
		public void SetClampTarget(bool clamp) => fillAmountSpring.SetClampTarget(clamp);
		public void StopSpringOnClamp(bool stop) => fillAmountSpring.StopSpringOnClamp(stop);
		public float GetUnifiedForce() => fillAmountSpring.GetUnifiedForce();
		public float GetUnifiedDrag() => fillAmountSpring.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			fillAmountSpring.SetUnifiedForceAndDragEnabled(true);
			fillAmountSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			fillAmountSpring.SetUnifiedForceAndDragEnabled(true);
			fillAmountSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		
		#endregion
	}
}