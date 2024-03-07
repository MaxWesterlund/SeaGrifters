using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonBehaviour<InputManager> {
    [SerializeField] InputActionAsset asset;

    void Awake() {
        asset.Enable();
        DontDestroyOnLoad(gameObject);
    }

    public void SubPerformedAndCanceled(string name, Action<InputAction.CallbackContext> s) {
        InputAction action = asset.FindAction(name);
        action.performed += s;
        action.canceled += s;
    }

    public void SubPerformed(string name, Action<InputAction.CallbackContext> s) {
        InputAction action = asset.FindAction(name);
        action.performed += s;
    }
}