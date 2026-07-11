using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FPS : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 0.12f;
    [SerializeField] private float maximumLookAngle = 85f;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    private CharacterController _characterController;

    private float _cameraPitch;
    private float _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                cameraTransform = mainCamera.transform;
            }
        }
    }

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleCursor();
    }

    private void HandleMouseLook()
    {
        if (Mouse.current == null || cameraTransform == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        // Rotate the entire player left and right.
        transform.Rotate(Vector3.up * mouseX);

        // Rotate only the camera up and down.
        _cameraPitch -= mouseY;
        _cameraPitch = Mathf.Clamp(
            _cameraPitch,
            -maximumLookAngle,
            maximumLookAngle
        );

        cameraTransform.localRotation = Quaternion.Euler(
            _cameraPitch,
            0f,
            0f
        );
    }

    private void HandleMovement()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            input.y += 1f;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            input.y -= 1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            input.x += 1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            input.x -= 1f;
        }

        input = Vector2.ClampMagnitude(input, 1f);

        float currentSpeed = Keyboard.current.leftShiftKey.isPressed
            ? sprintSpeed
            : walkSpeed;

        Vector3 horizontalMovement =
            transform.right * input.x +
            transform.forward * input.y;

        horizontalMovement *= currentSpeed;

        if (_characterController.isGrounded)
        {
            if (_verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _verticalVelocity = Mathf.Sqrt(
                    jumpHeight * -2f * gravity
                );
            }
        }

        _verticalVelocity += gravity * Time.deltaTime;

        Vector3 movement = horizontalMovement;
        movement.y = _verticalVelocity;

        _characterController.Move(movement * Time.deltaTime);
    }

    private void HandleCursor()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnlockCursor();
        }

        if (
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame
        )
        {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}