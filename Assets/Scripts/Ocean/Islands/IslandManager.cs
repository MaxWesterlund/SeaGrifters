using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IslandManager : SingletonBehaviour<IslandManager> {
    [SerializeField] Transform ship;
    [SerializeField] float maxDistance;

    public event Action<Island> EnteredIsland;
    public event Action ExitedIsland;

    Island selectedIsland;

    void Start() {
        InputManager.Instance.SubPerformed("EnterIsland", OnEnterIsland);
    }
    
    void Update() {
        Island[] islands = FindObjectsOfType<Island>();

        float shortestDist = maxDistance;
        Island closest = null;
        foreach (Island island in islands) {
            float dist = Vector3.Distance(ship.transform.position, island.transform.position);
            if (dist < shortestDist) {
                closest = island;
                shortestDist = dist;
            }
        }

        if (selectedIsland == closest) return;

        selectedIsland = closest;

        if (selectedIsland == null) {
            ExitedIsland.Invoke();
        }
        else {
            EnteredIsland.Invoke(selectedIsland);
        }

        ShipCameraFollow.Instance.SecondaryTarget = selectedIsland == null ? null : selectedIsland.transform;
    }

    void OnEnterIsland(InputAction.CallbackContext ctx) {
        if (selectedIsland == null) return;

        if (PlayerDeviceInfo.IsPlayer1(ctx)) {
            SceneManager.LoadScene(selectedIsland.ScenePath);
        }
    }
}
