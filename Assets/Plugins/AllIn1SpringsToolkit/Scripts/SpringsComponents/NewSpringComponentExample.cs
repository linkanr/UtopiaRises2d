using UnityEngine;

namespace AllIn1SpringsToolkit
{
    public partial class NewSpringComponentExample : SpringComponent
    {
        //This component is an example template of how to create a new SpringComponent
        //Goal of the component: This component will use a SpringFloat to animate the X position of a transform
        
        //These are the overall steps that you need to follow to create your own SpringComponent:
        //1. Create a new script that inherits from SpringComponent
        //2. Declare the Spring(s) that you will use in the component
        //3. Register the Spring(s) in the RegisterSprings method
        //4. Return true or false in the IsValidSpringComponent method
        //5. Set the initial values of the Spring(s) in the SetInitialValues method (otherwise the springs will start at 0)
        //6. Use the Spring(s) in the Update method (or in any other method you see fit)
        
        //Initializing the spring protects us from errors caused by incorrect AddComponent handling (optional initialization)
        public SpringFloat springFloat = new SpringFloat();
        [SerializeField] private Transform exampleTransform;
        [SerializeField] private float targetXPosition;
        
        public override bool IsValidSpringComponent()
        {
            //Here it's a good place to check if the component has all necessary references
            //If the component is not valid, it will be automatically disabled
            
            //For example here we make sure that the exampleTransform is not null
            bool isExampleTransformValid = exampleTransform != null;
            
            if (!isExampleTransformValid)
			{
				//Additionally and optionally we can add an error reason to be displayed in the console in a nice and understandable way
				AddErrorReason($"{gameObject.name} exampleTransform is null.");
			}
            
            return isExampleTransformValid;
        }

        protected override void RegisterSprings()
        {
            //For each spring you have to call RegisterSpring(spring)
            //This will add the spring to the list of springs that will be updated
            //And will also register the spring in the DebuggerWindow if ALLIN1SPRINGS_DEBUGGER is enabled
            RegisterSpring(springFloat);
        }

		protected override void SetCurrentValueByDefault()
		{
			//Defaulting to some value found in the scene will always be our default behavior (when possible)
			springFloat.SetCurrentValue(exampleTransform.position.x);
		}

		protected override void SetTargetByDefault()
		{
			//Defaulting to some value found in the scene will always be our default behavior (when possible)
			springFloat.SetTarget(exampleTransform.position.x);
		}

		public void Update()
        {
			//If not initialized we return. This protects us from errors caused by incorrect AddComponent handling (optional line)
			if (!initialized) { return; }

			//Here we set the target of the spring every frame
			//But we could do it in a separate function or whatever you see fit
			springFloat.SetTarget(targetXPosition);
            
            //Here we update the transform position with the spring value
            Vector3 newPosition = exampleTransform.position;
            newPosition.x = springFloat.GetCurrentValue();
            exampleTransform.position = newPosition;
        }
	}
}