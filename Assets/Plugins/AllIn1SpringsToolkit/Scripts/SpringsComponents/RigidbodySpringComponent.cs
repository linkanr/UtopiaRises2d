using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Rigidbody Spring Component")]
	public partial class RigidbodySpringComponent : SpringComponent
	{
		private const float VELOCITY_FACTOR = 10f;

		[SerializeField] private SpringVector3 positionSpring = new SpringVector3();
		[SerializeField] private SpringRotation rotationSpring = new SpringRotation();

		public bool useTransformAsTarget;

        [SerializeField] private Rigidbody rigidBodyFollower;
		[SerializeField] private Transform target;

		private Vector3 positionTarget;
		private Quaternion rotationTarget;

		protected override void SetInitialValues()
		{
			if (!hasCustomInitialValues)
			{
				SetCurrentValueByDefault();
			}
			else
			{
				if (!positionSpring.useInitialValues)
				{
					SetCurrentValuePositionByDefault();
				}
				if (!rotationSpring.useInitialValues)
				{
					SetCurrentValueRotationByDefault();
				}
			}

			if (useTransformAsTarget)
			{
				UpdateTarget();
			}
			else
			{
				if (!hasCustomTarget)
				{
					SetTargetByDefault();
				}
				else
				{
					if (!positionSpring.useCustomTarget)
					{
						SetTargetPositionByDefault();
					}
					if (!rotationSpring.useCustomTarget)
					{
						SetTargetRotationByDefault();
					}
				}
			}
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValuePositionByDefault();
			SetCurrentValueRotationByDefault();
		}

		private void SetCurrentValuePositionByDefault()
		{
			SetCurrentValuePosition(rigidBodyFollower.position);
		}

		private void SetCurrentValueRotationByDefault()
		{
			SetCurrentValueRotation(rigidBodyFollower.rotation);
		}

		protected override void SetTargetByDefault()
		{
			SetTargetPositionByDefault();
			SetTargetRotationByDefault();
		}

		private void SetTargetPositionByDefault()
		{
			SetTargetPosition(rigidBodyFollower.position);
			positionTarget = GetTargetPosition();
		}

		private void SetTargetRotationByDefault()
		{
			SetTargetRotation(rigidBodyFollower.rotation);
			rotationTarget = GetTargetRotation();
		}

		protected override void RegisterSprings()
		{
			RegisterSpring(positionSpring);
			RegisterSpring(rotationSpring);
		}

		public void Update()
		{
			if (!initialized) { return; }
			UpdateTarget();
		}

        private void FixedUpdate()
        {
			if (!initialized) { return; }

			if (positionSpring.springEnabled)
			{
				Vector3 velocity = (positionSpring.GetCurrentValue() - rigidBodyFollower.position) * VELOCITY_FACTOR;
				rigidBodyFollower.velocity = velocity;
			}

			if (rotationSpring.springEnabled)
			{
				Quaternion targetRotation = rotationSpring.GetCurrentValue();
				ApplyTorqueTowards(targetRotation);
			}
		}
        
        private void ApplyTorqueTowards(Quaternion targetRotation)
        {
	        Quaternion rotationDifference = targetRotation * Quaternion.Inverse(rigidBodyFollower.rotation);
	        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);
    
	        // Ensure the angle is between -180 and 180 degrees
	        if (angle > 180f)
	        {
		        angle -= 360f;
	        }
    
	        Vector3 angularVelocity = (angle * Mathf.Deg2Rad / Time.fixedDeltaTime) * axis.normalized;
	        rigidBodyFollower.angularVelocity = angularVelocity;
        }

		private void UpdateTarget()
		{
			if (useTransformAsTarget)
			{
				positionTarget = target.position;
				rotationTarget = target.rotation;
			}

			if (positionSpring.springEnabled)
			{
				positionSpring.SetTarget(positionTarget);
			}

			if (rotationSpring.springEnabled)
			{
				rotationSpring.SetTarget(rotationTarget);
			}
		}
		
		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (useTransformAsTarget && target == null)
			{
				AddErrorReason($"{gameObject.name} useTransformAsTarget is enabled but target cannot be null");
				res = false;
			}
			if (rigidBodyFollower == null)
			{
				AddErrorReason($"{gameObject.name} rigidBodyFollower cannot be null");
				res = false;
			}

			return res;
		}

		#region ENABLE/DISABLE SPRING PROPERTIES
		public SpringVector3 PositionSpring
		{
			get => positionSpring;
			set => positionSpring = value;
		}

		public SpringRotation RotationSpring
		{
			get => rotationSpring;
			set => rotationSpring = value;
		}
		#endregion

#if UNITY_EDITOR
		private void OnValidate()
		{
			if(rigidBodyFollower == null)
			{
				rigidBodyFollower = GetComponent<Rigidbody>();
			}
		}
#endif
	}
}