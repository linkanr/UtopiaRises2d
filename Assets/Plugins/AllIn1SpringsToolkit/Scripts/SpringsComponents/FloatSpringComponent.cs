using UnityEngine;

namespace AllIn1SpringsToolkit
{
	//Broad purpose spring class that can be used to animate any float value, we can then use this spring values to feed them to Reactor components
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Float Spring Component")]
	public partial class FloatSpringComponent : SpringComponent
	{
		[SerializeField] private SpringFloat springFloat = new SpringFloat();
		
		protected override void RegisterSprings()
		{
			RegisterSpring(springFloat);
		}

		protected override void SetCurrentValueByDefault()
		{
			springFloat.SetCurrentValue(0f);
		}

		protected override void SetTargetByDefault()
		{
			springFloat.SetTarget(0f);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}


	}
}
