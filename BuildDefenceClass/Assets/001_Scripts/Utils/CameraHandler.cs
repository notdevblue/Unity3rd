using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vCam;

    [SerializeField] private float maxFoV = 15.0f;
    [SerializeField] private float minFoV = 8.5f;
    [SerializeField] private float zoomSpeed = 5.0f;
    private float targetSize = 0.0f;
    private float targetCamSize = 0.0f;

    private void Start()
    {
        targetCamSize = vCam.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        // Move Funciton Call
        HandleMovement();
        // Zoom Function Call
        HandleZoom();

    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        // Camera Movement
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30.0f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float mouseWheelDelta = Input.mouseScrollDelta.y * zoomSpeed;

        targetSize = mouseWheelDelta + targetCamSize;
        targetSize = Mathf.Clamp(targetSize, minFoV, maxFoV);

        vCam.m_Lens.OrthographicSize = Mathf.Lerp(targetCamSize, targetSize, Time.deltaTime * zoomSpeed);
    }
}
