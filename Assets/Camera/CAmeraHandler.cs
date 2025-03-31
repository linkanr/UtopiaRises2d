using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CAmeraHandler : MonoBehaviour
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

    [Header("Edge Glow UI")]
    [SerializeField] CanvasGroup edgeGlowTop;
    [SerializeField] CanvasGroup edgeGlowBottom;
    [SerializeField] CanvasGroup edgeGlowLeft;
    [SerializeField] CanvasGroup edgeGlowRight;
    [SerializeField] float glowFadeDuration = 0.2f;

    private void Start()
    {
        currentFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
        targetFOV = currentFOV;

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

        // Mouse Edge Scrolling
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x < edgeScrollThreshold) moveDir.x -= 1;
        if (mousePos.x > Screen.width - edgeScrollThreshold) moveDir.x += 1;
        if (mousePos.y < edgeScrollThreshold) moveDir.y -= 1;
        if (mousePos.y > Screen.height - edgeScrollThreshold) moveDir.y += 1;

        // Move and Clamp
        transform.position += moveDir.normalized * moveSpeed * Time.unscaledDeltaTime;
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minPosition.x, maxPosition.x);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minPosition.y, maxPosition.y);
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
        glow.DOFade(0f, 0f);
    }

    private void FadeGlow(CanvasGroup glow, bool shouldFadeIn)
    {
        float targetAlpha = shouldFadeIn ? 1f : 0f;
        glow.DOFade(targetAlpha, glowFadeDuration).SetEase(Ease.InOutSine);
    }
}
