using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipSteering : MonoBehaviour {
    Player player;

    [SerializeField] Transform ship;

    Vector2 steerVec;

    float rotation;

    void Awake() {
        player = GetComponent<Player>();
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
        rotation += steerVec.x * Time.deltaTime;
        ship.rotation = Quaternion.Euler(rotation * Vector3.forward);
    }
}
