using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraHandleNew : MonoBehaviour
{
    private float currentFOV;
    private float targetFOV;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] float minZoom = 30;
    [SerializeField] float maxZoom = 110;
    [SerializeField] float zoomAmount = 3f;
    [SerializeField] float zoomSpeed = 10f;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 60f;
    [SerializeField] float edgeScrollThreshold = 10f;

    [Header("Camera Bounds")]
    [SerializeField] Vector2 minPosition = new Vector2(8f, 3f);
    [SerializeField] Vector2 maxPosition = new Vector2(65f, 50f);
    [SerializeField] float boundsPaddingAtMinZoom = 35f;
    [SerializeField] float boundsPaddingAtMaxZoom = 15f;

    [Header("Edge Glow UI")]
    [SerializeField] CanvasGroup edgeGlowTop;
    [SerializeField] CanvasGroup edgeGlowBottom;
    [SerializeField] CanvasGroup edgeGlowLeft;
    [SerializeField] CanvasGroup edgeGlowRight;


    private void OnEnable()
    {
        BattleSceneActions.OnIntializationComplete += Animate;
    }


    private void OnDisable()
    {
        BattleSceneActions.OnIntializationComplete -= Animate;
    }
    private void Animate()
    {
        StartCoroutine(AnimateRoutine());

    }
    private IEnumerator AnimateRoutine()
    {
        yield return new WaitForSeconds(.5f);
        // Animate to the right and zoom in
        // Animate to the left and zoom out
        DOVirtual.Float(currentFOV, 110f, 1.5f, value =>
        {
            targetFOV = value;
        });

        transform.DOMoveX(5f, 3f).SetEase(Ease.InOutSine);
    }

    private void Start()
    {
        currentFOV = 40f; // Start zoomed in
        targetFOV = currentFOV;
        cinemachineVirtualCamera.m_Lens.FieldOfView = currentFOV;

        // Start position near the right
        transform.position = new Vector3(70f, 25f, transform.position.z);

        InitGlow(edgeGlowTop);
        InitGlow(edgeGlowBottom);
        InitGlow(edgeGlowLeft);
        InitGlow(edgeGlowRight);


    }

    private void Update()
    {
        HandleMovment();
        HandleZoom();
        HandleEdgeGlow();
    }

    private void HandleMovment()
    {
        Vector3 moveDir = Vector3.zero;

        // Keyboard Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDir += new Vector3(x, y, 0f).normalized;

        if (!DebuggerGlobal.instance.disableEdgeScrolling)
        {
            // Mouse Edge Scrolling
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x < edgeScrollThreshold) moveDir.x -= 1;
            if (mousePos.x > Screen.width - edgeScrollThreshold) moveDir.x += 1;
            if (mousePos.y < edgeScrollThreshold) moveDir.y -= 1;
            if (mousePos.y > Screen.height - edgeScrollThreshold) moveDir.y += 1;

        }


        // Move
        transform.position += moveDir.normalized * moveSpeed * Time.unscaledDeltaTime;

        // Dynamic bounds based on zoom
        float zoomT = Mathf.InverseLerp(minZoom, maxZoom, currentFOV);
        float padding = Mathf.Lerp(boundsPaddingAtMinZoom, boundsPaddingAtMaxZoom, zoomT);

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minPosition.x - padding, maxPosition.x + padding);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minPosition.y - padding, maxPosition.y + padding);
        transform.position = clampedPos;
    }

    private void HandleZoom()
    {
        targetFOV += -Input.mouseScrollDelta.y * zoomAmount;
        targetFOV = Mathf.Clamp(targetFOV, minZoom, maxZoom);
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.unscaledDeltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.FieldOfView = currentFOV;
    }

    private void HandleEdgeGlow()
    {
        Vector3 mousePos = Input.mousePosition;

        FadeGlow(edgeGlowLeft, mousePos.x < edgeScrollThreshold);
        FadeGlow(edgeGlowRight, mousePos.x > Screen.width - edgeScrollThreshold);
        FadeGlow(edgeGlowBottom, mousePos.y < edgeScrollThreshold);
        FadeGlow(edgeGlowTop, mousePos.y > Screen.height - edgeScrollThreshold);
    }

    private void InitGlow(CanvasGroup glow)
    {
        glow.alpha = 0f;
    }

    private void FadeGlow(CanvasGroup glow, bool shouldFadeIn)
    {
        glow.alpha = shouldFadeIn ? 1f : 0f;
    }
}
