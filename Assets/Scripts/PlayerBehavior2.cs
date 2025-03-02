using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior2 : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
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

    void UpdateMovement()
    {
        //var x = Input.GetAxisRaw("Horizontal");
        //var y = Input.GetAxisRaw("Vertical");
        //Debug.Log(x);
        //Debug.Log(y);
        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.Normalize(input); //makes it so you dont move faster diagonally than horizontally

        var jumpInput = jumpAction.ReadValue<float>();
        if (jumpInput > 0 && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }
        
        controller.Move((input * movementSpeed + velocity) * Time.deltaTime);

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
