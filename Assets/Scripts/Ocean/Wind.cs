using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : SingletonBehaviour<Wind> {
    Vector3 direction;
    
    void Start() {
        float angle = Random.Range(0, 2 * Mathf.PI);
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    public Vector3 GetWindDirection() {
        return direction;
    }

    public float GetWindAngle() {
        return Mathf.Atan2(direction.z, direction.x);
    }
}
