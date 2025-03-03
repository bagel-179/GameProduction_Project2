using System;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] Transform cameraTransform;

    public event Action OnBeforeMove;

    internal float movementSpeedMultiplier;

    public bool IsGrounded => controller.isGrounded;

    public float Height
    {
        get => controller.height;
        set => controller.height = value;
    }

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction sprintAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        sprintAction = playerInput.actions["sprint"];
    }

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock the cursor at the start of the game
        Time.timeScale = 1f;
    }

    void Update()
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    Vector3 GetMovementInput()
    {
        var moveInput = moveAction.ReadValue<Vector2>();
        var input = new Vector3();

        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.Normalize(input); //makes it so you dont move faster diagonally than horizontally

        //var sprintInput = sprintAction.ReadValue<float>();
        //var multiplier = sprintInput > 0 ? 1.5f : 1f;

        input *= movementSpeed * movementSpeedMultiplier;
        return input;
    }

    void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        OnBeforeMove?.Invoke();

        var input = GetMovementInput();

        var factor = acceleration * Time.deltaTime; //velocity gradually increases and decreases when 'w' is pressed and unpressed
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        var jumpInput = jumpAction.ReadValue<float>();
        if (jumpInput > 0 && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }
        
        controller.Move(velocity * Time.deltaTime);

    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;
        Debug.Log(look);

        look.y = Mathf.Clamp(look.y, -89f, 89f); //make it so player can't look 360 degrees

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }

}
