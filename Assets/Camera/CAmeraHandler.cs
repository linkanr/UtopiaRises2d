using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmeraHandler : MonoBehaviour
{
    private float currentFOV;
    private float targetFOV;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] float minZoom = 20;
    [SerializeField] float maxZoom = 80;
    [SerializeField] float zoomAmount = 1f;
    [SerializeField] float zoomSpeed = 4f;
    private void Start()
    {
        currentFOV = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetFOV = currentFOV;
    }
    void Update()
    {
        HandleMovment();
        HandleZoom();

  

    

    }
    private void HandleMovment()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, y, 0f).normalized;
        float moveSpeed = 60f;
        transform.position += moveDir * moveSpeed * Time.unscaledDeltaTime;
    }
    private void HandleZoom()
    {
        

        targetFOV += -Input.mouseScrollDelta.y * zoomAmount;
        

        targetFOV = Mathf.Clamp(targetFOV, minZoom, maxZoom);
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.unscaledDeltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = currentFOV * zoomAmount;
    }
}
