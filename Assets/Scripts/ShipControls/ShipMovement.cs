using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float wheelRotationSpeed;
    [SerializeField] float sailRotationSpeed;
    [SerializeField] float maxWheelRotation;
    [SerializeField] float maxSailRotation;

    float wheelRotation;
    float sailRotation;

    void Update() {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + wheelRotation * turnSpeed * Vector3.up);
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    public void AddWheelRotation(float rot) {
        wheelRotation = Mathf.Clamp(wheelRotation + rot * wheelRotationSpeed, -maxWheelRotation, maxWheelRotation);
    }

    public void AddSailRotation(float rot) {
        sailRotation = Mathf.Clamp(sailRotation + rot * sailRotationSpeed, -maxSailRotation, maxSailRotation);
    }
}
