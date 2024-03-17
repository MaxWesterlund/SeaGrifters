using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour {
    [SerializeField] Transform ship;
    [SerializeField] float maxDistance;
    
    void Update() {
        Island[] islands = FindObjectsOfType<Island>();

        float shortestDist = maxDistance;
        Island selectedIsland = null;
        foreach (Island island in islands) {
            float dist = Vector3.Distance(ship.transform.position, island.transform.position);
            if (dist < maxDistance) {
                selectedIsland = island;
                shortestDist = dist;
            }
        }

        ShipCameraFollow.Instance.SecondaryTarget = selectedIsland == null ? null : selectedIsland.transform;
    }
}
