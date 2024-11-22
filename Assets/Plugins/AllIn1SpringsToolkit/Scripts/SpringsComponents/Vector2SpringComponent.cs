using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Vector2 Spring Component")]
	public partial class Vector2SpringComponent : SpringComponent
	{
		[SerializeField] private SpringVector2 springVector2 = new SpringVector2(); 

		protected override void RegisterSprings()
		{
			RegisterSpring(springVector2);
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(Vector2.zero);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(Vector2.zero);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
	}
}