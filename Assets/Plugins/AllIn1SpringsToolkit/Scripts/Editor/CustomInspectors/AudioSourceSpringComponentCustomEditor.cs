using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(AudioSourceSpringComponent))]
	[CanEditMultipleObjects]
	public class AudioSourceSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SerializedProperty spAutoUpdatedAudioSource;

		private SpringFloatDrawer volumeSpringDrawer;
		private SpringFloatDrawer pitchSpringDrawer;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spAutoUpdatedAudioSource = serializedObject.FindProperty("autoUpdatedAudioSource");
		}

		protected override void CreateDrawers()
		{
			volumeSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("volumeSpring"), false, false);
			pitchSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("pitchSpring"), false, false);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring("Target Volume", LabelWidth, volumeSpringDrawer);
			DrawCustomTargetBySpring("Target Pitch", LabelWidth, pitchSpringDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring("Initial Values Volume", LabelWidth, volumeSpringDrawer);
			DrawInitialValuesBySpring("Initial Values Pitch", LabelWidth, pitchSpringDrawer);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spAutoUpdatedAudioSource, LabelWidth);
		}

		protected override void DrawSprings()
		{
			DrawSpring(volumeSpringDrawer);
			DrawSpring(pitchSpringDrawer);
		}

		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);

			if (spAutoUpdatedAudioSource.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("Auto Updated Audio Source is not assigned!", MessageType.Error);
			}
		}
	}
}