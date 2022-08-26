using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float mouseSens = 2f;
    private float walkSpeed = 5f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        MouseMovement();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void MouseMovement()
    {
        xRotation -= Input.GetAxisRaw("Mouse Y") * mouseSens;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += Input.GetAxisRaw("Mouse X") * mouseSens;


        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Movement()
    {
        Vector3 axis = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, Input.GetAxis("Vertical") * walkSpeed);

        Vector3 forwardDirection = Quaternion.Euler(0, yRotation, 0) * Vector3.forward;
        Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * forwardDirection;
        Vector3 wishDirection = (forwardDirection * axis.y + rightDirection * axis.x);

        rb.velocity = wishDirection;
    }
}
