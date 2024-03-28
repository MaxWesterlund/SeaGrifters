using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    [SerializeField] PlayerNumber playerNumber = PlayerNumber.One;

    public bool IsInput(InputAction.CallbackContext ctx) {
        int id = ctx.control.device.deviceId;
        if (PlayerNumber.One == playerNumber && PlayerDeviceInfo.Player1ID == id) {
            return true;
        }
        else if (PlayerNumber.Two == playerNumber && PlayerDeviceInfo.Player2ID == id) {
            return true;
        }
        return false;
    }

    enum PlayerNumber {
        One,
        Two
    }
}
