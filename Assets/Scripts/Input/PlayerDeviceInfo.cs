using UnityEngine.InputSystem;

public static class PlayerDeviceInfo {
    public static int Player1ID = -1;
    public static int Player2ID = -1;

    public static bool IsPlayer1(InputAction.CallbackContext ctx) {
        return ctx.control.device.deviceId == Player1ID;
    }

    public static bool IsPlayer2(InputAction.CallbackContext ctx) {
        return ctx.control.device.deviceId == Player1ID;
    }
}
