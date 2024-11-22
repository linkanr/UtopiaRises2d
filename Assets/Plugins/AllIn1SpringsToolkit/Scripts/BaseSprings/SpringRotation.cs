using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringRotation : Spring
	{
		public enum AxisRestriction
		{
			None = 0,
			OnlyXAxis = 1,
			OnlyYAxis = 2,
			OnlyZAxis = 3
		}

		public const int SPRING_SIZE = 9;

		private const int FORWARD_X = 0;
		private const int FORWARD_Y = 1;
		private const int FORWARD_Z = 2;

		private const int UP_X = 6;
		private const int UP_Y = 7;
		private const int UP_Z = 8;

		private const int LOCAL_AXIS_X = 3;
		private const int LOCAL_AXIS_Y = 4;
		private const int LOCAL_AXIS_Z = 5;

		[SerializeField] private AxisRestriction axisRestriction;

		public SpringRotation() : base(SPRING_SIZE)
		{

		}

		public override int GetSpringSize()
		{
			return SPRING_SIZE;
		}

		public override bool HasValidSize()
		{
			return (springValues.Length == SPRING_SIZE);
		}

		#region CURRENT VALUES
		private Vector3 CurrentLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetCurrentValue(),
					springValues[LOCAL_AXIS_Y].GetCurrentValue(),
					springValues[LOCAL_AXIS_Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetCurrentValue(value.x);
				springValues[LOCAL_AXIS_Y].SetCurrentValue(value.y);
				springValues[LOCAL_AXIS_Z].SetCurrentValue(value.z);
			}
		}

		private Vector3 CurrentForward
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[FORWARD_X].GetCurrentValue(),
					springValues[FORWARD_Y].GetCurrentValue(),
					springValues[FORWARD_Z].GetCurrentValue()).normalized;

				return res;
			}
			set
			{
				springValues[FORWARD_X].SetCurrentValue(value.x);
				springValues[FORWARD_Y].SetCurrentValue(value.y);
				springValues[FORWARD_Z].SetCurrentValue(value.z);
			}
		}

		private Vector3 CurrentUp
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[UP_X].GetCurrentValue(),
					springValues[UP_Y].GetCurrentValue(),
					springValues[UP_Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValues[UP_X].SetCurrentValue(value.x);
				springValues[UP_Y].SetCurrentValue(value.y);
				springValues[UP_Z].SetCurrentValue(value.z);
			}
		}

		public Quaternion GetCurrentGlobalRotation()
		{
			Quaternion res = Quaternion.LookRotation(CurrentForward, CurrentUp);
			return res;
		}

		public Quaternion GetCurrentValue()
		{
			Quaternion globalQuat = GetCurrentGlobalRotation();

			Vector3 forward = globalQuat * Vector3.forward;
			Vector3 up = globalQuat * Vector3.up;
			Vector3 right = globalQuat * Vector3.right;

			Quaternion res =
				Quaternion.AngleAxis(CurrentLocalAxis.x, right) *
				Quaternion.AngleAxis(CurrentLocalAxis.y, up) *
				Quaternion.AngleAxis(CurrentLocalAxis.z, forward) *
				globalQuat;

			return res;
		}

		public void SetCurrentValue(Quaternion newCurrentQuaternion)
		{
			CurrentForward = (newCurrentQuaternion * Vector3.forward).normalized;
			CurrentUp = (newCurrentQuaternion * Vector3.up).normalized;
		}

		public void SetCurrentValue(Vector3 currentEuler)
		{
			if (axisRestriction == AxisRestriction.OnlyXAxis)
			{
				currentEuler.x = CurrentLocalAxis.x;
			}
			if (axisRestriction == AxisRestriction.OnlyYAxis)
			{
				currentEuler.y = CurrentLocalAxis.y;
			}
			if (axisRestriction == AxisRestriction.OnlyZAxis)
			{
				currentEuler.z = CurrentLocalAxis.z;
			}

			CurrentLocalAxis = currentEuler;
		}
		#endregion

		#region TARGET
		private Vector3 TargetLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetTarget(),
					springValues[LOCAL_AXIS_Y].GetTarget(),
					springValues[LOCAL_AXIS_Z].GetTarget());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetTarget(value.x);
				springValues[LOCAL_AXIS_Y].SetTarget(value.y);
				springValues[LOCAL_AXIS_Z].SetTarget(value.z);
			}
		}

		private Vector3 TargetForward
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[FORWARD_X].GetTarget(),
					springValues[FORWARD_Y].GetTarget(),
					springValues[FORWARD_Z].GetTarget());

				return res;
			}
			set
			{
				springValues[FORWARD_X].SetTarget(value.x);
				springValues[FORWARD_Y].SetTarget(value.y);
				springValues[FORWARD_Z].SetTarget(value.z);
			}
		}

		private Vector3 TargetUp
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[UP_X].GetTarget(),
					springValues[UP_Y].GetTarget(),
					springValues[UP_Z].GetTarget());

				return res;
			}
			set
			{
				springValues[UP_X].SetTarget(value.x);
				springValues[UP_Y].SetTarget(value.y);
				springValues[UP_Z].SetTarget(value.z);
			}
		}

		private Vector3 TargetRight
		{
			get
			{
				Vector3 res = Vector3.Cross(TargetUp, TargetForward).normalized;
				return res;
			}
		}

		public void SetTarget(Quaternion target)
		{
			Vector3 rawForward = (target * Vector3.forward).normalized;
			Vector3 rawUp = (target * Vector3.up).normalized;

			Vector3 correctedForward = rawForward;
			Vector3 correctedUp = rawUp;
			LimitTargetRotation(rawForward, rawUp, ref correctedForward, ref correctedUp);

			TargetForward = correctedForward;
			TargetUp = correctedUp;
		}

		private void LimitTargetRotation(Vector3 rawForward, Vector3 rawUp, ref Vector3 correctedForward, ref Vector3 correctedUp)
		{
			bool isXLimited = axisRestriction == AxisRestriction.OnlyXAxis;
			bool isYLimited = axisRestriction == AxisRestriction.OnlyYAxis;
			bool isZLimited = axisRestriction == AxisRestriction.OnlyZAxis;

			int numIterations = 5;
			for (int i = 0; i < numIterations; i++)
			{
				Vector3 correctedRight = Vector3.Cross(correctedUp, correctedForward).normalized;

				if (isXLimited)
				{
					Quaternion cancelQuat = GetCancelQuat(correctedRight, Vector3.right, 0f);
					correctedForward = cancelQuat * correctedForward;
					correctedUp = cancelQuat * correctedUp;
				}
				if (isYLimited)
				{
					Quaternion cancelQuat = GetCancelQuat(correctedUp, Vector3.up, 0f);
					correctedForward = cancelQuat * correctedForward;
					correctedUp = cancelQuat * correctedUp;
				}
				if (isZLimited)
				{
					Quaternion cancelQuat = GetCancelQuat(correctedForward, Vector3.forward, 0f);
					correctedForward = cancelQuat * correctedForward;
					correctedUp = cancelQuat * correctedUp;
				}
			}
		}

		private Quaternion GetCancelQuat(Vector3 vec, Vector3 reference, float maxAngle)
		{
			Quaternion res = Quaternion.identity;

			float angle = Vector3.Angle(vec, reference);
			if (angle >= maxAngle)
			{
				float diffAngle = angle - maxAngle;
				Vector3 rotationAxis = Vector3.Cross(vec, reference).normalized;
				Vector3 rotatedVec = Quaternion.AngleAxis(diffAngle, rotationAxis) * vec;
				res = Quaternion.FromToRotation(vec, rotatedVec);
			}

			return res;
		}

		public struct DotAndAxis
		{
			public float angle;
			public Vector3 axis;

			public DotAndAxis(Vector3 vec, Vector3 axis)
			{
				this.angle = Vector3.Angle(axis, vec);
				this.axis = axis;
			}
		}

		public void SetTarget(Vector3 targetValues)
		{
			if (axisRestriction == AxisRestriction.OnlyXAxis)
			{
				targetValues.x = TargetLocalAxis.x;
			}
			if (axisRestriction == AxisRestriction.OnlyYAxis)
			{
				targetValues.y = TargetLocalAxis.y;
			}
			if (axisRestriction == AxisRestriction.OnlyZAxis)
			{
				targetValues.z = TargetLocalAxis.z;
			}

			TargetLocalAxis = targetValues;
		}

		public Quaternion GetTargetGlobalRotation()
		{
			Quaternion res = Quaternion.LookRotation(TargetForward, TargetUp);
			return res;
		}

		public Quaternion GetTarget()
		{
			Quaternion globalQuat = GetTargetGlobalRotation();
		
			Vector3 forward = globalQuat * Vector3.forward;
			Vector3 up = globalQuat * Vector3.up;
			Vector3 right = globalQuat * Vector3.right;

			Quaternion res =
				Quaternion.AngleAxis(TargetLocalAxis.x, right) *
				Quaternion.AngleAxis(TargetLocalAxis.y, up) *
				Quaternion.AngleAxis(TargetLocalAxis.z, forward) *
				globalQuat;

			return res;
		}
		#endregion

		#region VELOCITY
		private Vector3 VelocityLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetVelocity(),
					springValues[LOCAL_AXIS_Y].GetVelocity(),
					springValues[LOCAL_AXIS_Z].GetVelocity());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetVelocity(value.x);
				springValues[LOCAL_AXIS_Y].SetVelocity(value.y);
				springValues[LOCAL_AXIS_Z].SetVelocity(value.z);
			}
		}

		private Vector3 VelocityForward
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[FORWARD_X].GetVelocity(),
					springValues[FORWARD_Y].GetVelocity(),
					springValues[FORWARD_Z].GetVelocity());

				return res;
			}
			set
			{
				springValues[FORWARD_X].SetVelocity(value.x);
				springValues[FORWARD_Y].SetVelocity(value.y);
				springValues[FORWARD_Z].SetVelocity(value.z);
			}
		}

		private Vector3 VelocityUp
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[UP_X].GetVelocity(),
					springValues[UP_Y].GetVelocity(),
					springValues[UP_Z].GetVelocity());

				return res;
			}
			set
			{
				springValues[UP_X].SetVelocity(value.x);
				springValues[UP_Y].SetVelocity(value.y);
				springValues[UP_Z].SetVelocity(value.z);
			}
		}

		public void AddVelocity(Vector3 eulerTarget)
		{
			const float velocityFactor = 150;
			VelocityLocalAxis += eulerTarget * velocityFactor;
		}

		public void SetVelocity(Vector3 eulerTarget)
		{
			VelocityLocalAxis = eulerTarget;
		}

		public Vector3 GetVelocity()
		{
			Vector3 res = VelocityLocalAxis;
			return res;
		}
		#endregion

		#region CLAMPING
		


		#endregion

		#region CANDIDATE VALUES
		private Vector3 CandidateLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetCandidateValue(),
					springValues[LOCAL_AXIS_Y].GetCandidateValue(),
					springValues[LOCAL_AXIS_Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetCandidateValue(value.x);
				springValues[LOCAL_AXIS_Y].SetCandidateValue(value.y);
				springValues[LOCAL_AXIS_Z].SetCandidateValue(value.z);
			}
		}

		private Vector3 CandidateForward
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[FORWARD_X].GetCandidateValue(),
					springValues[FORWARD_Y].GetCandidateValue(),
					springValues[FORWARD_Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValues[FORWARD_X].SetCandidateValue(value.x);
				springValues[FORWARD_Y].SetCandidateValue(value.y);
				springValues[FORWARD_Z].SetCandidateValue(value.z);
			}
		}

		private Vector3 CandidateUp
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[UP_X].GetCandidateValue(),
					springValues[UP_Y].GetCandidateValue(),
					springValues[UP_Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValues[UP_X].SetCandidateValue(value.x);
				springValues[UP_Y].SetCandidateValue(value.y);
				springValues[UP_Z].SetCandidateValue(value.z);
			}
		}

		private Quaternion CandidateGlobalRotation
		{
			get
			{
				Quaternion res = Quaternion.LookRotation(CandidateForward, CandidateUp);
				return res;
			}
		}

		public override void ProcessCandidateValue()
		{
			Vector3 deltaLocalAxis = CandidateLocalAxis - CurrentLocalAxis;
			const float maxRotationPerFrame = 80f;
			deltaLocalAxis = Vector3.ClampMagnitude(deltaLocalAxis, maxRotationPerFrame);
			CandidateLocalAxis = deltaLocalAxis + CurrentLocalAxis;

			Quaternion quatCandidate = CandidateGlobalRotation;
			Quaternion quatCurrent = GetCurrentGlobalRotation();


			float angle = Quaternion.Angle(quatCurrent, quatCandidate);
			if(angle > 15f)
			{
				quatCandidate = Quaternion.RotateTowards(quatCurrent, quatCandidate, 80f);
				CandidateForward = quatCandidate * Vector3.forward;
				CandidateUp = quatCandidate * Vector3.up;
			}

			base.ProcessCandidateValue();
		}
		#endregion

#if UNITY_EDITOR

		internal override void DrawGizmosSelected(Vector3 componentPosition)
		{
			base.DrawGizmosSelected(componentPosition);

			if (axisRestriction != AxisRestriction.None)
			{
				const float lineLength = 1.5f;
				const float sphereRadius = 0.1f;

				Vector3 vec = Vector3.up;
				switch (axisRestriction)
				{
					case AxisRestriction.OnlyXAxis:
						vec = Vector3.right;
						break;
					case AxisRestriction.OnlyYAxis:
						vec = Vector3.up;
						break;
					case AxisRestriction.OnlyZAxis:
						vec = Vector3.forward;
						break;
				}

				Color vecColor = Color.cyan;
				Color sphereColor = vecColor;
				sphereColor.a = 0.5f;

				Gizmos.color = vecColor;
				Vector3 position01 = componentPosition - vec * lineLength;
				Vector3 position02 = componentPosition + vec * lineLength;
				Gizmos.DrawLine(position01, position02);

				Gizmos.color = sphereColor;
				Gizmos.DrawSphere(position01, sphereRadius);
				Gizmos.DrawSphere(position02, sphereRadius);
			}
		}
#endif
	}
}