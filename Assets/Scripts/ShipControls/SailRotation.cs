using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SailRotation : MonoBehaviour {
    Player player;
    ShipMovement shipMovement;

    [SerializeField] Transform sail;

    Vector2 rotationVec;

    float rotation;

    void Awake() {
        player = GetComponent<Player>();
        shipMovement = GetComponentInParent<ShipMovement>();
    }

    void Start() {
        InputManager im = InputManager.Instance;
        im.SubPerformedAndCanceled("RotateMast", OnRotateMast);
    }

    void OnRotateMast(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;

        Vector2 value = ctx.ReadValue<Vector2>();
        rotationVec = value;
    }

    void Update() {
        shipMovement.AddSailRotation(rotationVec.x * Time.deltaTime);
    }
}
