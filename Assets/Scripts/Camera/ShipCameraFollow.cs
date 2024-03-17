using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCameraFollow : SingletonBehaviour<ShipCameraFollow> {
    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float lookAngle;
    [SerializeField] float lookAheadAngle;

    [HideInInspector] public Transform SecondaryTarget;
    
    void Update() {
        Vector3 offset = new Vector3(
            0,
            distance * Mathf.Cos(-lookAngle * Mathf.Deg2Rad),
            distance * Mathf.Sin(-lookAngle * Mathf.Deg2Rad)
        );
        Vector3 targetPosition = target.position;
        if (SecondaryTarget != null) {
            targetPosition = (target.position + SecondaryTarget.position) / 2;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition + offset, 10 * Time.deltaTime);
        transform.rotation = Quaternion.Euler((90 - lookAngle - lookAheadAngle) * Vector3.right);
    }
}
