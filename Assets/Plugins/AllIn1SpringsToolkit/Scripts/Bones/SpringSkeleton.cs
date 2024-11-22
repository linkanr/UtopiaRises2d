using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
#endif

namespace AllIn1SpringsToolkit.Bones
{
	[DisallowMultipleComponent]
	public class SpringSkeleton : MonoBehaviour
	{
		public enum BoneOrientation
		{
			FORWARD = 0,
			BACK = 1,
			RIGHT = 2,
			LEFT = 3,
			UP = 4,
			DOWN = 5,
		}

		public int skeletonDepth;

		public bool motionInertiaEnabled;
		public float minMotionInertia;
		public float maxMotionInertia;
		public float unifiedMotionInertiaValue;
		public AnimationCurve motionInertiaCurve;

		public float minForce;
		public float maxForce;
		public float unifiedForce;

		public float minDrag;
		public float maxDrag;
		public float unifiedDrag;

		public AnimationCurve forceAndDragCurve;

		[HideInInspector] public bool generalPropertiesUnfolded = true;
		[HideInInspector] public bool springSkeletonUnfolded = true;
		[HideInInspector] public bool initialValuesUnfolded;

		public bool autoInitialize;

		public Transform rootTransform;
		public SpringBone rootBone;


		public bool unifiedMotionInertia;
		public bool unifiedForceAndDrag;

		public BoneOrientation boneOrientation;

		private bool skeletonEnabled = true;
		private bool initialized;


#if UNITY_EDITOR
		[SerializeField] private bool gizmosEnabled;
		
		[SerializeField]
		[Min(0f)]
		private float boneGizmoSize = 0.25f;
#endif

		private void Start()
		{
			if (autoInitialize)
			{
				Initialize();
			}
		}

		public void Initialize()
		{
			if (!initialized)
			{
				rootBone.ConfigureAndPropagateNormalizedDepth();
				rootBone.Initialize(transform.rotation);
				rootBone.Stop();

				initialized = true;
			}
		}

		public void SetSkeletonEnabled(bool skeletonEnabled)
		{
			this.skeletonEnabled = skeletonEnabled;

			rootBone.Stop();
		}

		private void LateUpdate()
		{
			if (!initialized) { return; }

			if (skeletonEnabled)
			{
				rootBone.PropagateRotation(transform.rotation);
				rootBone.UpdateSprings();
				rootBone.UpdateRotations();
			}
		}

		public float GetForceByLevel(float normalizedHierarchyLevel)
		{
			float res = unifiedForce;

			if (!unifiedForceAndDrag)
			{
				float propagationValue = forceAndDragCurve.Evaluate(normalizedHierarchyLevel);
				res = Mathf.Lerp(minForce, maxForce, propagationValue);
			}

			return res;
		}

		public float GetDragByLevel(float normalizedHierarchyLevel)
		{
			float res = unifiedDrag;

			if (!unifiedForceAndDrag)
			{
				float propagationValue = forceAndDragCurve.Evaluate(normalizedHierarchyLevel);
				res = Mathf.Lerp(minDrag, maxDrag, propagationValue);
			}

			return res;
		}

		public float GetNormalizedHierarchyLevel(int hierarchyLevel)
		{
			float normalizedBoneDepth = (float)hierarchyLevel / skeletonDepth;
			return normalizedBoneDepth;
		}

		public float GetInertiaByHierarchyLevel(float normalizedHierarchyLevel)
		{
			float res = unifiedMotionInertiaValue;
			if (!unifiedMotionInertia)
			{
				res = Mathf.Lerp(minMotionInertia, maxMotionInertia, normalizedHierarchyLevel);
			}

			return res;
		}

		public void SetForce(float force)
		{
			rootBone.SetForce(force);
		}

		public void SetDrag(float drag)
		{
			rootBone.SetDrag(drag);
		}

#if UNITY_EDITOR

		public void Reset()
		{
			Setup();
		}

		[ContextMenu("Setup")]
		public void Setup()
		{
			SetupInitialValues();

			if (motionInertiaCurve == null)
			{
				motionInertiaCurve = new AnimationCurve();
				motionInertiaCurve.AddKey(0f, 0f);
				motionInertiaCurve.AddKey(1f, 1f);
			}

			if (forceAndDragCurve == null)
			{
				forceAndDragCurve = new AnimationCurve();
				forceAndDragCurve.AddKey(0f, 0f);
				forceAndDragCurve.AddKey(1f, 1f);
			}

			this.rootTransform = FindRootBone();
			CleanHierarchy();
			Create();
		}

		private void SetupInitialValues()
		{
			this.motionInertiaEnabled = true;
			this.autoInitialize = true;

			this.minMotionInertia = 20f;
			this.maxMotionInertia = 200f;
			this.unifiedMotionInertiaValue = 200f;
			this.minForce = 75f;
			this.maxForce = 100f;
			this.unifiedForce = 100f;

			this.minDrag = 5f;
			this.maxDrag = 10f;
			this.unifiedDrag = 10f;

			this.unifiedMotionInertia = true;
			this.unifiedForceAndDrag = true;
		}

		public void MarkSkeletonDirty()
		{
			PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
			if (prefabStage != null)
			{
				EditorSceneManager.MarkSceneDirty(prefabStage.scene);
			}
			else
			{
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
		}

		private void OnValidate()
		{
			PropagateValues();
		}

		public void PropagateValues()
		{
			rootBone.ConfigureAndPropagateNormalizedDepth();
		}

		private SpringBone CreateBoneHierarchy(Transform boneTransform, SpringBone parentBone)
		{
			RotationSpringComponent boneTransformSpringComponent = boneTransform.gameObject.AddComponent<RotationSpringComponent>();
			boneTransformSpringComponent.doesAutoInitialize = false;
			boneTransformSpringComponent.enabled = false;

			SpringBone bone = boneTransform.gameObject.AddComponent<SpringBone>();
			bone.boneParent = parentBone;
			bone.rotationSpringComponent = boneTransformSpringComponent;
			bone.skeleton = this;
			bone.children = new List<SpringBone>();

			int boneHierarchyLevel = 0;
			CalculateHierarchyLevel(boneTransform, ref boneHierarchyLevel);
			bone.hierarchyLevel = boneHierarchyLevel;

			if (boneTransform.childCount > 0)
			{
				SpringBone childBone = CreateBoneHierarchy(boneTransform.GetChild(0), bone);
				bone.AddChildEditor(childBone);
			}

			return bone;
		}

		public SpringBone CreateBoneByTransform(Transform tr, SpringBone parentBone)
		{
			RotationSpringComponent boneTransformSpringComponent = tr.gameObject.AddComponent<RotationSpringComponent>();
			boneTransformSpringComponent.doesAutoInitialize = false;
			boneTransformSpringComponent.enabled = false;

			SpringBone bone = tr.gameObject.AddComponent<SpringBone>();
			bone.boneParent = parentBone;
			bone.rotationSpringComponent = boneTransformSpringComponent;
			bone.skeleton = this;
			bone.children = new List<SpringBone>();
			bone.hierarchyLevel = parentBone.hierarchyLevel + 1;

			return bone;
		}

		private void GetSkeletonDepth(SpringBone bone, ref int sekeletonDepth)
		{
			if (bone.hierarchyLevel >= sekeletonDepth)
			{
				sekeletonDepth = bone.hierarchyLevel;
			}

			for (int i = 0; i < bone.children.Count; i++)
			{
				GetSkeletonDepth(bone.children[i], ref sekeletonDepth);
			}
		}

		private void Create()
		{
			if (rootTransform == null)
			{
				Debug.Log("Root Bone not found!");
				return;
			}

			rootBone = CreateBoneHierarchy(rootTransform, null);

			int newSkeletonDepth = -1;
			GetSkeletonDepth(rootBone, ref newSkeletonDepth);
			this.skeletonDepth = newSkeletonDepth;

			ConfigureAndPropagateNormalizedDepth();
		}

		public void ConfigureAndPropagateNormalizedDepth()
		{
			rootBone.ConfigureAndPropagateNormalizedDepth();
		}

		private Transform FindRootBone()
		{
			Transform res = null;

			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform trChild = transform.GetChild(i);
				if (trChild.childCount > 0)
				{
					res = trChild;
					break;
				}
			}

			if(res == null)
			{
				res = transform.GetChild(0);
			}

			return res;
		}

		private void CalculateHierarchyLevel(Transform tr, ref int hierarchyLevel)
		{
			if (tr != rootTransform)
			{
				hierarchyLevel++; 
				CalculateHierarchyLevel(tr.parent, ref hierarchyLevel);
			}
		}

		private void CleanHierarchy()
		{
			SpringComponent[] springComponents = rootTransform.GetComponentsInChildren<SpringComponent>(true);
			SpringBone[] springBonesComponents = rootTransform.GetComponentsInChildren<SpringBone>(true);

			for (int i = 0; i < springComponents.Length; i++)
			{
				DestroyImmediate(springComponents[i]);
			}

			for (int i = 0; i < springBonesComponents.Length; i++)
			{
				DestroyImmediate(springBonesComponents[i]);
			}

			MarkSkeletonDirty();
		}

		public void Clean()
		{
			CleanHierarchy();

			DestroyImmediate(this);
		}

		public void ModifyRoot(Transform newRoot)
		{
			CleanHierarchy();

			this.rootTransform = newRoot;
			CleanHierarchy();
			Create();
		}

		private void OnDrawGizmosSelected()
		{
			if (!gizmosEnabled) { return; }
			if(rootBone == null) { return; }

			rootBone.DrawGizmosSelected(boneGizmoSize);
		}
#endif
	}
}