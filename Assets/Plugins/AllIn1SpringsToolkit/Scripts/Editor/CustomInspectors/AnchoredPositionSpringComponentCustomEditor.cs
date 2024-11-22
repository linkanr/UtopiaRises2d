using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(AnchoredPositionSpringComponent))]
	[CanEditMultipleObjects]
	public class AnchoredPositionSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringVector2Drawer anchoredPositionSpringDrawer;

		private SerializedProperty spFollowRectTransform;
		private SerializedProperty spTargetRectTransform;
		private SerializedProperty spUseTransformAsTarget;

		protected override void CreateDrawers()
		{
			anchoredPositionSpringDrawer = new SpringVector2Drawer(serializedObject.FindProperty("anchoredPositionSpring"), false, false);
		}

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spFollowRectTransform = serializedObject.FindProperty("followRectTransform");
			spTargetRectTransform = serializedObject.FindProperty("targetRectTransform");
			spUseTransformAsTarget = serializedObject.FindProperty("useTransformAsTarget");
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring("Target", LabelWidth, anchoredPositionSpringDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring("Initial Values", LabelWidth, anchoredPositionSpringDrawer);
		}

		protected override void DrawSprings()
		{
			DrawSpring(anchoredPositionSpringDrawer);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spFollowRectTransform, LabelWidth);

			SpringsEditorUtility.Space();

			DrawSerializedProperty(spUseTransformAsTarget, LabelWidth);
			if (spUseTransformAsTarget.boolValue)
			{
				DrawSerializedProperty(spTargetRectTransform, LabelWidth);
			}

			SpringsEditorUtility.Space();
		}

		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);

			if (spFollowRectTransform.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("Follower is not assigned!", MessageType.Error);
			}
			if (spUseTransformAsTarget.boolValue && spHasCustomTarget.boolValue)
			{
				EditorGUILayout.HelpBox("You are using a rect transform as a target and custom initial target at the same time. Disable one of them", MessageType.Warning);
			}
			if (spUseTransformAsTarget.boolValue && spTargetRectTransform.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("Use Transform As Target is enabled but the reference is not assigned", MessageType.Warning);
			}
		}
	}
}