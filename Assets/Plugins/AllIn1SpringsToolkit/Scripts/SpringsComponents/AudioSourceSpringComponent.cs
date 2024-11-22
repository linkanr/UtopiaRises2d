using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Audio Source Spring Component")]
	public partial class AudioSourceSpringComponent : SpringComponent
	{
        [SerializeField] private AudioSource autoUpdatedAudioSource;

		[SerializeField] private SpringFloat volumeSpring = new SpringFloat();
		[SerializeField] private SpringFloat pitchSpring = new SpringFloat();

		protected override void RegisterSprings()
		{
			RegisterSpring(volumeSpring);
			RegisterSpring(pitchSpring);
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValueVolume(autoUpdatedAudioSource.volume);
			SetCurrentValuePitch(autoUpdatedAudioSource.pitch);
		}

		protected override void SetTargetByDefault()
		{
			SetTargetVolume(autoUpdatedAudioSource.volume);
			SetTargetPitch(autoUpdatedAudioSource.pitch);
		}

		public void Update()
		{
			if (!initialized) { return; }

			UpdateAudioSource();
		}

		private void UpdateAudioSource()
		{
			autoUpdatedAudioSource.volume = volumeSpring.GetCurrentValue();
			autoUpdatedAudioSource.pitch = pitchSpring.GetCurrentValue();
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (autoUpdatedAudioSource == null)
			{
				AddErrorReason($"{gameObject.name} autoUpdatedAudioSource is null.");
				res = false;
			}

			return res;
		}
		
		#region ENABLE/DISABLE SPRING PROPERTIES
		public SpringFloat VolumeSpring
		{
			get => volumeSpring;
			set => volumeSpring = value;
		}

		public SpringFloat PitchSpring
		{
			get => pitchSpring;
			set => pitchSpring = value;
		}
		#endregion

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			if(autoUpdatedAudioSource == null)
			{
				autoUpdatedAudioSource = GetComponent<AudioSource>();
			}
		}
#endif
	}
}
