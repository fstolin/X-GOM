using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 100f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float cameraPanSpeed = 5f;
    [SerializeField] private float zoomUpperBounds = 8f;
    [SerializeField] private float zoomLowerBounds = 1f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private Vector3 followOffset;

    private void Awake()
    {
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom2();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputMoveDirection *= 2;
        }
        if (Input.GetMouseButton(0))
        {
            inputMoveDirection += new Vector3(-Input.GetAxis("Mouse X"), 0f, -Input.GetAxis("Mouse Y")) * cameraPanSpeed;
            
        }
        transform.Translate(inputMoveDirection * Time.deltaTime * cameraMoveSpeed);
    }

    private void HandleRotation()
    {

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, cameraRotationSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -cameraRotationSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetMouseButton(2))
        {
            transform.Rotate(0f, 3*cameraRotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"), 0f);
        }
    }

    private void HandleZoom2()
    {

        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0) targetFollowOffset.y -= zoomAmount;
        if (Input.mouseScrollDelta.y < 0) targetFollowOffset.y += zoomAmount;

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, zoomLowerBounds, zoomUpperBounds);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
