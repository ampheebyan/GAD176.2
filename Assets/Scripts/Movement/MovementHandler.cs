using System;
using Characters.LocalPlayer;
using UnityEngine;

// Phoebe Faith (1033478)

/// <summary>
/// This contains all code specific to local player movement.
/// </summary>

namespace Movement
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public class MovementHandler : MonoBehaviour
    {

        [Serializable]
        public class MovementState
        {
            public bool isRunning = false;
            public bool isCrouching = false;
            public bool isGrounded = false;
            public float currentWalkSpeed = 0f;
        }

        [Serializable]
        public class MovementHandlerDebugInformation
        {
            public MovementState currentMovementState;
            public Vector3 position;

            public Vector2 rotation;
            public Vector2 velocity;
        }
        
        [Header("Public API")]
        public MovementState movementState = new MovementState();
        
        [Header("Private Assignable")]
        [SerializeField] 
        private Camera mainCamera;

        [Header("Movement Settings")] 
        [SerializeField]
        private float walkSpeed = 6f;
        [SerializeField]
        private float runSpeed = 9f;
        [SerializeField]
        private float crouchSpeed = 4f;
        [SerializeField]
        private float crouchHeight = 1f;
        [SerializeField]
        private float jumpHeight = 1f;
        [SerializeField]
        private float gravity = 20f;
        
        [Header("Camera Settings")]
        [SerializeField]
        private Vector2 cameraSensitivity = new Vector2(2f, 2f);

        // These variables are fully private. Don't touch them unless you _really_ have to.
        private CharacterController _controller;
        private float _defaultCharacterHeight = 0f; // This will be set when we find the character controller.
        private Vector3 _defaultCharacterCameraPosition; // This is set later.

        // Movement specific stuff
        private Vector2 _rotation;
        private Vector3 _velocity;

        // Misc
        private bool _mouseLock = false;
        private LocalPlayer _localPlayer;
        private bool _initComplete = false;
        
        [Header("Miscellaneous Properties")]
        [SerializeField]
        [Tooltip("Don't edit anything in this, it will not reflect in the actual game. This is used as a visualisation of what debug information this object will return.")]
        private MovementHandlerDebugInformation _debugInformation = new MovementHandlerDebugInformation();
        
        public event Action<MovementHandlerDebugInformation> OnDebugInformationChanged;
        
        private void OnEnable()
        {
            // Find character controller and set it.
            if (TryGetComponent<CharacterController>(out _controller))
            {
                _defaultCharacterHeight = _controller.height;
                _defaultCharacterCameraPosition = mainCamera.transform.localPosition;
                
                TryGetComponent<LocalPlayer>(out _localPlayer);
                _debugInformation.currentMovementState = movementState;

                _initComplete = true;
            }
            else
            {
                throw new MissingComponentException("CharacterController not found, this script will not function.");
            }
        }

        private void LockMouse()
        {
            // Lock/unlock cursor.
            Cursor.lockState = GlobalReference.isDebug ? CursorLockMode.None : _mouseLock ? CursorLockMode.Locked : CursorLockMode.None; // If _mouseLock is true, set lockState to locked, otherwise set to none.
            if (Input.GetMouseButtonDown(0) && !_mouseLock) _mouseLock = true; // If click on window, set _mL to true.
            else if (Input.GetKeyDown(KeyCode.Escape) && _mouseLock) _mouseLock = false; // If press escape, set _mL to false.
        }

        private void CameraHandling(Vector2 mouseXY)
        {
            // Up/down
            _rotation.x -= mouseXY.y * cameraSensitivity.y;
            _rotation.x = Mathf.Clamp(_rotation.x, -90f, 90f);
            
            // Left/right
            _rotation.y -= mouseXY.x * cameraSensitivity.x;
            _debugInformation.rotation = _rotation;

            transform.eulerAngles = new Vector3(0, _rotation.y * -1, 0);
            mainCamera.transform.localEulerAngles = new Vector3(_rotation.x, 0, 0);
        }
        
        private void MoveController(Vector2 movementInput)
        {
            // Set all specific variables
            movementState.isCrouching = Input.GetKey(KeyCode.LeftControl);
            movementState.isRunning = Input.GetKey(KeyCode.LeftShift);
            movementState.isGrounded = _controller.isGrounded;
            movementState.currentWalkSpeed = movementState.isCrouching ? crouchSpeed : movementState.isRunning ? runSpeed : walkSpeed; // One real fat ternary operator
            
            // Crouch height handling
            mainCamera.transform.localPosition = movementState.isCrouching ? (_defaultCharacterCameraPosition - new Vector3(0, (_defaultCharacterHeight - crouchHeight) / 2.15f, 0)) : _defaultCharacterCameraPosition;
            _controller.height = movementState.isCrouching ? crouchHeight : _defaultCharacterHeight;
            
            // Jumping
            if (Input.GetKey(KeyCode.Space) && movementState.isGrounded)
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);

            Gravity();
            
            // Player movement
            Vector3 movement = transform.right * movementInput.x + transform.forward * movementInput.y;
            Vector3 motion = movement * (movementState.currentWalkSpeed * Time.deltaTime) + _velocity * Time.deltaTime;
            
            Physics.SyncTransforms();
            _controller.Move(motion);
            _debugInformation.position = transform.position;

        }

        private void Gravity()
        {
            if (movementState.isGrounded && _velocity.y < 0) _velocity.y = -2f; 
            _velocity.y += -gravity * Time.deltaTime; // Physics! Kinda!
            _debugInformation.velocity = _velocity;
        }

        public MovementHandlerDebugInformation GetDebugInformation()
        {
            return _debugInformation;
        }
        
        private void Update()
        {
            // Nothing should ever occur if the initialisation of this script is not complete.
            if (!_initComplete) return; 
            // Handle mouse locking and handling of camera movement.
            LockMouse();
            CameraHandling(new Vector2(
                Input.GetAxis("Mouse X"), 
                Input.GetAxis("Mouse Y"))); 
            
            // Handle movement of player.
            MoveController(new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")));
            
            OnDebugInformationChanged?.Invoke(_debugInformation);
            // I should probably avoid GetAxis, and use the newer Input Manager, but I feel like we probably don't need that much control.
            // I could also probably implement my own form of GetAxis for keyboard stuff, because reading how GetAxis works from the Unity docs, it just is effectively a lerp between
            // -1 and 1 at steps of 0.05. Kinda easy, and would probably allow for the ability to "weight" movement.
            // I'll get to it at some point I guess.
        }
    }
}
