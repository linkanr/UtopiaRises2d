using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Vector4 Spring Component")]
	public partial class Vector4SpringComponent : SpringComponent
	{
		[SerializeField] private SpringVector4 springVector4 = new SpringVector4();

		protected override void RegisterSprings()
		{
			RegisterSpring(springVector4);
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(Vector4.zero);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(Vector4.zero);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
	}
}