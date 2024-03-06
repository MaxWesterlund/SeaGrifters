using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAssignUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI deviceNameText;
    [SerializeField] Image readyStatusImage;

    string defaultDeviceName;

    void Start() {
        defaultDeviceName = deviceNameText.text;
    }

    public void SetDeviceName(string newName) {
        deviceNameText.text = newName;
    }

    public void SetReadyStatus(bool isReady) {
        if (isReady) {
            readyStatusImage.color = new Color(0, 255, 0);
        }
        else {
            readyStatusImage.color = new Color(255, 0, 0);
        }
    }

    public void Reset() {
        SetDeviceName(defaultDeviceName);
        SetReadyStatus(false);
    }
}
