using UnityEditor;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringEditorObject
	{
		protected const int X = 0;
		protected const int Y = 1;
		protected const int Z = 2;
		protected const int W = 3;

		protected const int R = 0;
		protected const int G = 1;
		protected const int B = 2;
		protected const int A = 3;

		public SerializedProperty spParentroperty;

		public SerializedProperty spUnifiedForceAndDrag;
		public SerializedProperty spUnifiedForce;
		public SerializedProperty spUnifiedDrag;

		public SerializedProperty spSpringValues;
		public SerializedProperty spShowDebugFields;

		public SerializedProperty spUseInitialValues;
		public SerializedProperty spUseCustomTarget;

		public SerializedProperty spSpringEnabled;
		public SerializedProperty spClampingEnabled;

		public SerializedProperty spEventsEnabled;

		public SerializedProperty spUnfolded;

		public SpringValuesEditorObject[] springValuesEditorObjects;

		public bool unifiedForceAndDrag
		{
			get
			{
				return spUnifiedForceAndDrag.boolValue;
			}
			set
			{
				spUnifiedForceAndDrag.boolValue = value;
			}
		}

		public bool UseInitialValues
		{
			get
			{
				return spUseInitialValues.boolValue;
			}
			set
			{
				spUseInitialValues.boolValue = value;
			}
		}

		public bool UseCustomTarget
		{
			get
			{
				return spUseCustomTarget.boolValue;
			}
			set
			{
				spUseCustomTarget.boolValue = value;
			}
		}

		public bool Unfolded
		{
			get
			{
				return spUnfolded.boolValue;
			}
			set
			{
				spUnfolded.boolValue = value;
			}
		}

		public bool ClampingEnabled
		{
			get
			{
				return spClampingEnabled.boolValue;
			}
			set
			{
				spClampingEnabled.boolValue = value;
			}
		}

		public SpringEditorObject(SerializedProperty spParentroperty, int size)
		{
			this.spParentroperty = spParentroperty;
			this.spSpringValues = spParentroperty.FindPropertyRelative("springValues");
			spSpringValues.arraySize = size;

			springValuesEditorObjects = new SpringValuesEditorObject[size];

			this.spUnifiedForceAndDrag = spParentroperty.FindPropertyRelative("unifiedForceAndDrag");
			this.spUnifiedForce = spParentroperty.FindPropertyRelative("unifiedForce");
			this.spUnifiedDrag = spParentroperty.FindPropertyRelative("unifiedDrag");

			this.spUseInitialValues = spParentroperty.FindPropertyRelative("useInitialValues");
			this.spUseCustomTarget = spParentroperty.FindPropertyRelative("useCustomTarget");

			this.spSpringEnabled = spParentroperty.FindPropertyRelative("springEnabled");
			this.spClampingEnabled = spParentroperty.FindPropertyRelative("clampingEnabled");

			this.spShowDebugFields = spParentroperty.FindPropertyRelative("showDebugFields");

			this.spEventsEnabled = spParentroperty.FindPropertyRelative("eventsEnabled");

			this.spUnfolded = spParentroperty.FindPropertyRelative("unfolded");

			CreateSpringValuesEditorObjects();
		}

		public int GetSize()
		{
			int res = springValuesEditorObjects.Length;
			return res;
		}

		public void AddSpringValuesEditorObject(SpringValuesEditorObject springValuesEditorObject, int index)
		{
			springValuesEditorObjects[index] = springValuesEditorObject;
		}

		public void CreateSpringValuesEditorObjects()
		{
			for (int i = 0; i < springValuesEditorObjects.Length; i++)
			{
				SerializedProperty sp = spSpringValues.GetArrayElementAtIndex(i);

				SerializedProperty spInitialValue = sp.FindPropertyRelative("initialValue");

				SerializedProperty spClampTarget = sp.FindPropertyRelative("clampTarget");
				SerializedProperty spClampCurrentValue = sp.FindPropertyRelative("clampCurrentValue");
				SerializedProperty spStopSrpingOnCurrentValueClamp = sp.FindPropertyRelative("stopSpringOnCurrentValueClamp");

				SerializedProperty spMinValue = sp.FindPropertyRelative("minValue");
				SerializedProperty spMaxValue = sp.FindPropertyRelative("maxValue");

				SerializedProperty spUpdate = sp.FindPropertyRelative("update");
				SerializedProperty spForce = sp.FindPropertyRelative("force");
				SerializedProperty spDrag = sp.FindPropertyRelative("drag");

				SerializedProperty spTarget = sp.FindPropertyRelative("target");

				SerializedProperty spCurrentValue = sp.FindPropertyRelative("currentValue");
				SerializedProperty spVelocity = sp.FindPropertyRelative("velocity");

				SerializedProperty spCandidateValue = sp.FindPropertyRelative("candidateValue");

				SerializedProperty spOperationValue = sp.FindPropertyRelative("operationValue");

				springValuesEditorObjects[i] = new SpringValuesEditorObject(
					spInitialValue: spInitialValue,
					spClampTarget: spClampTarget, spClampCurrentValue: spClampCurrentValue, spStopSpringOnCurrentValueClamp: spStopSrpingOnCurrentValueClamp,
					spMinValue: spMinValue,
					spMaxValue: spMaxValue,
					spUpdate: spUpdate,
					spForce: spForce,
					spDrag: spDrag,
					spTarget: spTarget,
					spCurrentValue: spCurrentValue,
					spVelocity: spVelocity,
					spCandidateValue: spCandidateValue,
					spOperationValue: spOperationValue);
			}
		}

		public bool IsDebugShowEnabled()
		{
			bool res = spShowDebugFields.boolValue;
			return res;
		}

		public bool IsClampTargetEnabled()
		{
			bool res = false;

			for (int i = 0; i < springValuesEditorObjects.Length; i++)
			{
				res = res || springValuesEditorObjects[i].GetClampTarget();
			}

			return res;
		}

		public void SetAllClampTarget(bool value)
		{
			for (int i = 0; i < springValuesEditorObjects.Length; i++)
			{
				springValuesEditorObjects[i].SetClampTarget(value);
			}
		}

		public void SetAllClampCurrentValue(bool value)
		{
			for (int i = 0; i < springValuesEditorObjects.Length; i++)
			{
				springValuesEditorObjects[i].SetClampCurrentValue(value);
			}
		}

		public bool IsClampCurrentValueEnabled()
		{
			bool res = false;

			for (int i = 0; i < springValuesEditorObjects.Length; i++)
			{
				res = res || springValuesEditorObjects[i].GetClampCurrentValue();
			}

			return res;
		}

		public virtual float GetExtraFieldsHeight()
		{
			float res = 0f;
			return res;
		}

		public abstract void AddVelocityNudge();

		public abstract void SetCurrentValuesNudge();

		public abstract void SetTargetNudge();
	}
}