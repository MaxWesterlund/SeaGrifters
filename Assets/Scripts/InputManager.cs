using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonBehaviour<InputManager> {
    [SerializeField] InputActionAsset inputActions;

    void Start() {
        inputActions.Enable();
    }
}
