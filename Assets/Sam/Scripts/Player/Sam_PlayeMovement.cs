/// <summary>
/// Handles player movement and rotation based on camera direction.
/// Includes adjustable speed and jumping functionality.
/// Not going to be used this was just for my testing, someone else is doing this for player
/// </summary>
using UnityEngine;

public class Sam_PlayeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 12f; // Player movement speed
    [SerializeField] private float jumpHeight = 2f; // Height of the player's jump
    [SerializeField] private float gravity = -9.81f; // Gravity applied to the player
    [SerializeField] private CharacterController controller; // Character controller for movement

    private Vector3 velocity; // Tracks player velocity
    private bool isGrounded; // Checks if the player is on the ground

    private void Update()
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
        Vector3 move = transform.right * x + transform.forward * z;
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
}
