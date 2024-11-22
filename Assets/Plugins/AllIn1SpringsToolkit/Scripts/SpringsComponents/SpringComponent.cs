using UnityEngine;
using System.Collections.Generic;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringComponent : MonoBehaviour
	{
		[Tooltip("When enabled, the Spring Component will initialize automatically on Awake. " +
		         "Otherwise you'll need to call Initialize() manually.")]
		[SerializeField] public bool doesAutoInitialize;
		
		public bool useScaledTime = true;

		private List<Spring> springs;

		[Tooltip("When enabled, sets the initial Current Value to a custom predefined value instead of the default")]
		[SerializeField] protected bool hasCustomInitialValues;

		[Tooltip("When enabled, uses a custom Target value instead of using the default Target")]
		[SerializeField] protected bool hasCustomTarget;
		
		private bool isValidSpringComponent;
		private string errorReason = string.Empty;
		
		[HideInInspector] public bool generalPropertiesUnfolded = true;
		[HideInInspector] public bool initialValuesUnfolded;
		private bool doFixedUpdateRate;
		private float springFixedTimeStep;

		protected bool initialized;

		private void Awake()
		{
			if (doesAutoInitialize)
			{
				Initialize();
			}
		}

		public virtual void OnDestroy()
		{
			UnregisterSprings();
		}

		public virtual void Initialize()
		{
			springs = new List<Spring>();
			doFixedUpdateRate = SpringsToolkitSettings.DoFixedUpdateRate;
			springFixedTimeStep = SpringsToolkitSettings.SpringFixedTimeStep;

			this.isValidSpringComponent = IsValidSpringComponent();
			if (!isValidSpringComponent)
			{
				SpringComponentNotValid();
			}
			else
			{
				RegisterSprings();
				CheckSpringSizes();

				for (int i = 0; i < springs.Count; i++)
				{
					springs[i].Initialize();
				}

				SetInitialValues();
			}

			initialized = true;
		}

		protected virtual void SetInitialValues()
		{
			if (!hasCustomInitialValues)
			{
				SetCurrentValueByDefault();
			}
			if (!hasCustomTarget)
			{
				SetTargetByDefault();
			}
		}

		private void SpringComponentNotValid()
		{
			string objectTypeName = GetType().ToString();
			objectTypeName = objectTypeName.Replace("AllIn1SpringsToolkit.", "");
			string fullErrorMessage = $"{gameObject.name} {objectTypeName} is not valid! Disabling Component";
			
			if (!string.IsNullOrEmpty(errorReason))
			{
				fullErrorMessage += $"\nReason: {errorReason}";
			}

			Debug.LogError(fullErrorMessage, gameObject);
			enabled = false;
		}

		protected void AddErrorReason(string reason)
		{
			if (!string.IsNullOrEmpty(errorReason))
			{
				errorReason += "\n";
			}

			errorReason += reason;
		}
		
		protected abstract void SetCurrentValueByDefault();
		protected abstract void SetTargetByDefault();

		protected abstract void RegisterSprings();

		public abstract bool IsValidSpringComponent();

		protected void RegisterSpring(Spring spring)
		{
			this.springs.Add(spring);

			spring.springEvents.SetSpring(spring);

#if ALLIN1SPRINGS_DEBUGGER && UNITY_EDITOR
			spring.AssignID();
			AllIn1DebuggerWindowData.RegisterSpring(this, spring);
#endif
		}

		private void UnregisterSprings()
		{
#if ALLIN1SPRINGS_DEBUGGER && UNITY_EDITOR
			AllIn1DebuggerWindowData.UnregisterSpring(this);
#endif
		}

		public virtual void LateUpdate()
		{
			if (!initialized)
			{
				return;
			}

			isValidSpringComponent = IsValidSpringComponent();
			if (!isValidSpringComponent)
			{
				SpringComponentNotValid();
				return;
			}
			
			float deltaTime = useScaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
			if(doFixedUpdateRate)
			{
				int numIterations = GetNumberOfFixedSteps(deltaTime);
				if (numIterations > 1)
				{
					float remainingDeltaTime = deltaTime;
					while (remainingDeltaTime > 0f)
					{
						float correctedDeltaTime;
						if(remainingDeltaTime > springFixedTimeStep)
						{
							correctedDeltaTime = springFixedTimeStep;
						}
						else
						{
							correctedDeltaTime = remainingDeltaTime;
						}

						UpdateSprings(correctedDeltaTime);
						remainingDeltaTime -= springFixedTimeStep;
					}
				}
				else
				{
					UpdateSprings(deltaTime);
				}
			}
			else
			{
				UpdateSprings(deltaTime);
			}
		}

		private void UpdateSprings(float deltaTime)
		{
			for (int i = 0; i < springs.Count; i++)
			{
				if (!springs[i].springEnabled)
				{
					continue;
				}

				SpringLogic.UpdateSpring(deltaTime, springs[i]);
				
				springs[i].CheckEvents();
				springs[i].ProcessCandidateValue();
			}
		}

		public virtual void ReachEquilibrium()
		{
			ValidateSpringsList();

			for (int i = 0; i < springs.Count; i++)
			{
				springs[i].ReachEquilibrium();
			}
		}

		private void ValidateSpringsList()
		{
			if(springs == null)
			{
				if(doesAutoInitialize)
				{
					Debug.LogWarning($"{gameObject.name} is not initialized, so we'll Initialize it now. " +
					                 $" {gameObject.name} was probably enabled this frame." +
					                 $" In these cases you can disable DoesAutoInitialize and call Initialize() yourself", gameObject);
					Initialize();
				}
				else
				{
					Debug.LogWarning($"{gameObject.name} is not initialized. " +
					                 $" DoesAutoInitialize is disabled and you forgot to Initialize()", gameObject);
				}
			}
		}

		private void CheckSpringSizes()
		{
			for (int i = 0; i < springs.Count; i++)
			{
				bool hasValidSize = springs[i].CheckCorrectSize();

				if (!hasValidSize)
				{
					Debug.LogWarning($"Spring size not valid! Fix in the editor to avoid performance issues: [{gameObject.name}]", gameObject);
				}
			} 
		}
		
		private int GetNumberOfFixedSteps(float deltaTime)
		{
			int res = 1;
			if (deltaTime > springFixedTimeStep)
			{
				res = Mathf.RoundToInt(deltaTime / springFixedTimeStep);
			}
			return res;
		}

#if ALLIN1SPRINGS_DEBUGGER && UNITY_EDITOR
		public string GetSpringFieldName(Spring spring)
		{
			string res = string.Empty;

            System.Reflection.FieldInfo[] fields = this.GetType().GetFields(
				System.Reflection.BindingFlags.NonPublic |
				System.Reflection.BindingFlags.Public |
				System.Reflection.BindingFlags.Instance);

			for(int i = 0; i < fields.Length; i++)
			{
				if (fields[i].FieldType == spring.GetType())
				{
					Spring springTmp = (Spring)fields[i].GetValue(this);
					if (springTmp.GetID() == spring.GetID())
					{
						res = fields[i].Name;
						break;
					}
				}
			}

			return res;
		}
#endif
#if UNITY_EDITOR
		protected virtual void Reset()
		{
			doesAutoInitialize = true;
		}

		private void OnDrawGizmosSelected()
		{
			DrawGizmosSelected();
		}

		protected virtual void DrawGizmosSelected()
		{
			if (springs == null) { return; }

			for (int i = 0; i < springs.Count; i++)
			{
				springs[i].DrawGizmosSelected(transform.position);
			}
		}
#endif
	}
}