using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public partial class AudioSourceSpringComponent
	{
		#region PUBLIC_VOLUME_SPRING_METHODS
		
		public SpringEvents VolumeEvents => volumeSpring.springEvents;
		public float GetTargetVolume() => volumeSpring.GetTarget();
		public void SetTargetVolume(float target) => volumeSpring.SetTarget(target);
		public float GetCurrentValueVolume() => volumeSpring.GetCurrentValue();
		public void SetCurrentValueVolume(float currentValues) => volumeSpring.SetCurrentValue(currentValues);
		public float GetVelocityVolume() => volumeSpring.GetVelocity();
		public void SetVelocityVolume(float velocity) => volumeSpring.SetVelocity(velocity);
		public void AddVelocityVolume(float velocityToAdd) =>	volumeSpring.AddVelocity(velocityToAdd);
		public void ReachEquilibriumVolume() => volumeSpring.ReachEquilibrium();
		public float GetForceVolume() => volumeSpring.GetForce();
		public void SetForceVolume(float force) => volumeSpring.SetForce(force);
		public float GetDragVolume() => volumeSpring.GetDrag();
		public void SetDragVolume(float drag) => volumeSpring.SetDrag(drag);
		public void SetMinValuesVolume(float minValue) => volumeSpring.SetMinValues(minValue);
		public void SetMaxValuesVolume(float maxValue) => volumeSpring.SetMaxValues(maxValue);
		public void SetClampCurrentValuesVolume(bool clamp) => volumeSpring.SetClampCurrentValues(clamp);
		public void SetClampTargetVolume(bool clamp) => volumeSpring.SetClampTarget(clamp);
		public void StopSpringOnClampVolume(bool stop) => volumeSpring.StopSpringOnClamp(stop);
		public float GetUnifiedForceVolume() => volumeSpring.GetUnifiedForce();
		public float GetUnifiedDragVolume() => volumeSpring.GetUnifiedDrag();
		public void SetUnifiedForceVolume(float unifiedForce)
		{
			volumeSpring.SetUnifiedForceAndDragEnabled(true);
			volumeSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDragVolume(float unifiedDrag)
		{
			volumeSpring.SetUnifiedForceAndDragEnabled(true);
			volumeSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDragVolume(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForceVolume(unifiedForce);
			SetUnifiedDragVolume(unifiedDrag);
		}
		
		#endregion
		
		#region PUBLIC_PITCH_SPRING_METHODS
		
		public SpringEvents PitchEvents => pitchSpring.springEvents;
		public float GetTargetPitch() => pitchSpring.GetTarget();
		public void SetTargetPitch(float target) => pitchSpring.SetTarget(target);
		public float GetCurrentValuePitch() => pitchSpring.GetCurrentValue();
		public void SetCurrentValuePitch(float currentValues) => pitchSpring.SetCurrentValue(currentValues);
		public float GetVelocityPitch() => pitchSpring.GetVelocity();
		public void SetVelocityPitch(float velocity) => pitchSpring.SetVelocity(velocity);
		public void AddVelocityPitch(float velocityToAdd) =>	pitchSpring.AddVelocity(velocityToAdd);
		public void ReachEquilibriumPitch() => pitchSpring.ReachEquilibrium();
		public float GetForcePitch() => pitchSpring.GetForce();
		public void SetForcePitch(float force) => pitchSpring.SetForce(force);
		public float GetDragPitch() => pitchSpring.GetDrag();
		public void SetDragPitch(float drag) => pitchSpring.SetDrag(drag);
		public void SetMinValuesPitch(float minValue) => pitchSpring.SetMinValues(minValue);
		public void SetMaxValuesPitch(float maxValue) => pitchSpring.SetMaxValues(maxValue);
		public void SetClampCurrentValuesPitch(bool clamp) => pitchSpring.SetClampCurrentValues(clamp);
		public void SetClampTargetPitch(bool clamp) => pitchSpring.SetClampTarget(clamp);
		public void StopSpringOnClampPitch(bool stop) => pitchSpring.StopSpringOnClamp(stop);
		public float GetUnifiedForcePitch() => pitchSpring.GetUnifiedForce();
		public float GetUnifiedDragPitch() => pitchSpring.GetUnifiedDrag();
		public void SetUnifiedForcePitch(float unifiedForce)
		{
			pitchSpring.SetUnifiedForceAndDragEnabled(true);
			pitchSpring.SetUnifiedForce(unifiedForce);
		}
		public void SetUnifiedDragPitch(float unifiedDrag)
		{
			pitchSpring.SetUnifiedForceAndDragEnabled(true);
			pitchSpring.SetUnifiedDrag(unifiedDrag);
		}
		public void SetUnifiedForceAndDragPitch(float unifiedForce, float unifiedDrag)
		{
			SetUnifiedForcePitch(unifiedForce);
			SetUnifiedDragPitch(unifiedDrag);
		}
		
		#endregion
	}
}