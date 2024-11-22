using UnityEngine;
namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringValues
	{
		[SerializeField] private float initialValue;

		[SerializeField] private bool clampTarget;
		[SerializeField] private bool clampCurrentValue;
		[SerializeField] private bool stopSpringOnCurrentValueClamp;
		
		[SerializeField] private float minValue = 0f;
		[SerializeField] private float maxValue = 100f;

		[SerializeField] private bool update;
		[SerializeField] private float force;
		[SerializeField] private float drag;

		[SerializeField] private float target;

		[SerializeField] private float currentValue;
		[SerializeField] private float candidateValue;
		private float unclampedCurrentValue;

		[SerializeField] private float velocity;

		[SerializeField] private bool clamped;

		[SerializeField]
		[HideInInspector]
		private float operationValue;

		public SpringValues()
		{
			this.update = true;
			this.force = 150f;
			this.drag = 10f;
		}

		public void ReachEquilibrium()
		{
			currentValue = target;
			Stop();
		}

		public void SetTarget(float target)
		{
			this.target = target;
			this.unclampedCurrentValue = target;
		}

		public float GetTarget()
		{
			return target;
		}

		public void Initialize()
		{
			this.currentValue = initialValue;
			Stop();
		}

		public void AddVelocity(float velocity)
		{
			if (update)
			{
				this.velocity += velocity;
			}
		}

		public bool IsOvershot()
		{
			bool res = candidateValue < minValue || candidateValue > maxValue;
			return res;
		}

		public void Clamp()
		{
			clamped = (candidateValue < minValue || candidateValue > maxValue);

			candidateValue = Mathf.Clamp(candidateValue, minValue, maxValue);
		}

		public void ClampTarget()
		{
			clamped = (target < minValue || target > maxValue);

			target = Mathf.Clamp(target, minValue, maxValue);
		}

		public bool IsClamped()
		{
			return clamped;
		}

		public bool IsTargetReached()
		{
			bool res = currentValue >= target;
			return res;
		}

		public void Stop()
		{
			velocity = 0f;
		}

		public void ApplyCandidateValue()
		{
			if (update)
			{
				SetCurrentValue(candidateValue);

				candidateValue = 0f;
				clamped = false;
			}
		}

		public float GetCurrentValue()
		{
			return currentValue;
		}

		public float GetVelocity()
		{
			return velocity;
		}

		public void SetVelocity(float velocity)
		{
			this.velocity = velocity;
		}

		public void SetCandidateValue(float candidateValue)
		{
			this.candidateValue = candidateValue;
		}

		public float GetCandidateValue()
		{
			return candidateValue;
		}

		public void SetCurrentValue(float newCurrentValue)
		{
			candidateValue = newCurrentValue;
			currentValue = newCurrentValue;
		}

		public bool GetClampTarget()
		{
			return clampTarget;
		}

		public void SetClampTarget(bool clampTarget)
		{
			this.clampTarget = clampTarget;
		}

		public bool GetClampCurrentValue()
		{
			return clampCurrentValue;
		}

		public void SetClampCurrentValue(bool clampCurrentValue)
		{
			this.clampCurrentValue = clampCurrentValue;
		}

		public bool GetStopSpringOnCurrentValueClamp()
		{
			return stopSpringOnCurrentValueClamp;
		}

		public void SetStopSpringOnCurrentValueClamp(bool stopSpringOnCurrentValueClamp)
		{
			this.stopSpringOnCurrentValueClamp = stopSpringOnCurrentValueClamp;
		}

		public void SetMinValue(float minValue)
		{
			this.minValue = minValue;
		}

		public void SetMaxValue(float maxValue)
		{
			this.maxValue = maxValue;
		}

		public bool IsEnabled()
		{
			return update;
		}

		public float GetForce()
		{
			return force;
		}

		public void SetForce(float force)
		{
			this.force = force;
		}

		public float GetDrag()
		{
			return drag;
		}

		public void SetDrag(float drag)
		{
			this.drag = drag;
		}

		public bool GetUpdate()
		{
			return update;
		}

		public float GetUnclampedValue()
		{
			return unclampedCurrentValue;
		}

		public float GetMinValue()
		{
			return minValue;
		}

		public float GetMaxValue()
		{
			return maxValue;
		}
	}
}