using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit.Bones
{
    public class SpringBone : MonoBehaviour
    {
		public SpringBone boneParent;

		private Quaternion initialRotation;
		private Quaternion quatDelta;

		protected Vector3 deltaPosition;

		private Vector3 lastPosition;



		public RotationSpringComponent rotationSpringComponent;

		public List<SpringBone> children;

		public int hierarchyLevel;
		public float normalizedHierarchyLevel;

		public bool IsRoot
		{
			get
			{
				bool res = hierarchyLevel == 0;
				return res;
			}
		}

		[Space, Header("Movement Inertia")]
        [SerializeField] private bool inertiaEnabled;

		public SpringSkeleton skeleton;


#if UNITY_EDITOR
		public bool enableDebug;
#endif

		public void ConfigureAndPropagateNormalizedDepth()
		{
			this.normalizedHierarchyLevel = skeleton.GetNormalizedHierarchyLevel(hierarchyLevel);

			this.rotationSpringComponent.SetUnifiedForce(skeleton.GetForceByLevel(normalizedHierarchyLevel));
			this.rotationSpringComponent.SetUnifiedDrag(skeleton.GetDragByLevel(normalizedHierarchyLevel));

			this.inertiaEnabled = skeleton.motionInertiaEnabled;

			for (int i = 0; i < children.Count; i++)
			{
				children[i].ConfigureAndPropagateNormalizedDepth();
			}
		}

		public virtual void Initialize(Quaternion initialRotationParent)
        {
			deltaPosition = Vector3.zero;
			if (boneParent != null)
			{
				deltaPosition = transform.position - boneParent.transform.position;
			}

			initialRotation = transform.rotation;
			quatDelta = Quaternion.Inverse(initialRotationParent) * transform.rotation;
			lastPosition = transform.position;

			rotationSpringComponent.Initialize();
			rotationSpringComponent.SetTarget(initialRotation);
			rotationSpringComponent.ReachEquilibrium();

			for (int i = 0; i < children.Count; i++)
			{
				children[i].Initialize(initialRotation);
			}
		}

		public void Stop()
		{
			rotationSpringComponent.ReachEquilibrium();
			lastPosition = transform.position;

			for(int i = 0; i < children.Count; i++)
			{
				children[i].Stop();
			}
		}

		public void AddChild(SpringBone child)
		{
			children.Add(child);
			child.boneParent = this;
			child.skeleton = skeleton;

			child.ConfigureAndPropagateNormalizedDepth();
			child.Initialize(transform.rotation);
		}

		public void PropagateRotation(Quaternion parentRotation)
		{
			float maxInertia = skeleton.GetInertiaByHierarchyLevel(normalizedHierarchyLevel);

			Quaternion target = parentRotation * quatDelta;

			//Inertia
			if (inertiaEnabled)
			{
				Vector3 deltaPosition = transform.position - lastPosition;
				Vector3 deltaDir = deltaPosition.normalized;

				Vector3 orientationAxis = GetOrientationAxis();
				Vector3 rotationAxis = Vector3.Cross(deltaDir, orientationAxis);

				float angle = deltaPosition.magnitude * maxInertia;
				target = Quaternion.AngleAxis(angle, rotationAxis) * target;

				lastPosition = transform.position;
			}
			//

			rotationSpringComponent.SetTarget(target);

			for (int i = 0; i < children.Count; i++)
			{
				children[i].PropagateRotation(transform.rotation);
			}
		}

		private Vector3 GetOrientationAxis()
		{
			Vector3 res;
			switch (skeleton.boneOrientation)
			{
				case SpringSkeleton.BoneOrientation.FORWARD:
					res = transform.forward;
					break;
				case SpringSkeleton.BoneOrientation.BACK:
					res = -transform.forward;
					break;
				case SpringSkeleton.BoneOrientation.RIGHT:
					res = transform.right;
					break;
				case SpringSkeleton.BoneOrientation.LEFT:
					res = -transform.right;
					break;
				case SpringSkeleton.BoneOrientation.UP:
					res = transform.up;
					break;
				case SpringSkeleton.BoneOrientation.DOWN:
					res = -transform.up;
					break;
				default:
					res = transform.forward;
					break;
			}

			return res;
		}

		public void UpdateSprings()
		{
			rotationSpringComponent.LateUpdate();

			for(int i = 0; i < children.Count; i++)
			{
				children[i].UpdateSprings();
			}
		}

		public virtual void UpdateRotations()
		{
			transform.rotation = rotationSpringComponent.GetCurrentValue();

			for (int i = 0; i < children.Count; i++)
			{
				children[i].UpdateRotations();
			}
		}

		public void RemoveChild(SpringBone childBone)
		{
			children.Remove(childBone);
		}

		public void SetForce(float force)
		{
			rotationSpringComponent.SetUnifiedForce(force);

			for(int i = 0; i < children.Count; i++)
			{
				children[i].SetForce(force);
			}
		}

		public void SetDrag(float drag)
		{
			rotationSpringComponent.SetUnifiedDrag(drag);

			for (int i = 0; i < children.Count; i++)
			{
				children[i].SetDrag(drag);
			}
		}

#if UNITY_EDITOR
		public void AddChildEditor()
		{
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform trChild = transform.GetChild(i);
				SpringBone springBone = trChild.GetComponent<SpringBone>();

				if(springBone == null)
				{
					springBone = skeleton.CreateBoneByTransform(trChild, this);
					children.Add(springBone);
				}
			}

			ConfigureAndPropagateNormalizedDepth();

			skeleton.MarkSkeletonDirty();
		}

		public void RemoveFromSkeleton()
		{
			for (int i = 0; i < children.Count; i++)
			{
				children[i].boneParent = boneParent;
				boneParent.AddChildEditor(children[i]);
			}

			boneParent.RemoveChild(this);

			skeleton.ConfigureAndPropagateNormalizedDepth();
			skeleton.MarkSkeletonDirty();
		}

		public void AddChildEditor(SpringBone childBone)
		{
			children.Add(childBone);
		}

		public int FindChildIndexByTransform(Transform boneTransform)
		{
			int res = -1;

			for(int i = 0; i < children.Count; i++)
			{
				if(children[i].transform == boneTransform)
				{
					res = i;
					break;
				}
			}

			return res;
		}

		public static bool IsCandidate(Transform tr)
		{
			SpringBone springBone = tr.GetComponent<SpringBone>();

			bool res = springBone == null;
			return res;
		}

		public bool HasChildrenCandidates()
		{
			bool res = false;

			int childCount = transform.childCount;
			for(int i = 0; i < childCount; i++)
			{
				Transform trChild = transform.GetChild(i);
				if (SpringBone.IsCandidate(trChild))
				{
					res = true;
					break;
				}
			}

			return res;
		}

		public SpringBone ModifyChild(Transform oldChildTransform, Transform newChildTransform)
		{
			SpringBone removedBone = null;

			bool newChildIsCandidate = SpringBone.IsCandidate(newChildTransform);
			int oldChildIndex = FindChildIndexByTransform(oldChildTransform);

			if(newChildIsCandidate)
			{
				SpringBone oldChild = children[oldChildIndex];
				SpringBone newChildBone = skeleton.CreateBoneByTransform(newChildTransform, this);
				children[oldChildIndex] = newChildBone;

				removedBone = oldChild;
			}

			return removedBone;
		}

		public void DrawGizmosSelected(float boneGizmosSize)
		{
			if(children.Count <= 0) { return; }

			Vector3 startPosition = transform.position;
			Vector3 endPosition = transform.position + GetOrientationAxis() * boneGizmosSize;

			Handles.color = Color.blue;
			Handles.DrawLine(startPosition, endPosition);

			for (int i = 0; i < children.Count; i++)
			{
				children[i].DrawGizmosSelected(boneGizmosSize);
			}
		}
#endif
	}
}