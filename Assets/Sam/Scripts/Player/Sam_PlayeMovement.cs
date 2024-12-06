using UnityEngine;

public class Sam_PlayeMovement : MonoBehaviour
{
    public float speed = 12f; // Movement speed
    public float jumpHeight = 2f; // Jump height
    public float gravity = -20f; // Gravity force
    public float mouseSensitivity = 2f; // Mouse sensitivity for rotation
    public float turnSmoothing = 10f; // Smoothing factor for turning

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField] private Transform cameraTransform; // Reference to the player's camera

    private float pitch = 0f; // Vertical camera rotation
    private float targetYaw; // Target horizontal rotation for smoothing
    private float targetPitch; // Target vertical rotation for smoothing

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned! Please assign a reference to the camera.");
        }

        // Lock the cursor to the game screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize target rotations
        targetYaw = transform.eulerAngles.y;
        targetPitch = cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        HandleMouseLook(); // Rotate the camera and player
        HandleMovement();  // Handle movement based on camera direction
    }

    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Update target rotation values
        targetYaw += mouseX;
        targetPitch -= mouseY;
        targetPitch = Mathf.Clamp(targetPitch, -90f, 90f); // Clamp vertical rotation to avoid flipping

        // Smoothly interpolate the current rotation to the target rotation
        float smoothedYaw = Mathf.LerpAngle(transform.eulerAngles.y, targetYaw, turnSmoothing * Time.deltaTime);
        float smoothedPitch = Mathf.LerpAngle(cameraTransform.localEulerAngles.x, targetPitch, turnSmoothing * Time.deltaTime);

        // Apply smoothed rotations
        transform.rotation = Quaternion.Euler(0f, smoothedYaw, 0f); // Rotate player horizontally
        cameraTransform.localRotation = Quaternion.Euler(smoothedPitch, 0f, 0f); // Rotate camera vertically
    }

    private void HandleMovement()
    {
        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset vertical velocity when grounded
        }

        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Calculate movement direction relative to the player's forward
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        // Apply movement
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical velocity
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        // Re-lock the cursor when the game regains focus
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
