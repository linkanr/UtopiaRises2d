using UnityEngine;

namespace AllIn1SpringsToolkit.Bones
{
	public class SpringBoneAttachable : SpringBone
	{
		public Vector3 attachPointLocalSpace;

		public override void Initialize(Quaternion initialRotationParent)
		{
			base.Initialize(initialRotationParent);

			attachPointLocalSpace = boneParent.transform.InverseTransformPoint(transform.position);
		}

		public override void UpdateRotations()
		{
			base.UpdateRotations();

			transform.position = boneParent.transform.TransformPoint(attachPointLocalSpace);
		}

#if UNITY_EDITOR
		public void Reset()
		{
			bool componentFound = gameObject.TryGetComponent(out rotationSpringComponent);
			if (!componentFound)
			{
				rotationSpringComponent = gameObject.AddComponent<RotationSpringComponent>();
			}
		}
#endif
	}
}