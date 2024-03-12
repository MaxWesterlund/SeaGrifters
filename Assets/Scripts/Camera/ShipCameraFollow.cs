using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCameraFollow : MonoBehaviour {
    [SerializeField] Transform ship;
    [SerializeField] float distance;
    [SerializeField] float lookAngle;

    void Start() {
        transform.position = GetRelativePosition();
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, ship.position + GetRelativePosition(), 10 * Time.deltaTime);
        transform.LookAt(ship);
    }

    Vector3 GetRelativePosition() {
        float length = Mathf.Sin(lookAngle * Mathf.Deg2Rad) * distance;
        float height = Mathf.Cos(lookAngle * Mathf.Deg2Rad) * distance;

        return new Vector3(-ship.forward.x * length, height, -ship.forward.z * length);
    }
}
