using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelUI : MonoBehaviour {
    [SerializeField] Image wheelImg;

    void Update() {
        wheelImg.transform.rotation = Quaternion.Euler(-ShipInfo.wheelRotation * Vector3.forward);
    }   
}
