using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    Player player;

    Vector2 moveDir;

    void Awake() {
        player = GetComponent<Player>();
    }

    void Start() {
        InputManager im = InputManager.Instance;
        im.SubPerformedAndCanceled("Move", OnMove);
    }

    void OnMove(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;
        
        Vector2 value = ctx.ReadValue<Vector2>();
        moveDir = value;
    }

    void Update() {
        transform.position += 3 * Time.deltaTime * (Vector3)moveDir;
    }
}
