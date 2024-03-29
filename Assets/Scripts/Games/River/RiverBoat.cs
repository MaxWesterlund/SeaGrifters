using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RiverBoat : MonoBehaviour {
    Rigidbody2D rb;

    [SerializeField] float speed;

    [HideInInspector] public bool IsOccupied;

    Player currentPlayer;

    Vector2 moveVec;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        InputManager im = InputManager.Instance;
        im.SubPerformedAndCanceled("Move", OnMove);
    }

    void OnMove(InputAction.CallbackContext ctx) {
        if (!IsOccupied) return;
        if (!currentPlayer.IsInput(ctx)) return;

        Vector2 value = ctx.ReadValue<Vector2>();
        moveVec = value;
    }

    void Update() {
        rb.velocity = moveVec.x * speed * Vector2.right;
    }

    public void AddPlayer(Player player) {
        currentPlayer = player;
        IsOccupied = true;
    }

    public void RemovePlayer() {
        currentPlayer = null;
        IsOccupied = false;
    }
}
