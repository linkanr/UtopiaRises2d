using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public abstract class Spring
	{
#if UNITY_EDITOR && ALLIN1SPRINGS_DEBUGGER
		private static int LAST_SPRING_ID = int.MinValue;

		private int id;
#endif

		[Tooltip("When enabled, uses the same Force and Drag values for all axes. When disabled, allows setting different Force and Drag values for each axis")]
		public bool unifiedForceAndDrag;
		[Tooltip("Higher values make the spring react more quickly to changes")]
		public float unifiedForce;
		[Tooltip("Higher values make the spring more stable with less bouncing")]
		public float unifiedDrag;

		public bool useInitialValues;
		public bool useCustomTarget;

		public bool springEnabled;
		public bool clampingEnabled;
		public SpringValues[] springValues;

		[HideInInspector] public bool showDebugFields;
		[HideInInspector] public bool unfolded;

		public bool eventsEnabled;

		public SpringEvents springEvents;

		public Spring(int size)
		{
			springValues = new SpringValues[size];

			this.unifiedForceAndDrag = true;
			this.unifiedForce = 150f;
			this.unifiedDrag = 10f;

			this.springEnabled = true;

			for (int i = 0; i < size; i++)
			{
				springValues[i] = new SpringValues();
			}
		}

		public bool IsClampTargetEnabled()
		{
			bool res = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				res = res || springValues[i].GetClampTarget();
			}

			return res;
		}

		public bool IsClampCurrentValueEnabled()
		{
			bool res = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				res = res || springValues[i].GetClampCurrentValue();
			}

			return res;
		}

		public void ReachEquilibrium()
		{
			for (int i = 0; i < springValues.Length; i++)
			{
				springValues[i].ReachEquilibrium();
			}
		}

		public abstract bool HasValidSize();

		public abstract int GetSpringSize();

		public bool CheckCorrectSize()
		{
			bool res = HasValidSize();
			if (!res)
			{
				int springSize = GetSpringSize();
				springValues = new SpringValues[springSize];
				for (int i = 0; i < springSize; i++)
				{
					springValues[i] = new SpringValues();
				}
			}

			return res;
		}

		public virtual void Initialize()
		{
			for(int i = 0; i < springValues.Length; i++)
			{
				springValues[i].Initialize();
			}
		}

		public virtual void ProcessCandidateValue()
		{
			for(int i = 0; i < springValues.Length; i++)
			{
				springValues[i].ApplyCandidateValue();
			}
		}

		public void CheckEvents()
		{
			springEvents.CheckEvents();
		}

		public bool IsClamped()
		{
			bool res = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				res = res || springValues[i].IsClamped();
			}

			return res;
		}

		public bool IsCloseToStopping()
		{
			bool res = true;

			for(int i = 0; i < springValues.Length; i++)
			{
				if (!springValues[i].IsEnabled())
				{
					continue;
				}

				float absVelocity = Mathf.Abs(springValues[i].GetVelocity());
				res = res && (absVelocity <= SpringsToolkitConstants.SPRING_VELOCITY_THRESHOLD);
			}

			return res;
		}

		public bool IsOnTarget()
		{
			bool res = true;

			for (int i = 0; i < springValues.Length; i++)
			{
				if (!springValues[i].IsEnabled())
				{
					continue;
				}

				float distanceToTarget = Mathf.Abs(springValues[i].GetTarget() - springValues[i].GetCurrentValue());
				res = res && (distanceToTarget <= SpringsToolkitConstants.SPRING_TARGET_DISTANCE_THRESHOLD);
			}

			return res;
		}

		public bool HasCurrentValueChanged()
		{
			bool res = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				if (!springValues[i].IsEnabled())
				{
					continue;
				}

				float delta = Mathf.Abs(springValues[i].GetCandidateValue() - springValues[i].GetCurrentValue());
				res = res || (delta >= SpringsToolkitConstants.SPRING_CHANGE_THRESHOLD);
			}

			return res;
		} 

#if UNITY_EDITOR && ALLIN1SPRINGS_DEBUGGER
		public void AssignID()
		{
			this.id = LAST_SPRING_ID;
			LAST_SPRING_ID++;
		}

		public int GetID()
		{
			return id;
		}
#endif

		#region FORCE AND DRAG
		public void SetUnifiedForceAndDragEnabled(bool value)
		{
			this.unifiedForceAndDrag = value;
		}

		public float GetUnifiedForce()
		{
			return unifiedForce;
		}

		public void SetUnifiedForce(float unifiedForce)
		{
			this.unifiedForce = unifiedForce;
		}

		public float GetUnifiedDrag()
		{
			return unifiedDrag;
		}

		public void SetUnifiedDrag(float unifiedDrag)
		{
			this.unifiedDrag = unifiedDrag;
		}

		public float GetForceByIndex(int index)
		{
			float res = springValues[index].GetForce();
			return res;
		}

		public void SetForceByIndex(int index, float force)
		{
			springValues[index].SetForce(force);
		}

		public float GetDragByIndex(int index)
		{
			float res = springValues[index].GetDrag();
			return res;
		}

		public void SetDragByIndex(int index, float drag)
		{
			springValues[index].SetDrag(drag);
		}
		#endregion

		#region CLAMPING

		public void SetClampingEnabled(bool clampingEnabled)
		{
			this.clampingEnabled = clampingEnabled;
		}

		protected void SetMinValueByIndex(int index, float minValue)
		{
			springValues[index].SetMinValue(minValue);
		}

		protected void SetMaxValueByIndex(int index, float maxValue)
		{
			springValues[index].SetMaxValue(maxValue);
		}

		protected void SetStopSpringOnCurrentValueClampByIndex(int index, bool stop)
		{
			springValues[index].SetStopSpringOnCurrentValueClamp(stop);
		}

		protected void SetClampTargetByIndex(int index, bool clampTarget)
		{
			springValues[index].SetClampTarget(clampTarget);
		}

		protected void SetClampCurrentValueByIndex(int index, bool clampCurrentValue)
		{
			springValues[index].SetClampCurrentValue(clampCurrentValue);
		}

		#endregion

#if UNITY_EDITOR
		internal virtual void DrawGizmosSelected(Vector3 componentPosition)
		{
			
		}
#endif
	}
}