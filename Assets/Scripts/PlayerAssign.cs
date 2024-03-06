using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssign : MonoBehaviour {
    [SerializeField] PlayerAssignUI player1UI;
    [SerializeField] PlayerAssignUI player2UI;

    bool player1Ready;
    bool player2Ready;

    void Start() {
        InputManager.Instance.SubPerformed("Join", OnJoin);
        InputManager.Instance.SubPerformed("Ready", OnReady);
        InputManager.Instance.SubPerformed("Leave", OnLeave);
    }

    void OnJoin(InputAction.CallbackContext ctx) {
        int id = ctx.control.device.deviceId;
        if (PlayerDeviceInfo.Player1ID == -1) {
            PlayerDeviceInfo.Player1ID = id;
            player1UI.SetDeviceName($"{id}");
        }
        else if (PlayerDeviceInfo.Player2ID == -1) {
            PlayerDeviceInfo.Player2ID = id;
            player2UI.SetDeviceName($"{id}");
        }
    }

    void OnReady(InputAction.CallbackContext ctx) {
        int id = ctx.control.device.deviceId;
        if (PlayerDeviceInfo.Player1ID == id) {
            player1Ready = !player1Ready;
            player1UI.SetReadyStatus(player1Ready);
        }
        else if (PlayerDeviceInfo.Player2ID == id) {
            player2Ready = !player2Ready;
            player2UI.SetReadyStatus(player2Ready);
        }
    }

    void OnLeave(InputAction.CallbackContext ctx) {
        int id = ctx.control.device.deviceId;
        if (PlayerDeviceInfo.Player1ID == id) {
            player1Ready = false;
            PlayerDeviceInfo.Player1ID = -1;
            player1UI.Reset();
        }
        else if (PlayerDeviceInfo.Player2ID == id) {
            player2Ready = false;
            PlayerDeviceInfo.Player2ID = -1;
            player2UI.Reset();
        }
    }
}
