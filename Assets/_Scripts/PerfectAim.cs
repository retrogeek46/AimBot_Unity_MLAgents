using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectAim : MonoBehaviour {

    public Transform    targetTransform;        // reference to the target gameobject
    public Transform    gunTransform;           // reference to the gun gameobject
    public bool         perfectAimmer = false;  // bool to check if perfect aiming is enabled
    public float        rotationSpeed = 0.5f;   // speed at which the gun can rotate
    public float        rotationDegree = 2000f; // the no of degrees gun can rotate each function call

    // Update is called once per frame
    void Update () {
        if (perfectAimmer) {
            // If enabled, call the aiming method
            Aim();
        }
    }

    /// <summary>
    /// This function is called every frame to rotate the gun towards the target
    /// </summary>
    private void Aim () {
        Vector3 directionEnemyBody;
        directionEnemyBody = targetTransform.position - gunTransform.position;
        Quaternion lookRotationEnemyBody = Quaternion.LookRotation(directionEnemyBody);
        gunTransform.rotation = Quaternion.RotateTowards(gunTransform.rotation, lookRotationEnemyBody, rotationDegree);
    }
}
