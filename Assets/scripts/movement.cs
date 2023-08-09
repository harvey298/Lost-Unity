using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 2.0f;

    public bool doubleJump = false;
    public int jumps = 0;

    private float verticalRotation = 0f;
    private bool isMouseLocked = true;

    private Vector3 playerVelocity;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        LockCursor();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMouseLocked = !isMouseLocked;

            if (isMouseLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }

        if (isMouseLocked)
        {
            // Mouse rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the player horizontally (around Y-axis)
            transform.Rotate(Vector3.up * mouseX);

            // Calculate vertical rotation and clamp it to a range
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            // Rotate the camera vertically (around X-axis)
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection = moveDirection.normalized;

        { // Jump Logic
            if (controller.isGrounded) { jumps = 0; }

            if (controller.isGrounded || (doubleJump && jumps < 2))
            {
                // Jumping
                if (Input.GetButtonDown("Jump"))
                {
                    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                    jumps += 1;
                }
            }
        }

        // Apply movement
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}