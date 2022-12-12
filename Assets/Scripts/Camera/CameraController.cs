using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 100f;

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleRotation();
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
            transform.Rotate(0f, cameraRotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"), 0f);
        }
    }
}
