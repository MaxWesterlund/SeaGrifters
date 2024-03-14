using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipSteering : MonoBehaviour {
    Player player;
    ShipMovement shipMovement;

    [SerializeField] Transform ship;

    Vector2 steerVec;

    void Awake() {
        player = GetComponent<Player>();
        shipMovement = GetComponentInParent<ShipMovement>();
    }

    void Start() {
        InputManager im = InputManager.Instance;
        im.SubPerformedAndCanceled("Steer", OnSteer);
    }

    void OnSteer(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;
        
        Vector2 value = ctx.ReadValue<Vector2>();
        steerVec = value;
    }

    void Update() {
        shipMovement.AddWheelRotation(steerVec.x * Time.deltaTime);
    }
}
