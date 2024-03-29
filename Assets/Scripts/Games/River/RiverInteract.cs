using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RiverInteract : MonoBehaviour {
    Player player;
    Rigidbody2D rb;

    [Header("Prefab")]
    [SerializeField] float minBoatDistance;

    [Header("Scene")]
    [SerializeField] RiverBoat boat;

    bool boatIsInRange;
    bool isOnBoat;

    public bool IsOnBoat { get { return isOnBoat; } }

    void Awake() {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        InputManager im = InputManager.Instance;
        im.SubPerformed("Interact", OnInteract);
        im.SubPerformed("Cancel", OnCancel);
    }

    void OnInteract(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;
        if (isOnBoat) return;

        if (boatIsInRange) {
            boat.AddPlayer(player);
            transform.parent = boat.transform;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            isOnBoat = true;
        }
    }

    void OnCancel(InputAction.CallbackContext ctx) {
        if (!player.IsInput(ctx)) return;
        if (!isOnBoat) return;
        
        boat.RemovePlayer();
        transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isOnBoat = false;
    }

    void Update() {
        if (boatIsInRange != Vector3.Distance(transform.position, boat.transform.position) <= minBoatDistance) {
            boatIsInRange = !boatIsInRange;
        }
    }
}
