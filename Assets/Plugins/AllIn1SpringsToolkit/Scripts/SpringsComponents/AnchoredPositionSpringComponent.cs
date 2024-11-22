using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Anchored Position Spring Component")]
    public partial class AnchoredPositionSpringComponent : SpringComponent
    {
		[SerializeField] private SpringVector2 anchoredPositionSpring = new SpringVector2();

        [SerializeField] private bool useTransformAsTarget = false;
        [SerializeField] private RectTransform followRectTransform;
		[SerializeField] private RectTransform targetRectTransform;
        [SerializeField] private Vector2 anchoredPositionTarget;
        
        protected override void RegisterSprings()
        {
	        RegisterSpring(anchoredPositionSpring);
        }

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(followRectTransform.anchoredPosition);
			//anchoredPositionSpring.SetCurrentValue(followRectTransform.anchoredPosition);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(followRectTransform.anchoredPosition);
		}

		private void UpdateSpringSetTarget()
		{
			if (useTransformAsTarget)
			{
				anchoredPositionSpring.SetTarget(targetRectTransform.anchoredPosition);
			}
			else
			{
				anchoredPositionSpring.SetTarget(anchoredPositionTarget);
			}
		}

		private void SetTargetInternal(Vector2 target)
		{
			if (useTransformAsTarget) 
			{
				targetRectTransform.anchoredPosition = target;
			}
			else
			{
				anchoredPositionTarget = target;
			}
		}

		private void SetCurrentValueInternal(Vector2 currentValue)
		{
			UpdateFollowerTransform();
		}

		private void ReachEquilibriumInternal()
		{
			UpdateFollowerTransform();
		}

		public void Update()
        {
			if (!initialized) { return; }

			RefreshAnchoredPositionTarget();
			UpdateSpringSetTarget();
            UpdateFollowerTransform();
        }

		private void RefreshAnchoredPositionTarget()
		{
			if (useTransformAsTarget)
			{
				anchoredPositionTarget = targetRectTransform.anchoredPosition;
			}
		}

        private void UpdateFollowerTransform()
        {
            followRectTransform.anchoredPosition = anchoredPositionSpring.GetCurrentValue();
        }
        
		public override bool IsValidSpringComponent()
        {
			bool res = true;

			if (useTransformAsTarget && targetRectTransform == null)
			{
				AddErrorReason($"{gameObject.name} useTransformAsTarget is enabled but targetRectTransform is null");
				res = false;
			}
			if (followRectTransform == null)
			{
				AddErrorReason($"{gameObject.name} followRectTransform cannot be null");
				res = false;
			}

            return res;
        }

        private void OnValidate()
        {
	        if(followRectTransform == null)
	        {
		        followRectTransform = GetComponent<RectTransform>();
	        }
        }

		#region API PUBLIC METHODS

		/*
		public void SetTarget(Vector2 target)
		{
			if (useTransformAsTarget)
			{
				targetRectTransform.anchoredPosition = target;
			}
			else
			{
				anchoredPositionTarget = target;
			}

			anchoredPositionSpring.SetTarget(target);
		}
		*/

		#endregion
	}
}