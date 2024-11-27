using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sam_PlayeMovement : MonoBehaviour
{
    public float speed = 12f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public CharacterController controller;
    private Vector3 velocity;
    public float gravity = -20f;
    public float groundDistance = 0.4f;
    private bool isGrounded;
    public float jumpHeight = 2f;
    public Transform playerCamera; // Reference to the player's camera

    private void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
      
        pitch -= speedV * Input.GetAxis("Mouse Y");
      
        pitch = Mathf.Clamp(pitch, -90f, 90f); // Clamp pitch to avoid over-rotation

        // Update input mappings for movement
        float moveForwardBack = Input.GetAxis("Vertical");
       
        float moveLeftRight = Input.GetAxis("Horizontal");

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
      
        playerCamera.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f); // Apply pitch to camera

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        // Combine forward/back and left/right movement
        Vector3 move = transform.right * moveLeftRight + transform.forward * moveForwardBack;
       
        controller.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);
        
    }

}
