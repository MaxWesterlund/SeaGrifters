using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
    [SerializeField] float depth;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float wheelRotationSpeed;
    [SerializeField] float sailRotationSpeed;
    [SerializeField] float maxWheelRotation;
    [SerializeField] float maxSailRotation;

    [SerializeField] Transform sails;

    float wheelRotation;
    float shipRotation;
    float sailRotation;

    void Start() {
        transform.position = depth * Vector3.down;
    }

    void Update() {
        Steer();
        RotateSails();
        Move();
    }

    void Steer() {
        shipRotation = shipRotation + wheelRotation * Time.deltaTime * turnSpeed;
        transform.rotation = Quaternion.Euler(shipRotation * Vector3.up);
    }

    void RotateSails() {
        sails.localRotation = Quaternion.Euler(-sailRotation * Vector3.up);
    }

    void Move() {
        float sailWorldRot = shipRotation + sailRotation + 90;
        Vector3 sailDirection = new(Mathf.Cos(sailWorldRot * Mathf.Deg2Rad), 0, Mathf.Sin(sailWorldRot * Mathf.Deg2Rad));
        Vector3 windDirection = Wind.Instance.GetWindDirection();
        float betweenRot = Mathf.Acos(Vector3.Dot(sailDirection, windDirection) / (sailDirection.magnitude * windDirection.magnitude));
        float windMultiplier = (Mathf.PI - betweenRot) / Mathf.PI;
        transform.position += speed * windMultiplier * Time.deltaTime * transform.forward;
    }

    public void AddWheelRotation(float rot) {
        wheelRotation = Mathf.Clamp(wheelRotation + rot * wheelRotationSpeed, -maxWheelRotation, maxWheelRotation);
        ShipInfo.wheelRotation = wheelRotation;
    }

    public void AddSailRotation(float rot) {
        sailRotation = Mathf.Clamp(sailRotation - rot * sailRotationSpeed, -maxSailRotation, maxSailRotation);
    }
}
