using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpPressBufferTime = .05f;

    //jumpAction = playerInput.actions["jump"];
    //InputAction jumpAction;

    Player player;

    bool tryingToJump;
    float lastJumpPressTime;

    void Awake()
    {
        player = GetComponent<Player>();

    }

    void OnEnable()
    {
        player.OnBeforeMove += OnBeforeMove;
    }

    void OnDisable()
    {
        player.OnBeforeMove -= OnBeforeMove;
    }

    void OnJump()
    {
        tryingToJump = true;
        lastJumpPressTime = Time.time;
    }

    void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;

        bool isOrWasTryingToJump = tryingToJump || wasTryingToJump;

        if (isOrWasTryingToJump && player.IsGrounded)
        {
            player.velocity.y += jumpSpeed;
        }
        tryingToJump = false;
    }

    /*var jumpInput = jumpAction.ReadValue<float>();
        if (jumpInput > 0 && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }*/
}
