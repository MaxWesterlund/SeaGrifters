using TMPro;
using UnityEngine;

public class IslandUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI islandBanner;
    [SerializeField] TextMeshProUGUI prompt;
    
    void Start() {
        IslandManager islandManager = IslandManager.Instance;
        islandManager.EnteredIsland += OnEnteredIsland;
        islandManager.ExitedIsland += OnExitedIsland;
    }

    void OnEnteredIsland(Island island) {
        islandBanner.text = island.IslandName;
        prompt.text = "Enter?";
    }

    void OnExitedIsland() {
        islandBanner.text = "";
        prompt.text = "";
    }
}
