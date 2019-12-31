using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Camera[]    cameras = new Camera[9];    // array to get reference to all the room cameras in the scene
    int                 noOfCameras = 0;            // int to store the total no of cameras in scene
    int                 counter = 1;                // int to keep track of current room camera index
    Camera              currentCamera = null;       // currently active camera

    // Start is called before the first frame update
    void Start () {
        // get all camera objects into the array
        var cameraObjects = GameObject.FindGameObjectsWithTag("gun");
        noOfCameras = cameraObjects.Length;
        for (int i = 0; i < noOfCameras; i++) {
            cameras[i] = cameraObjects[i].GetComponent<Camera>();
        }
        currentCamera = cameras[0];
    }

    // Update is called once per frame
    void Update () {
        if (counter == cameras.Length) {
            counter = 0;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            SwitchToGameCamera();
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            SwitchToMainCamera();
        }
    }

    /// <summary>
    /// Switches the view from overworld camera to room camera. If already in room camera mode then
    /// switches between different rooms.
    /// </summary>
    public void SwitchToGameCamera () {
        currentCamera.depth = -1;
        if (noOfCameras > 1) {
            currentCamera = cameras[counter++];
        }
        currentCamera.depth = 1;
    }

    /// <summary>
    /// Switch to the overworld camera from the main camera.
    /// </summary>
    public void SwitchToMainCamera () {
        currentCamera.depth = -1;
    }
}
