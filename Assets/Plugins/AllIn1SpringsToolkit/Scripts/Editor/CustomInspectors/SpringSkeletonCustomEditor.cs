using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit.Bones
{
	[CustomEditor(typeof(SpringSkeleton))]
	public class SpringSkeletonCustomEditor : Editor
	{
		private SerializedProperty spSpringSkeletonUnfolded;
		private SerializedProperty spGeneralPropertiesUnfolded;
		private SerializedProperty spInitialValuesUnfolded;

		private SerializedProperty spAutoInitialize;

		private SerializedProperty spRootTransform;
		private SerializedProperty spRootBone;

		private SerializedProperty spInertiaEnabled;
		private SerializedProperty spMinInertia;
		private SerializedProperty spMaxIntertia;
		private SerializedProperty spInertiaCurve;

		private SerializedProperty spUnifiedForceAndDrag;

		private SerializedProperty spUnifiedForce;
		private SerializedProperty spMinForce;
		private SerializedProperty spMaxForce;

		private SerializedProperty spUnifiedDrag;
		private SerializedProperty spMinDrag;
		private SerializedProperty spMaxDrag;
		private SerializedProperty spForceAndDragCurve;

		private SerializedProperty spPropagationCurve;

		private SerializedProperty spTransformSpringComponent;

		private SerializedProperty spUnifiedInertia;
		private SerializedProperty spUnifiedInertiaValue;

		private SerializedProperty spBoneOrientation;

		private SerializedProperty spGizmosEnabled;
		private SerializedProperty spBoneGizmoSize;


		private SpringSkeleton springSkeleton;

		private List<SpringBone> bonesToClean;
		private bool cleanSkeleton;
		private bool rootModified;
		private Transform newRootTransform;

		//Styles
		protected GUIStyle guiStyleLabelTitle;

		protected void RefreshSerializedProperties()
		{
			spSpringSkeletonUnfolded = serializedObject.FindProperty("springSkeletonUnfolded");
			spGeneralPropertiesUnfolded = serializedObject.FindProperty("generalPropertiesUnfolded");
			spInitialValuesUnfolded = serializedObject.FindProperty("initialValuesUnfolded");

			spAutoInitialize = serializedObject.FindProperty("autoInitialize");

			spRootTransform = serializedObject.FindProperty("rootTransform");
			spRootBone = serializedObject.FindProperty("rootBone");

			spInertiaEnabled = serializedObject.FindProperty("motionInertiaEnabled");
			spUnifiedInertia = serializedObject.FindProperty("unifiedMotionInertia");
			spUnifiedInertiaValue = serializedObject.FindProperty("unifiedMotionInertiaValue");
			spMinInertia = serializedObject.FindProperty("minMotionInertia");
			spMaxIntertia = serializedObject.FindProperty("maxMotionInertia");
			spInertiaCurve = serializedObject.FindProperty("motionInertiaCurve");

			spUnifiedForceAndDrag = serializedObject.FindProperty("unifiedForceAndDrag");

			spUnifiedForce = serializedObject.FindProperty("unifiedForce");
			spMinForce = serializedObject.FindProperty("minForce");
			spMaxForce = serializedObject.FindProperty("maxForce");

			spUnifiedDrag = serializedObject.FindProperty("unifiedDrag");
			spMinDrag = serializedObject.FindProperty("minDrag");
			spMaxDrag = serializedObject.FindProperty("maxDrag");
			spForceAndDragCurve = serializedObject.FindProperty("forceAndDragCurve");

			spPropagationCurve = serializedObject.FindProperty("propagationCurve");

			spTransformSpringComponent = serializedObject.FindProperty("transformSpringComponent");

			spBoneOrientation = serializedObject.FindProperty("boneOrientation");

			spGizmosEnabled = serializedObject.FindProperty("gizmosEnabled");
			spBoneGizmoSize = serializedObject.FindProperty("boneGizmoSize");
		}

		protected void RefreshStyles()
		{
			guiStyleLabelTitle = new GUIStyle(EditorStyles.boldLabel);
			guiStyleLabelTitle.normal.textColor = Color.white;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			springSkeleton = (SpringSkeleton)target;

			RefreshSerializedProperties();
			RefreshStyles();

			if (bonesToClean == null)
			{
				bonesToClean = new List<SpringBone>();
			}

			DrawHierarchyZone();

			spGeneralPropertiesUnfolded.boolValue = AllIn1SpringsEditorUtility.DrawRectangleArea("General Properties", spGeneralPropertiesUnfolded.boolValue, guiStyleLabelTitle, serializedObject);
			if (spGeneralPropertiesUnfolded.boolValue)
			{
				DrawGeneralProperties();
			}

			serializedObject.ApplyModifiedProperties();
			

			if (rootModified)
			{
				springSkeleton.ModifyRoot(newRootTransform);

				rootModified = false;
				newRootTransform = null;
			}

			serializedObject.ApplyModifiedProperties();


			if (cleanSkeleton)
			{
				springSkeleton.Clean();
				cleanSkeleton = false;
			}
		}

		private void DrawSerializedProperty(SerializedProperty serializedProperty)
		{
			Rect rect = EditorGUILayout.GetControlRect();
			EditorGUI.PropertyField(rect, serializedProperty, new GUIContent(serializedProperty.displayName, string.Empty));
		}

		private void DrawSerializedProperty(SerializedProperty serializedProperty, string tooltip)
		{
			Rect rect = EditorGUILayout.GetControlRect();
			EditorGUI.PropertyField(rect, serializedProperty, new GUIContent(serializedProperty.displayName, tooltip));
		}

		private void DrawHierarchyZone()
		{
			spSpringSkeletonUnfolded.boolValue = AllIn1SpringsEditorUtility.DrawRectangleArea("Skeleton Hierarchy", spSpringSkeletonUnfolded.boolValue, guiStyleLabelTitle, serializedObject);
			if (spSpringSkeletonUnfolded.boolValue)
			{
				DrawHierarchy();
			}
		}

		private void DrawGeneralProperties()
		{
			DrawSerializedProperty(spInertiaEnabled);
			if (spInertiaEnabled.boolValue)
			{
				DrawSerializedProperty(spBoneOrientation, SpringSkeletonUtility.TOOLTIP_BONE_ORIENTATION);
				DrawSerializedProperty(spUnifiedInertia);
				if (spUnifiedInertia.boolValue)
				{
					DrawSerializedProperty(spUnifiedInertiaValue);
				}
				else
				{
					DrawSerializedProperty(spMinInertia);
					DrawSerializedProperty(spMaxIntertia);
					DrawSerializedProperty(spInertiaCurve);
				}
			}

			Space(2);

			DrawSerializedProperty(spUnifiedForceAndDrag);

			if (spUnifiedForceAndDrag.boolValue)
			{
				DrawSerializedProperty(spUnifiedForce);
				DrawSerializedProperty(spUnifiedDrag);
			}
			else
			{
				DrawSerializedProperty(spMinForce);
				DrawSerializedProperty(spMaxForce);

				Space();

				DrawSerializedProperty(spMinDrag);
				DrawSerializedProperty(spMaxDrag);

				Space();

				DrawSerializedProperty(spForceAndDragCurve);
			}

			Space();

			DrawSerializedProperty(spAutoInitialize);

			Space(2);

			DrawSerializedProperty(spGizmosEnabled);
			if (spGizmosEnabled.boolValue)
			{
				DrawSerializedProperty(spBoneGizmoSize);
			}

			Space(2);

			if (GUILayout.Button("Redo Configuration"))
			{
				bool dialogResult = EditorUtility.DisplayDialog("Do you want to COMPLETELY reset the skeleton?", "The current configuration will be lost", "Yes", "Back");
				if (dialogResult)
				{
					ConfigureSkeleton();
				}
			}

			if (GUILayout.Button("Clean"))
			{
				bool dialogResult = EditorUtility.DisplayDialog("Do you want to clean the skeleton?", "All the components in the hierarchy will be removed", "Remove all", "Back");
				cleanSkeleton = dialogResult;
			}
		}

		private void DrawHierarchy()
		{
			Space();

			DrawHierarchyRecursively(springSkeleton.rootBone);

			Space();
		}

		private void DrawHierarchyRecursively(SpringBone springBone)
		{
			EditorGUILayout.BeginHorizontal();
			Rect rect = EditorGUILayout.GetControlRect();

			float displacement = springBone.hierarchyLevel * 5f;
			rect.x += displacement;
			rect.width -= displacement;

			EditorGUIUtility.labelWidth = 50f;

			string label = string.Empty;
			if (springBone.hierarchyLevel == 0)
			{
				Rect rectBackground = new Rect(rect.position, new Vector2(EditorGUIUtility.currentViewWidth, rect.height));

				float extrHeight = rectBackground.height * 0.2f;
				rectBackground.height += extrHeight;
				rectBackground.y -= extrHeight * 0.5f;

				Color backgroundColor = SpringsEditorUtility.ASSET_ICON_COLOR;
				backgroundColor.a = 0.5f;
				EditorGUI.DrawRect(rectBackground, backgroundColor);

				label = "Root";
			}

			EditorGUI.BeginChangeCheck();
			Transform newBoneTransform = (Transform)EditorGUI.ObjectField(rect, label, springBone.transform, typeof(Transform), true);
			if (EditorGUI.EndChangeCheck())
			{
				if (springBone.IsRoot)
				{
					rootModified = true;
					newRootTransform = newBoneTransform;
				}
				else
				{
					SpringBone oldBone = springBone.boneParent.ModifyChild(springBone.transform, newBoneTransform);
					if (oldBone != null)
					{
						bonesToClean.Add(oldBone);
					}
				}
			}

			EditorGUI.BeginDisabledGroup(springBone.IsRoot);
			if (GUILayout.Button("-", GUILayout.MaxWidth(50f)))
			{
				springBone.RemoveFromSkeleton();
				bonesToClean.Add(springBone);
			}
			EditorGUI.EndDisabledGroup();

			bool hasChildrenCandidates = springBone.HasChildrenCandidates();
			EditorGUI.BeginDisabledGroup(!hasChildrenCandidates);
			if (GUILayout.Button("+", GUILayout.MaxWidth(50f)))
			{
				springBone.AddChildEditor();
			}
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.EndHorizontal();


			if(springBone.hierarchyLevel == 0)
			{
				Space(2);
			}

			for (int i = 0; i < springBone.children.Count; i++)
			{
				DrawHierarchyRecursively(springBone.children[i]);
			}

			ProcessBonesToClean();
		}

		private void ProcessBonesToClean()
		{
			for(int i = 0; i < bonesToClean.Count; i++)
			{
				SpringBone bone = bonesToClean[i];
				DestroyImmediate(bone.rotationSpringComponent);
				DestroyImmediate(bone);
			}

			bonesToClean.Clear();
		}

		private void ApplyValues()
		{
			SpringSkeleton boneSkeleton = (SpringSkeleton)target;
			boneSkeleton.PropagateValues();
		}

		private void ConfigureSkeleton()
		{
			SpringSkeleton boneSkeleton = (SpringSkeleton)target;
			boneSkeleton.Setup();
		}

		private void Space()
		{
			Space(1);
		}

		private void Space(int numSpace)
		{
			for (int i = 0; i < numSpace; i++)
			{
				EditorGUILayout.Space();
			}
		}
	}
}