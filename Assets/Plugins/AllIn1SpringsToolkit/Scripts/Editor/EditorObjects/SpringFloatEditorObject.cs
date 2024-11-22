using UnityEditor;

namespace AllIn1SpringsToolkit
{
	public class SpringFloatEditorObject : SpringEditorObject
	{
		public SpringFloatEditorObject(SerializedProperty spParent) : base(spParent, SpringFloat.SPRING_SIZE) 
		{}
		
		public float InitialValue
		{
			get
			{
				float res = springValuesEditorObjects[X].GetInitialValue();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetInitialValue(value);
			}
		}

		public bool Update
		{
			get
			{
				bool res = springValuesEditorObjects[X].GetUpdate();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetUpdate(value);
			}
		}

		public bool ClampTarget
		{
			get
			{
				bool res = springValuesEditorObjects[X].GetClampTarget();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampTarget(value);
			}
		}

		public bool ClampCurrentValue
		{
			get
			{
				bool res = springValuesEditorObjects[X].GetClampCurrentValue();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetClampCurrentValue(value);
			}
		}

		public bool StopSpringOnCurrentValueClamp
		{
			get
			{
				bool res = springValuesEditorObjects[X].GetStopSpringOnCurrentValueClamp();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetStopSpringOnCurrentValueClamp(value);
			}
		}

		public float Force
		{
			get
			{
				float res = springValuesEditorObjects[X].GetForce();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetForce(value);
			}
		}

		public float Drag
		{
			get
			{
				float res = springValuesEditorObjects[X].GetDrag();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetDrag(value);
			}
		}

		public float MinValue
		{
			get
			{
				float res = springValuesEditorObjects[X].GetMinValue();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMinValue(value);
			}
		}

		public float MaxValue
		{
			get
			{
				float res = springValuesEditorObjects[X].GetMaxValue();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetMaxValue(value);
			}
		}

		public float Target
		{
			get
			{
				float res = springValuesEditorObjects[X].GetTarget();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetTarget(value);
			}
		}

		public float CurrentValue
		{
			get
			{
				float res = springValuesEditorObjects[X].GetCurrentValue();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetCurrentValue(value);
			}
		}

		public float Velocity
		{
			get
			{
				float res = springValuesEditorObjects[X].GetVelocity();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetVelocity(value);
			}
		}

		public float OperationValue
		{
			get
			{
				float res = springValuesEditorObjects[X].GetOperationValue();
				return res;
			}
			set
			{
				springValuesEditorObjects[X].SetOperationValue(value);
			}
		}

		public override void AddVelocityNudge()
		{
			Velocity += OperationValue;
		}

		public override void SetCurrentValuesNudge()
		{
			CurrentValue = OperationValue;
		}

		public override void SetTargetNudge()
		{
			Target = OperationValue;
		}
	}
}