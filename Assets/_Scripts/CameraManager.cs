using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Camera[] cameras = new Camera[9];
    int noOfCameras = 0;
    int counter = 1;
    Camera currentCamera = null;
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

    public void SwitchToGameCamera () {
        currentCamera.depth = -1;
        if (noOfCameras > 1) {
            currentCamera = cameras[counter++];
        }
        currentCamera.depth = 1;
    }

    public void SwitchToMainCamera () {
        currentCamera.depth = -1;
    }
}
