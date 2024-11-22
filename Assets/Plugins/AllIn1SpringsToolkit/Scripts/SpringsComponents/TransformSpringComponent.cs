using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Transform Spring Component")] 
	public partial class TransformSpringComponent : SpringComponent
	{
		public enum SpaceType
		{
			WorldSpace,
			LocalSpace,
		}
		
		public SpaceType spaceType;

		[SerializeField] protected SpringVector3 positionSpring = new SpringVector3();
		[SerializeField] protected SpringVector3 scaleSpring = new SpringVector3();
		[SerializeField] protected SpringRotation rotationSpring = new SpringRotation();

		[Tooltip("Follower Transform will be set to match the spring values every frame")]
		public Transform followerTransform;

		[Tooltip("When enabled, the Target position, rotation, and scale this is TargetTransform. When disabled, you must manually set targets via code")]
		public bool useTransformAsTarget;
		public Transform targetTransform;

		private Vector3 positionTarget;
		private Vector3 scaleTarget;
		private Quaternion rotationTarget;

		#region INIT

		protected override void RegisterSprings()
		{
			RegisterSpring(positionSpring);
			RegisterSpring(scaleSpring);
			RegisterSpring(rotationSpring);
		}

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
				if (!scaleSpring.useInitialValues)
				{
					SetCurrentValueScaleByDefault();
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
					if (!scaleSpring.useCustomTarget)
					{
						SetTargetScaleByDefault();
					}
				}
			}
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValuePositionByDefault();
			SetCurrentValueRotationByDefault();
			SetCurrentValueScaleByDefault();
		}

		private void SetCurrentValuePositionByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				positionSpring.SetCurrentValue(followerTransform.localPosition);
			}
			else
			{
				positionSpring.SetCurrentValue(followerTransform.position);
			}
		}

		private void SetCurrentValueRotationByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				rotationSpring.SetCurrentValue(followerTransform.localRotation);
			}
			else
			{
				rotationSpring.SetCurrentValue(followerTransform.rotation);
			}
		}

		private void SetCurrentValueScaleByDefault()
		{
			scaleSpring.SetCurrentValue(followerTransform.localScale);
		}

		protected override void SetTargetByDefault()
		{
			SetTargetPositionByDefault();
			SetTargetRotationByDefault();
			SetTargetScaleByDefault();
		}

		private void SetTargetPositionByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				positionSpring.SetTarget(followerTransform.localPosition);
			}
			else
			{
				positionSpring.SetTarget(followerTransform.position);
			}
		}

		private void SetTargetRotationByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				rotationSpring.SetTarget(followerTransform.localRotation);
			}
			else
			{
				rotationSpring.SetTarget(followerTransform.rotation);
			}
		}

		private void SetTargetScaleByDefault()
		{
			scaleSpring.SetTarget(followerTransform.localScale);
		}

		#endregion

		private void Start()
		{
			if (!initialized) { return; }

			UpdateTransform();
		}

		public void Update()
		{
			if (!initialized) { return; }

			UpdateTransform();

			if (useTransformAsTarget)
			{
				UpdateTarget();
			}
		}

		#region UPDATE

		public void UpdateTarget()
		{
			if (spaceType == SpaceType.WorldSpace)
			{
				GetTargetsWorldSpace();
			}
			else if (spaceType == SpaceType.LocalSpace)
			{
				GetTargetsLocalSpace();
			}

			RefreshSpringsTargets();
		}

		private void UpdateTransform()
		{
			if (spaceType == SpaceType.WorldSpace)
			{
				UpdateTransformWorldSpace();
			}
			else if (spaceType == SpaceType.LocalSpace)
			{
				UpdateTransformLocalSpace();
			}
		}

		private void UpdateTransformWorldSpace()
		{
			if (positionSpring.springEnabled)
			{
				followerTransform.position = positionSpring.GetCurrentValue();
			}

			if (rotationSpring.springEnabled)
			{
				followerTransform.rotation = rotationSpring.GetCurrentValue();
			}

			if (scaleSpring.springEnabled)
			{
				followerTransform.localScale = scaleSpring.GetCurrentValue();
			}
		}

		private void UpdateTransformLocalSpace()
		{
			if (positionSpring.springEnabled)
			{
				followerTransform.localPosition = positionSpring.GetCurrentValue();
			}

			if (rotationSpring.springEnabled)
			{
				followerTransform.localRotation = rotationSpring.GetCurrentValue();
			}

			if (scaleSpring.springEnabled)
			{
				followerTransform.localScale = scaleSpring.GetCurrentValue();
			}
		}

		private void GetTargetsWorldSpace()
		{
			if (positionSpring.springEnabled)
			{
				positionTarget = targetTransform.position;
			}

			if (rotationSpring.springEnabled)
			{
				rotationTarget = targetTransform.rotation;
			}

			if (scaleSpring.springEnabled)
			{
				scaleTarget = targetTransform.localScale;
			}
		}

		private void GetTargetsLocalSpace()
		{
			if (positionSpring.springEnabled)
			{
				positionTarget = targetTransform.localPosition;
			}

			if (rotationSpring.springEnabled)
			{
				rotationTarget = targetTransform.localRotation;
			}

			if (scaleSpring.springEnabled)
			{
				scaleTarget = targetTransform.localScale;
			}
		}

		private void RefreshSpringsTargets()
		{
			positionSpring.SetTarget(positionTarget);

			if(spaceType == SpaceType.WorldSpace)
			{
				rotationSpring.SetTarget(rotationTarget);
			}
			else
			{
				rotationSpring.SetTarget(rotationTarget);
			}

			scaleSpring.SetTarget(scaleTarget);
		}

		#endregion

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if(useTransformAsTarget && targetTransform == null)
			{
				AddErrorReason($"{gameObject.name} useTransformAsTarget is enabled but targetTransform is null");
				res = false;
			}
			if(followerTransform == null)
			{
				AddErrorReason($"{gameObject.name} followerTransform cannot be null");
				res = false;
			}
			
			return res;
		}
		
		#region ENABLE/DISABLE SPRING PROPERTIES
		public bool SpringPositionEnabled
		{
			get => positionSpring.springEnabled;
			set => positionSpring.springEnabled = value;
		}

		public bool SpringRotationEnabled
		{
			get => rotationSpring.springEnabled;
			set => rotationSpring.springEnabled = value;
		}

		public bool SpringScaleEnabled
		{
			get => scaleSpring.springEnabled;
			set => scaleSpring.springEnabled = value;
		}
		#endregion

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			followerTransform = transform;
		}

		protected override void DrawGizmosSelected()
		{
			positionSpring.DrawGizmosSelected(transform.position);
			rotationSpring.DrawGizmosSelected(transform.position);
			scaleSpring.DrawGizmosSelected(transform.position);
		}
#endif
	}
}
