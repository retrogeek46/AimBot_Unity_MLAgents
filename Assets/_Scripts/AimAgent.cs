using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class AimAgent : Agent {
    private GameObject targetObject;
    private GameObject mainCamera;
    public Target gameManager;
    private MoveCamera moveCamera;
    private float oldAngle = 0f;
    public float resetTime = 5f;
    private int currentTime = 0;
    private bool timeUp = false;
    float tempAngle = 0f;
    float diff = 0f;
    private bool aiming = false;
    // Start is called before the first frame update
    void Start () {
        targetObject = GameObject.FindGameObjectWithTag("target");
        mainCamera = GameObject.FindObjectOfType<Camera>().gameObject;
        moveCamera = mainCamera.GetComponent<MoveCamera>();
        oldAngle = 180f;
    }

    void LateUpdate () {
        if (currentTime > resetTime) {
            currentTime = 0;
            timeUp = true;
        }
        if (oldAngle == 0) {
            oldAngle = 180f;
        }
        if (targetObject == null) {
            targetObject = GameObject.FindGameObjectWithTag("target");
        }
        float diff = GetAngle(mainCamera, targetObject) - tempAngle;
        if (diff < 0) {
            aiming = true;
        }
        if (diff > 0) {
            aiming = false;
        }
        //DebugText.AddDebugText("" + diff);
        if (currentTime % 50 == 0) {
            tempAngle = GetAngle(mainCamera, targetObject);
        }
    }

    private float GetAngle (GameObject objectA, GameObject objectB) {
        Vector3 targetVector = objectB.transform.position - objectA.transform.position;
        Vector3 crosshairVector = (objectA.transform.forward);

        return Vector3.Angle(targetVector, crosshairVector);
    }
    // Reset environment if target has appeared for n number of times
    // Reset environment if reward does not increase for set number of time
    public override void AgentReset () {
        //DebugText.AddDebugText("resetting");
        MoveCamera.ResetCamera();
        Target.totalTargetsYet = 0;
        gameManager.Respawn();
    }

    public override void CollectObservations () {
        //base.CollectObservations();
        if (targetObject == null) { 
            targetObject = GameObject.FindGameObjectWithTag("target");
        }
        //AddVectorObs(GetAngle(mainCamera, targetObject));
        AddVectorObs(mainCamera.transform.forward);
        AddVectorObs(targetObject.transform.position - mainCamera.transform.position);
    }

    public override void AgentAction (float[] vectorAction) {
        // Actions, size = 3
        float vertical = vectorAction[0];
        float horizontal = vectorAction[1];
        float fire = vectorAction[2];
        //float left = vectorAction[3];
        //float fire = vectorAction[4];
        //DebugText.AddDebugText(horizontal + " , " + vertical);
        //Debug.Log(horizontal + " , " + vertical);
        if (vertical > 0.5) {
            //Debug.Log(vertical + ":A");
            moveCamera.MoveCursor(1);
        }
        else if (vertical < -0.5) {
            //Debug.Log(vertical + ":B");
            moveCamera.MoveCursor(6);
        }
        if (horizontal > 0.5) {
            //Debug.Log(horizontal + ":C");
            moveCamera.MoveCursor(4);
        }
        if (horizontal < -0.5) {
            //Debug.Log(horizontal + ":D");
            moveCamera.MoveCursor(3);
        }
        if (fire > 0.9f) {
            moveCamera.Fire();
        }

        //DebugText.AddDebugText("0: " + vectorAction[0] + " , 1: " + vectorAction[1] + " , 2: " + vectorAction[2] + " , 3: " + vectorAction[3] + " , 4: " + vectorAction[4]);
        //DebugText.AddDebugText("0: " + vectorAction[0] + " , 1: " + vectorAction[1] + " , 2: " + vectorAction[2]);

        if (targetObject == null) {
            targetObject = GameObject.FindGameObjectWithTag("target");
        }

        if (Input.GetKey(KeyCode.U)) {
            DebugText.AddDebugText("oldangle: " + tempAngle + "\n newangle: " + GetAngle(mainCamera, targetObject));
        }

        //DebugText.AddDebugText("aiming is : " + aiming);
        // Rewards 
        // if correctly moving aim towards target
        //if (aiming) {
        //    //DebugText.AddDebugText("oldangle: " + tempAngle + "\n newangle: " + GetAngle(mainCamera, targetObject)); 
        //    //oldAngle = GetAngle(mainCamera, targetObject) + 20f;
        //    AddReward(0.1f);
        //} 
        //else {
        //    AddReward(-0.1f);
        //}

        // if target successfully hit
        //if (Target.targetHit) {
        //    AddReward(1f);
        //    Done();
        //}

        if (timeUp) {
            DebugText.AddDebugText("done cause time");
            timeUp = false;
            Done();
        }

        RaycastHit hit;
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward, Color.red);
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit)) {
            if (hit.transform.tag != "target") {
                if (hit.transform.tag != "wall") {
                    //DebugText.AddDebugText("going out of bounds");
                    AddReward(-0.1f);
                    Done();
                }
            }
            if (hit.transform.tag == "target") {
                SetReward(5f);
                Done();
            }
            //Debug.Log("tag : " + hit.transform.tag);
        }

        //// if reward gets to -10
        //if (GetReward() < -9) {
        //    Done();
        //}

        // if 5 targets missed
        if (Target.totalTargetsYet > 4) {
            Done();
        }
        //DebugText.AddDebugText("Reward" + GetReward());
    }

    public override float[] Heuristic () {
        //// when action = 5
        //var action = new float[5];
        //if (Input.GetKey(KeyCode.W)) {
        //    action[0] = 1;
        //}
        //if (Input.GetKey(KeyCode.D)) {
        //    action[1] = 1;
        //}
        //if (Input.GetKey(KeyCode.S)) {
        //    action[2] = 1;
        //}
        //if (Input.GetKey(KeyCode.A)) {
        //    action[3] = 1;
        //}
        //if (InputManager.firePressed) {
        //    action[4] = 1;
        //} else {
        //    action[4] = 0;
        //}

        // when action = 3
        var action = new float[3];
        
        action[0] = Input.GetAxis("Vertical");
        action[1] = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.F)) {
            action[2] = 1;
        }
        //DebugText.AddDebugText("0: " + action[0] + " , 1: " + action[1] + " , 2: " + action[2]);
        return action;
    }
}
