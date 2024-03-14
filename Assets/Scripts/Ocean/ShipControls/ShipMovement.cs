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

    Vector3 lastPos = Vector3.zero;

    void Start() {
        transform.position = depth * Vector3.down;
    }

    void Update() {
        Steer();
        RotateSails();
        Move();

        lastPos = transform.position;
    }

    void Steer() {
        shipRotation = (shipRotation + wheelRotation * turnSpeed) % 180;
        transform.rotation = Quaternion.Euler(shipRotation * Vector3.up);
    }

    void RotateSails() {
        sails.localRotation = Quaternion.Euler(sailRotation * Vector3.up);
    }

    void Move() {
        float windAngle = Wind.Instance.GetWindAngle() * Mathf.Rad2Deg;
        if (windAngle < 0) {
            windAngle += 360;
        }
        float sailWorldRot = (shipRotation + sailRotation) % 360;
        print(sailWorldRot);
        float rotDiff = Mathf.Abs(windAngle - sailWorldRot);
        float minRotDiff = Mathf.Min(rotDiff, 360 - rotDiff);
        float windMultiplier = 1 - minRotDiff / 180;
        transform.position += speed * windMultiplier * Time.deltaTime * transform.forward;
    }

    public void AddWheelRotation(float rot) {
        wheelRotation = Mathf.Clamp(wheelRotation + rot * wheelRotationSpeed, -maxWheelRotation, maxWheelRotation);
    }

    public void AddSailRotation(float rot) {
        sailRotation = Mathf.Clamp(sailRotation + rot * sailRotationSpeed, -maxSailRotation, maxSailRotation);
    }
}
