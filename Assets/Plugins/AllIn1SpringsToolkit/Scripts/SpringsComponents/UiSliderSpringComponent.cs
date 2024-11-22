using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Ui Slider Spring Component")]
	public partial class UiSliderSpringComponent : SpringComponent
	{
		[SerializeField] private SpringFloat fillAmountSpring = new SpringFloat();

		[SerializeField] private Image autoUpdatedSliderImage;

		protected override void RegisterSprings()
		{
			RegisterSpring(fillAmountSpring);
		}

		protected override void SetCurrentValueByDefault()
		{
			fillAmountSpring.SetCurrentValue(autoUpdatedSliderImage.fillAmount);
		}

		protected override void SetTargetByDefault()
		{
			fillAmountSpring.SetTarget(autoUpdatedSliderImage.fillAmount);
		}

		public void Update()
		{
			if (!initialized) { return; } 

			UpdateFillAmount();
		}

		private void UpdateFillAmount()
		{
			autoUpdatedSliderImage.fillAmount = fillAmountSpring.GetCurrentValue();
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if(autoUpdatedSliderImage == null)
			{
				AddErrorReason($"{gameObject.name} autoUpdatedSliderImage is null.");
				res = false;
			}

			return res;
		}

		private void ReachEquilibriumInternal()
		{
			UpdateFillAmount();
		}

		private void SetCurrentValueInternal(float currentValues)
		{
			UpdateFillAmount();
		}

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			if(autoUpdatedSliderImage == null)
			{
				autoUpdatedSliderImage = GetComponent<Image>();
			}
		}
#endif
	}
}
