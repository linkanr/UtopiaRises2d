#if UNITY_EDITOR && ALLIN1SPRINGS_DEBUGGER

using System.Collections.Generic;

namespace AllIn1SpringsToolkit
{
	public static class AllIn1DebuggerWindowData
	{
		public class SpringState
		{
			public Spring spring;
			public bool isExpanded;
			public string springName;
			public string displayName;

			public SpringState(Spring spring, string springName, string displayName)
			{
				this.spring = spring;
				this.isExpanded = false;
				this.springName = springName;
				this.displayName = displayName;
			}

			public int GetID()
			{
				int res = spring.GetID();
				return res;
			}

			public override string ToString()
			{
				string res = $"\tID: {spring.GetID()} - Type: {spring.GetType()}";
				return res;
			}
		}

		public class SpringComponentState
		{
			public SpringComponent springComponent;
			public bool isExpanded;
			public List<SpringState> springsStates;

			public SpringComponentState(SpringComponent springComponent)
			{
				this.springComponent = springComponent;
				this.isExpanded = false;
				springsStates = new List<SpringState>();
			}

			public void AddSpringState(Spring spring)
			{
				SpringState springState = GetSpringStateBySpring(spring);
				if (springState == null)
				{
					string springFieldName = springComponent.GetSpringFieldName(spring);
					string displayName = char.ToUpper(springFieldName[0]) + springFieldName.Substring(1);

					springState = new SpringState(spring, springFieldName, displayName);
					springsStates.Add(springState);
				}
			}

			public int GetID()
			{
				int res = springComponent.GetInstanceID();
				return res;
			}

			public string GetName()
			{
				string res = springComponent.name;
				return res;
			}

			public string GetTypeName()
			{
				string res = springComponent.GetType().Name;
				return res;
			}

			public void SetActive(bool value)
			{
				springComponent.gameObject.SetActive(value);
			}

			public bool IsActiveInHierarchy()
			{
				bool res = springComponent.gameObject.activeInHierarchy;
				return res;
			}

			public SpringState GetSpringStateBySpring(Spring spring)
			{
				SpringState res = null;

				for (int i = 0; i < springsStates.Count; i++)
				{
					if (springsStates[i].GetID() == spring.GetID())
					{
						res = springsStates[i];
						break;
					}
				}

				return res;
			}

			public override string ToString()
			{
				string res = springComponent.name;
				res += "\n";

				for (int i = 0; i < springsStates.Count; i++)
				{
					res += springsStates[i];
					res += "\n";
				}

				return res;
			}
		}

		public static List<SpringComponentState> springComponentsStates = new List<SpringComponentState>();



		public static void RegisterSpring(SpringComponent springComponent, Spring spring)
		{
			if (springComponentsStates == null)
			{
				springComponentsStates = new List<SpringComponentState>();
			}

			SpringComponentState springComponentState = GetSpringComponentStateBySpringComponent(springComponent);
			if (springComponentState == null)
			{
				springComponentState = CreateSpringComponentState(springComponent);
			}

			springComponentState.AddSpringState(spring);
		}

		public static void UnregisterSpring(SpringComponent springComponent)
		{
			SpringComponentState springComponentStateToRemove = GetSpringComponentStateBySpringComponent(springComponent);
			if (springComponentStateToRemove != null)
			{
				springComponentsStates.Remove(springComponentStateToRemove);
			}
		}

		private static SpringComponentState GetSpringComponentStateBySpringComponent(SpringComponent springComponent)
		{
			SpringComponentState res = null;

			for (int i = 0; i < springComponentsStates.Count; i++)
			{
				if (springComponentsStates[i].GetID() == springComponent.GetInstanceID())
				{
					res = springComponentsStates[i];
					break;
				}
			}

			return res;
		}

		private static SpringComponentState CreateSpringComponentState(SpringComponent springComponent)
		{
			SpringComponentState res = new SpringComponentState(springComponent);
			springComponentsStates.Add(res);

			return res;
		}
	}
}

#endif