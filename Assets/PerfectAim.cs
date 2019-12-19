using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectAim : MonoBehaviour {

    public Transform targetTransform;
    public Transform gunTransform;
    public bool perfectAimmer = false;
    public float rotationSpeed = 0.5f;
    public float rotationDegree = 2000f;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (perfectAimmer) {
            //MoveTowardsTarget();
            Aim();
        }
    }

    private void MoveTowardsTarget () {
        Quaternion lookRotationEnemyBody;
        Vector3 directionEnemyBody;
        Vector3 directionGun;
        //get the direction vector of player to enemy and gun
        directionEnemyBody = targetTransform.position - gunTransform.position;
        directionGun = gunTransform.forward;
        //calculate the quaternion from the vector
        lookRotationEnemyBody = Quaternion.LookRotation(directionEnemyBody);
        //check if player is actually visible, if he is, turn
        Debug.DrawRay(gunTransform.position, directionGun * 20, Color.red);
        gunTransform.rotation = Quaternion.Lerp(gunTransform.rotation, lookRotationEnemyBody, rotationSpeed);
        // LEGACY CODE
        //Ray ray = new Ray(holder.position, directionGun);
        //if (Physics.Raycast(ray, out RaycastHit hit, 100f) && hit.collider.tag == "target") {
        //    /*
        //     * when animation is completed, set animation bool here instead of these
        //     */
        //    ReadyArm(lookRotationEnemyBody, lookRotationGun);
        //} else if (armReady) {
        //    LowerArm();
        //}
    }

    private void Aim () {
        Vector3 directionEnemyBody;
        directionEnemyBody = targetTransform.position - gunTransform.position;
        Quaternion lookRotationEnemyBody = Quaternion.LookRotation(directionEnemyBody);
        gunTransform.rotation = Quaternion.RotateTowards(gunTransform.rotation, lookRotationEnemyBody, rotationDegree);
    }
}
