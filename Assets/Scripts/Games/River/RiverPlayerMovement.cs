using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RiverPlayerMovement : MonoBehaviour {
    Player player;
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpForgiveTime;

    const float gravityVel = 9.82f;

    Vector2 moveVec;
    bool isGrounded;
    float lastJumpTime;

    void Awake() {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        InputManager im = InputManager.Instance;
        im.SubPerformedAndCanceled("Move", OnMove);
        im.SubPerformed("Jump", OnJump);
    }

    void OnMove(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;

        Vector2 value = ctx.ReadValue<Vector2>();
        moveVec = value;
    }

    void OnJump(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;

        lastJumpTime = Time.time;
    }

    void Update() {
        // Gravity
        if (!isGrounded) {
            rb.velocity += gravityVel * Time.deltaTime * Vector2.down;
        }
        else {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
        // Jump
        if (isGrounded && Time.time - lastJumpTime < jumpForgiveTime) {
            rb.velocity += jumpForce * Vector2.up;
        }

        // Move
        rb.velocity = new Vector2(moveVec.x * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col) {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col) {
        isGrounded = false;
    }
}
