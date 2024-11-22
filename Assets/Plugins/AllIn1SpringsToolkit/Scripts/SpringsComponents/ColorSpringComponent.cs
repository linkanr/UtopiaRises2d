using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Color Spring Component")]
	public partial class ColorSpringComponent : SpringComponent
	{
		[SerializeField] private SpringColor colorSpring = new SpringColor();

		[SerializeField] private bool autoUpdate;
		[SerializeField] private bool autoUpdatedObjectIsRenderer = true;
		[SerializeField] private Renderer autoUpdatedRenderer;
		[SerializeField] private Graphic autoUpdatedUiGraphic;
		
		private Color GetDefaultColor()
		{
			Color res = Color.white;

			if (autoUpdate)
			{
				if (autoUpdatedObjectIsRenderer)
				{
					res = autoUpdatedRenderer is SpriteRenderer spriteRenderer ? spriteRenderer.color : autoUpdatedRenderer.material.color;
				}
				else
				{
					res = autoUpdatedUiGraphic.color;
				}
			}

			return res;
		}

		protected override void SetCurrentValueByDefault()
		{
			Color defaultColor = GetDefaultColor();
			colorSpring.SetCurrentValue(defaultColor);
		}

		protected override void SetTargetByDefault()
		{
			Color defaultColor = GetDefaultColor();
			colorSpring.SetTarget(defaultColor);
		}

		public void Update()
		{
			if (!initialized) { return; }

			UpdateAutoUpdatedObject();
		}

		private void UpdateAutoUpdatedObject()
		{
			if (autoUpdate)
			{
				if (autoUpdatedObjectIsRenderer)
				{
					autoUpdatedRenderer.material.color = GetCurrentValue();
				}
				else
				{
					autoUpdatedUiGraphic.color = GetCurrentValue();
				}
			}
		}

		protected override void RegisterSprings()
		{
			RegisterSpring(colorSpring);
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (autoUpdate)
			{
				if (autoUpdatedObjectIsRenderer)
				{
					if (autoUpdatedRenderer == null)
					{
						AddErrorReason($"{gameObject.name} ColorSpringComponent Target Renderer is null but targetIsRenderer is enabled.");
						res = false;
					}
				}
				else if (autoUpdatedUiGraphic == null)
				{
					AddErrorReason($"{gameObject.name} ColorSpringComponent Target Image is null but autoUpdate is enabled.");
					res = false;
				}
			}

			return res;
		}

		private void ReachEquilibriumInternal()
		{
			UpdateAutoUpdatedObject();
		}

		private void SetCurrentValueInternal(Color currentValues)
		{
			UpdateAutoUpdatedObject();
		}

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			if (autoUpdatedRenderer == null)
			{
				autoUpdatedRenderer = GetComponent<Renderer>();
			}
			if (autoUpdatedUiGraphic == null)
			{
				autoUpdatedUiGraphic = GetComponent<Graphic>();
			}
			
			colorSpring.unifiedForceAndDrag = true;
			colorSpring.unifiedForce = 50f;
			colorSpring.unifiedDrag = 10f;
		}
#endif

		#region API PUBLIC METHODS

		public void SetAutoUpdateToFalse()
		{
			autoUpdate = false;
		}
		
		public void SetAutoUpdateToTrue(bool isRenderer, GameObject autoUpdatedObject)
		{
			autoUpdate = true;
			autoUpdatedObjectIsRenderer = isRenderer;
			autoUpdatedRenderer = autoUpdatedObject.GetComponent<Renderer>();
			autoUpdatedUiGraphic = autoUpdatedObject.GetComponent<Graphic>();
		}

		#endregion
	}
}