namespace AllIn1SpringsToolkit
{
	public static class SpringLogic
	{
		public static void UpdateSpring(float deltaTime, Spring spring)
		{
			for (int i = 0; i < spring.springValues.Length; i++)
			{
				if (!spring.springValues[i].IsEnabled()) { return; }

				float force = spring.unifiedForceAndDrag ? spring.unifiedForce : spring.springValues[i].GetForce();
				float drag = spring.unifiedForceAndDrag ? spring.unifiedDrag : spring.springValues[i].GetDrag();

				CheckTargetClamping(spring.springValues[i], spring.clampingEnabled);
				CalculateNewVelocityAndCurrentValue(deltaTime, force, drag, spring.springValues[i]);
				CheckCurrentValueClamping(spring.springValues[i], spring.clampingEnabled);
			}
		}

		private static void CalculateNewVelocityAndCurrentValue(float deltaTime, float force, float drag, SpringValues springValues)
		{
			float springForce = force * (springValues.GetTarget() - springValues.GetCurrentValue());

			float dampingForce = -drag * springValues.GetVelocity();
			float acceleration = springForce + dampingForce;

			springValues.AddVelocity(acceleration * deltaTime);

			float increase = springValues.GetVelocity() * deltaTime;

			float newCandidateValue = springValues.GetCurrentValue() + increase;
			springValues.SetCandidateValue(newCandidateValue);
		}

		private static void CheckCurrentValueClamping(SpringValues springValues, bool clampingEnabled)
		{
			if (clampingEnabled)
			{
				if (springValues.IsOvershot() && springValues.GetClampCurrentValue())
				{
					springValues.Clamp();

					if (springValues.GetStopSpringOnCurrentValueClamp())
					{
						springValues.Stop();
					}
				}
			}
		}

		private static void CheckTargetClamping(SpringValues springValues, bool clampingEnabled)
		{
			if (clampingEnabled && springValues.GetClampTarget())
			{
				springValues.ClampTarget();
			}
		}
	}
}