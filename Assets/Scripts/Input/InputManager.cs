using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : SingletonBehaviour<InputManager> {
    [SerializeField] InputActionAsset asset;

    InputActionMap currentActionMap;

    void Awake() {
        foreach (InputActionMap map in asset.actionMaps) {
            map.Disable();
        }
        asset.Enable();
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        string name = scene.name;
        InputActionMap newMap = asset.FindActionMap(name);
        currentActionMap?.Disable();
        currentActionMap = newMap;
        currentActionMap.Enable();
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