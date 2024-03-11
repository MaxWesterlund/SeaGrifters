using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MastRotation : MonoBehaviour {
    Player player;

    [SerializeField] Transform mast;

    Vector2 rotationVec;

    float rotation;

    void Awake() {
        player = GetComponent<Player>();
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
        rotation += Time.deltaTime * rotationVec.x;
        mast.rotation = Quaternion.Euler(rotation * Vector3.forward);
    }
}
