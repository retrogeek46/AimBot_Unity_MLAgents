using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private static GameObject mainCamera;
    public static float rotationMultiplier = 0.5f;
    private Dictionary<KeyState, int> keyMapping = new Dictionary<KeyState, int> {
        { KeyState.up, 1},
        { KeyState.right, 4},
        { KeyState.down, 6},
        { KeyState.left, 3},
    };

    // Start is called before the first frame update
    void Start() {
        if (mainCamera == null) {
            mainCamera = GameObject.FindObjectOfType<Camera>().gameObject;
        }
    }

    // Update is called once per frame
    void Update() {
        //Vector3 cameraAngles = mainCamera.transform.rotation.eulerAngles;
        //DebugText.AddDebugText(cameraAngles);
        if (InputManager.anyKeyPressed) {
            if (InputManager.firePressed) {
                Fire();
            }
            if (keyMapping.ContainsKey(InputManager.currentState)) {
                //MoveCursor(keyMapping[InputManager.currentState]);
                //Debug.Log("in update trying to move keys");
                ;
            }
        }
        //// Clamp x axis
        //if (cameraAngles.x > 14) {
        //    mainCamera.transform.Rotate(14, cameraAngles.y, 0);
        //}
        //if (cameraAngles.x < -14) {
        //    mainCamera.transform.Rotate(-14, cameraAngles.y, 0);
        //}
        //// Clamp y axis
        //if (cameraAngles.x > 25) {
        //    mainCamera.transform.Rotate(cameraAngles.x, 25, 0);
        //}
        //if (cameraAngles.x < -25) {
        //    mainCamera.transform.Rotate(cameraAngles.x, -25, 0);
        //}
    }

    public void Fire () {
        //DebugText.AddDebugText("firing");
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit)) {
            if (hit.transform.tag == "target") {
                DebugText.AddDebugText("hit target");
                Target.targetHit = true;
            }
        }
    }

    /// <summary>
    /// This function moves the camera using keypresses in the direction specified.
    /// TODO: add bounds
    /// </summary>
    /// <param name="direction">An int from 0-8 signifying the direction to turn</param>
    public void MoveCursor(int direction) {
        //Debug.Log("dir : " + direction);
        // reset z axis rotation
        mainCamera.transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0); 
        switch (direction) {
            // move top-left
            case 0: 
                mainCamera.transform.Rotate(new Vector3(-1, 1, 0) * rotationMultiplier);
                break;
            // move top
            case 1:
                mainCamera.transform.Rotate(new Vector3(-1, 0, 0) * rotationMultiplier);
                break;
            // move top-right
            case 2:
                mainCamera.transform.Rotate(new Vector3(1, 1, 0) * rotationMultiplier);
                break;
            // move left
            case 3:
                mainCamera.transform.Rotate(new Vector3(0, -1, 0) * rotationMultiplier);
                break;
            // move right
            case 4:
                mainCamera.transform.Rotate(new Vector3(0, 1, 0) * rotationMultiplier);
                break;
            // move bottom-left
            case 5:
                mainCamera.transform.Rotate(new Vector3(-1, -1, 0) * rotationMultiplier);
                break;
            // move bottom
            case 6:
                mainCamera.transform.Rotate(new Vector3(1, 0, 0) * rotationMultiplier);
                break;
            // move bottom-right
            case 7:
                mainCamera.transform.Rotate(new Vector3(-1, -1, 0) * rotationMultiplier);
                break;
        }
    }

    public void MoveCursorRaw (float xRot, float yRot) {
        DebugText.AddDebugText("getting inputs : " + xRot + " , " + yRot);
        mainCamera.transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
        if (xRot > 0) {
            mainCamera.transform.Rotate(new Vector3(1, 0, 0) * rotationMultiplier);
        }
        if (xRot < 0) {
            mainCamera.transform.Rotate(new Vector3(-1, 0, 0) * rotationMultiplier);
        }
        if (yRot > 0) {
            mainCamera.transform.Rotate(new Vector3(0, 1, 0) * rotationMultiplier);
        }
        if (yRot < 0) {
            mainCamera.transform.Rotate(new Vector3(0, -1, 0) * rotationMultiplier);
        }

    }

    public static void ResetCamera () {
        //DebugText.AddDebugText("reset camera");
        mainCamera.transform.rotation = Quaternion.identity;
    }
}
