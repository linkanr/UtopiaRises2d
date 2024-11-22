using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Vector3 Spring Component")]
	public partial class Vector3SpringComponent : SpringComponent
	{
		[SerializeField] private SpringVector3 springVector3 = new SpringVector3();

		protected override void RegisterSprings()
		{
			RegisterSpring(springVector3);
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(Vector3.zero);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(Vector3.zero);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
	}
}