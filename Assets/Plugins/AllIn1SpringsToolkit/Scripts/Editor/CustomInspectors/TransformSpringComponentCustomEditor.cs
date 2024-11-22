using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(TransformSpringComponent))]
	[CanEditMultipleObjects]
	public class TransformSpringComponentCustomEditor : SpringComponentCustomEditor 
	{
		private SerializedProperty spFollowerTransform;
		
		private SerializedProperty spUseTransformAsTarget;
		private SerializedProperty spTargetTransform;

		private SerializedProperty spPositionSpring;
		private SerializedProperty spRotationSpring;
		private SerializedProperty spScaleSpring;

		private SerializedProperty spSpaceType;

		private SpringVector3Drawer springPositionDrawer;
		private SpringRotationDrawer springRotationDrawer;
		private SpringVector3Drawer springScaleDrawer;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spPositionSpring = serializedObject.FindProperty("positionSpring");
			spRotationSpring = serializedObject.FindProperty("rotationSpring");
			spScaleSpring = serializedObject.FindProperty("scaleSpring");

			spFollowerTransform = serializedObject.FindProperty("followerTransform");
			spUseTransformAsTarget = serializedObject.FindProperty("useTransformAsTarget");
			spTargetTransform = serializedObject.FindProperty("targetTransform");

			spSpaceType = serializedObject.FindProperty("spaceType");
		}

		protected override void CreateDrawers()
		{
			springPositionDrawer = new SpringVector3Drawer(spPositionSpring, false, false);
			springRotationDrawer = new SpringRotationDrawer(spRotationSpring, false, false);
			springScaleDrawer = new SpringVector3Drawer(spScaleSpring, false, false);
		}

		protected override void DrawSprings()
		{
			DrawSpring("Position Spring", spPositionSpring, springPositionDrawer);
			DrawSpring("Rotation Spring", spRotationSpring, springRotationDrawer);
			DrawSpring("Scale Spring", spScaleSpring, springScaleDrawer);
		}
		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spFollowerTransform, LabelWidth);

			DrawSerializedProperty(spUseTransformAsTarget, LabelWidth);

			if (spUseTransformAsTarget.boolValue)
			{
				DrawSerializedProperty(spTargetTransform, LabelWidth);
			}

			DrawSerializedProperty(spSpaceType, LabelWidth);
		}

		private void DrawSpring(string springName, SerializedProperty spSpring, SpringDrawer springDrawer)
		{
			springDrawer.springEditorObject.Unfolded = DrawRectangleArea(areaName: springName, springDrawer.springEditorObject.spSpringEnabled, springDrawer.springEditorObject.Unfolded);

			if (springDrawer.springEditorObject.Unfolded)
			{
				springDrawer.SetParentProperty(spSpring);
				Rect propertyRect = EditorGUILayout.GetControlRect(hasLabel: false, height: springDrawer.GetPropertyHeight());
				springDrawer.OnGUI(propertyRect, spSpring, GUIContent.none);
			}
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelUseInitialValues: "Use Initial Position", 
				labelInitialValues: "Initial Position",
				width: LabelWidth,
				springEnabled: springPositionDrawer.springEditorObject.spSpringEnabled.boolValue, 
				springDrawer: springPositionDrawer);

			DrawInitialValuesBySpring(
				labelUseInitialValues: "Use Initial Rotation",
				labelInitialValues: "Initial Rotation",
				width: LabelWidth,
				springEnabled: springRotationDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springRotationDrawer);

			DrawInitialValuesBySpring(
				labelUseInitialValues: "Use Initial Scale",
				labelInitialValues: "Initial Scale",
				width: LabelWidth,
				springEnabled: springScaleDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springScaleDrawer);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelUseCustomTarget: "Use Custom Target Position",
				labelCustomTarget: "Target Position",
				width: LabelWidth,
				springEnabled: springPositionDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springPositionDrawer);

			DrawCustomTargetBySpring(
				labelUseCustomTarget: "Use Custom Target Rotation",
				labelCustomTarget: "Target Rotation",
				width: LabelWidth,
				springEnabled: springRotationDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springRotationDrawer);

			DrawCustomTargetBySpring(
				labelUseCustomTarget: "Use Custom Target Scale",
				labelCustomTarget: "Target Scale",
				width: LabelWidth,
				springEnabled: springScaleDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springScaleDrawer);

			if (spUseTransformAsTarget.boolValue)
			{
				SpringsEditorUtility.Space(2);
				EditorGUILayout.HelpBox("Custom Target won't have any effect becouse you're using a transform as a target", MessageType.Warning);
			}
		}

		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);
			
			if(spFollowerTransform.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("Follower is not assigned!", MessageType.Error);
			}
			if(spUseTransformAsTarget.boolValue && spHasCustomTarget.boolValue)
			{
				EditorGUILayout.HelpBox("You are using a transform as a target and custom initial target at the same time. Disable one of them", MessageType.Warning);
			}
			if (spUseTransformAsTarget.boolValue && spTargetTransform.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("Use Transform As Target is enabled but the reference is not assigned", MessageType.Warning);
			}
		}
	}
}