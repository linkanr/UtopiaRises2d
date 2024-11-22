using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class RotationSpringComponent
	{
		#region PUBLIC_ROTATION_SPRING_METHODS
		
		public SpringEvents Events => springRotation.springEvents;
		public Quaternion GetTarget() => springRotation.GetTarget();
		public void SetTarget(Quaternion targetQuaternion)
		{
			springRotation.SetTarget(targetQuaternion);
		}
		public void SetTarget(Vector3 targetEuler)
		{
			springRotation.SetTarget(targetEuler);
		}
		public Quaternion GetCurrentValue() => springRotation.GetCurrentValue();
		public void SetCurrentValue(Quaternion currentQuaternion) => springRotation.SetCurrentValue(currentQuaternion);
		public void SetCurrentValue(Vector3 currentEuler) => springRotation.SetCurrentValue(currentEuler);
		public Vector3 GetVelocity() => springRotation.GetVelocity();
		public void SetVelocity(Vector3 velocity) => springRotation.SetVelocity(velocity);
		public void AddVelocity(Vector3 velocityToAdd) => springRotation.AddVelocity(velocityToAdd);
		public float GetUnifiedForce() => springRotation.GetUnifiedForce();
		public float GetUnifiedDrag() => springRotation.GetUnifiedDrag();
		public void SetUnifiedForce(float unifiedForce)
		{
			springRotation.SetUnifiedForceAndDragEnabled(true);
			springRotation.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDrag(float unifiedDrag)
		{
			springRotation.SetUnifiedForceAndDragEnabled(true);
			springRotation.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDrag(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForce(unifiedForce);
			SetUnifiedDrag(unifiedDrag);
		}
		
		#endregion
	}
}