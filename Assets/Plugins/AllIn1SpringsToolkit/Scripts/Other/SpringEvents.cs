using System;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringEvents
	{
		private Spring spring;

		private bool targetReachedEventEnabled;
		private bool clampingEventEnabled;

		public event Action OnCurrentValueChanged;
		public event Action OnTargetReached;
		public event Action OnClampingApplied;

		public void SetSpring(Spring spring)
		{
			this.spring = spring;

			this.targetReachedEventEnabled = false;
			this.clampingEventEnabled = false;
		}

		public void CheckEvents()
		{
			if (!spring.eventsEnabled) { return; }

			bool isOnTarget = spring.IsOnTarget();
			bool isClamped = spring.IsClamped();

			CheckTargetReached(isOnTarget);
			CheckCurrentValueChanged();
			CheckClampingApplied(isClamped);

			if(!clampingEventEnabled && !isClamped)
			{
				clampingEventEnabled = true;
			}

			if (!isOnTarget && !targetReachedEventEnabled)
			{
				targetReachedEventEnabled = true;
			}
		}

		private void CheckTargetReached(bool isOnTarget)
		{
			if (spring.IsCloseToStopping() && isOnTarget && targetReachedEventEnabled)
			{
				OnTargetReached?.Invoke();
				targetReachedEventEnabled = false;
			}
		}

		private void CheckCurrentValueChanged()
		{
			if (spring.HasCurrentValueChanged())
			{
				OnCurrentValueChanged?.Invoke();
			}
		}

		private void CheckClampingApplied(bool isClamped)
		{
			if (clampingEventEnabled && isClamped)
			{
				OnClampingApplied?.Invoke();
				clampingEventEnabled = false;
			}
		}
	}
}