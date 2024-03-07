using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    [SerializeField] PlayerNumber playerNbr = PlayerNumber.One;

    public bool IsInput(InputAction.CallbackContext ctx) {
        int id = ctx.control.device.deviceId;
        if (PlayerNumber.One == playerNbr && PlayerDeviceInfo.Player1ID == id) {
            return true;
        }
        else if (PlayerNumber.Two == playerNbr && PlayerDeviceInfo.Player2ID == id) {
            return true;
        }
        return false;
    }

    enum PlayerNumber {
        One,
        Two
    }
}
