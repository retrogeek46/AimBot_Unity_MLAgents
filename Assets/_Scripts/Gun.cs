using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject    gun;                    // static reference to the gameobject
    public Target        target;                // refernce to the target component
    public static float  sensitivity = 0.5f;     // sensitivity of the gun rotation
    public PerfectAim    perfectAim;

    // Start is called before the first frame update
    void Start () {
        perfectAim = gameObject.GetComponent<PerfectAim>();
    }

    // Update is called once per frame
    void Update () {

    }

    /// <summary>
    /// This function fires a ray forward and compares the tag to check if target has been hit
    /// </summary>
    public void Fire () {
        //DebugText.AddDebugText("firing");
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out RaycastHit hit)) {
            if (hit.transform.CompareTag("target")) {
                DebugText.AddDebugText("hit target");
                target.targetHit = true;
            }
        }
    }

    /// <summary>
    /// This function moves the camera using keypresses in the direction specified.
    /// Currently only up-down and left-right are being used
    /// </summary>
    /// <param name="direction">An int from 0-8 signifying the direction to turn</param>
    public void MoveGun (int direction) {
        //Debug.Log("dir : " + direction);
        // reset z axis rotation
        gun.transform.eulerAngles = new Vector3(gun.transform.eulerAngles.x, gun.transform.eulerAngles.y, 0);
        // move in correct direction based on direction provided 
        switch (direction) {
            // move top-left
            case 0:
                gun.transform.Rotate(new Vector3(-1, 1, 0) * sensitivity);
                break;
            // move top
            case 1:
                gun.transform.Rotate(new Vector3(-1, 0, 0) * sensitivity);
                break;
            // move top-right
            case 2:
                gun.transform.Rotate(new Vector3(1, 1, 0) * sensitivity);
                break;
            // move left
            case 3:
                gun.transform.Rotate(new Vector3(0, -1, 0) * sensitivity);
                break;
            // move right
            case 4:
                gun.transform.Rotate(new Vector3(0, 1, 0) * sensitivity);
                break;
            // move bottom-left
            case 5:
                gun.transform.Rotate(new Vector3(-1, -1, 0) * sensitivity);
                break;
            // move bottom
            case 6:
                gun.transform.Rotate(new Vector3(1, 0, 0) * sensitivity);
                break;
            // move bottom-right
            case 7:
                gun.transform.Rotate(new Vector3(-1, -1, 0) * sensitivity);
                break;
        }
    }

    /// <summary>
    /// Resets the rotation of the camera when episode is done
    /// </summary>
    public void ResetCamera () {
        //DebugText.AddDebugText("reset camera");
        gun.transform.rotation = Quaternion.identity;
    }
}
