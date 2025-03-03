using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;

    //jumpAction = playerInput.actions["jump"];
    //InputAction jumpAction;

    Player player;

    bool tryingToJump;

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
        if (player.IsGrounded)
        {
            player.velocity.y += jumpSpeed;
        }
    }

    void OnBeforeMove()
    {
        if (tryingToJump && player.IsGrounded)
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
