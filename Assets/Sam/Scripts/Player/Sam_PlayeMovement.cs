using UnityEngine;

/// <summary>
/// Handles player movement and rotation based on camera direction.
/// Includes adjustable speed, jumping functionality, and smooth mouse look.
/// </summary>
public class Sam_PlayeMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f; // Player movement speed
    [SerializeField] private float jumpHeight = 2f; // Height of the player's jump
    [SerializeField] private float gravity = -9.81f; // Gravity applied to the player

    [Header("Mouse Look Settings")]
    [SerializeField] private float mouseSensitivity = 100f; // Sensitivity of the mouse movement
    [SerializeField] private Transform playerBody; // Player body for horizontal rotation
    [SerializeField] private Transform playerCamera; // Camera for vertical rotation

    [Header("References")]
    [SerializeField] private CharacterController controller; // Character controller for movement

    private Vector3 velocity; // Tracks player velocity
    private bool isGrounded; // Checks if the player is on the ground
    private float xRotation = 0f; // Tracks camera's vertical rotation

    private void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(transform.position, 0.4f, LayerMask.GetMask("Ground"));

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset velocity when grounded
        }

        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move relative to the camera direction
        Vector3 move = playerBody.right * x + playerBody.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera vertically (X-axis rotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp to prevent over-rotation
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player horizontally (Y-axis rotation)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
