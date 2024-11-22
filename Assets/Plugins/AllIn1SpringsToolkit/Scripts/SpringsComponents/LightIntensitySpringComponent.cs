using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Light Intensity Spring Component")]
	public partial class LightIntensitySpringComponent : SpringComponent
	{
		[SerializeField] private SpringFloat lightIntensitySpring = new SpringFloat();

		[SerializeField] private Light autoUpdatedLight;

		protected override void RegisterSprings()
		{
			RegisterSpring(lightIntensitySpring);
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(autoUpdatedLight.intensity);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(autoUpdatedLight.intensity);
		}

		public void Update()
		{
			if (!initialized) { return; }

			UpdateLightIntensity();
		}

		public void UpdateLightIntensity()
		{
			autoUpdatedLight.intensity = GetCurrentValue();
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (autoUpdatedLight == null)
			{
				AddErrorReason($"{gameObject.name} autoUpdatedLight is null.");
				res = false;
			}
			
			return res;
		}

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			if(autoUpdatedLight == null)
			{
				autoUpdatedLight = GetComponent<Light>();
			}
		}
#endif
	}
}
