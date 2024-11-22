using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Rotation Spring Component")]
	public partial class RotationSpringComponent : SpringComponent
	{
		[SerializeField] private SpringRotation springRotation = new SpringRotation();

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(Quaternion.identity);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(Quaternion.identity);
		}

		protected override void RegisterSprings()
		{
			RegisterSpring(springRotation);
		}
	}
}