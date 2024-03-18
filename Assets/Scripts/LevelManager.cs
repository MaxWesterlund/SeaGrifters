using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonBehaviour<LevelManager> {
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void SwitchScene(string name) {
        SceneManager.LoadScene(name);
    }
}
