using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagRotation : MonoBehaviour {
    [SerializeField] Transform flag;

    void Update() {
        float rotation = Wind.Instance.GetWindAngle();
        flag.rotation = Quaternion.Euler(rotation * Mathf.Rad2Deg * Vector3.up);
    }
}
