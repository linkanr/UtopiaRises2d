using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Cam Fov Or Size Spring Component")]
	public partial class CamFovOrSizeSpringComponent : SpringComponent
	{
		[SerializeField] private SpringFloat fovSpring = new SpringFloat();

		[SerializeField] private Camera autoUpdatedCamera;

		protected override void RegisterSprings()
		{
			RegisterSpring(fovSpring);
		}
		
		public override void Initialize()
		{
			FindCamera();
			base.Initialize();
		}

		public void Update()
        {
			if (!initialized) { return; }

			UpdateCamera();
		}

		private void UpdateCamera()
		{
			if (autoUpdatedCamera.orthographic)
			{
				autoUpdatedCamera.orthographicSize = fovSpring.GetCurrentValue();
			}
			else
			{
				autoUpdatedCamera.fieldOfView = fovSpring.GetCurrentValue();
			}
		}

		public override bool IsValidSpringComponent()
		{
			if(autoUpdatedCamera == null)
			{
				AddErrorReason($"No autoUpdatedCamera found from {gameObject.name}. " +
				               $"We looked for Camera.main and then for any Camera in the scene.");
				return false;
			}

			return true;
		}
		
		protected override void SetCurrentValueByDefault()
		{
			float cameraFov = GetCameraFovOrSize();
			SetCurrentValue(cameraFov);
		}

		protected override void SetTargetByDefault()
		{
			float cameraFov = GetCameraFovOrSize();
			SetTarget(cameraFov);
		}

		private float GetCameraFovOrSize()
		{
			float res = autoUpdatedCamera.orthographic ? autoUpdatedCamera.orthographicSize : autoUpdatedCamera.fieldOfView;
			return res;
		}

		private void FindCamera()
		{
			if (autoUpdatedCamera == null)
			{
				autoUpdatedCamera = Camera.main;

				if (autoUpdatedCamera == null)
				{
					autoUpdatedCamera = FindObjectOfType<Camera>();
				}
			}
		}

		public Camera GetAutoUpdatedCamera()
		{
			return autoUpdatedCamera;
		}
		
		public void SetAutoUpdatedCamera(Camera newAutoUpdatedCamera)
		{
			autoUpdatedCamera = newAutoUpdatedCamera;
		}
		
#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			if (!EditorApplication.isPlayingOrWillChangePlaymode && !PrefabUtility.IsPartOfPrefabAsset(this))
			{
				FindCamera();
			}
		}
#endif
	}
}