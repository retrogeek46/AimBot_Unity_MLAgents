using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class AimAgent : Agent {
    public GameObject   targetObject;
    public GameObject   gunObject;
    public Target       target;
    private Gun         gunComponent;
    private float       oldAngle = 0f;
    public float        resetTime = 1f;
    private int         currentTime = 0;
    private bool        timeUp = false;
    float               tempAngle = 0f;
    private bool        aiming = false;
    
    // Start is called before the first frame update
    void Start () {
        gunComponent = gunObject.GetComponent<Gun>();
        target = targetObject.GetComponent<Target>();
        tempAngle = GetAngle(gunObject, targetObject);
    }

    void Update () {
        // run timer
        if (currentTime > resetTime) {
            currentTime = 0;
            timeUp = true;
        }

        // check if gun is moving towards or away from target
        if (tempAngle != GetAngle(gunObject, targetObject)) {
            float diff = GetAngle(gunObject, targetObject) - tempAngle;
            if (diff < 0) {
                aiming = true;
            }
            if (diff >= 0) {
                aiming = false;
            }
            tempAngle = GetAngle(gunObject, targetObject);
        } else {
            aiming = false;
        }
        //DebugText.AddDebugText("Dir : " + aiming);

        //Debug.DrawRay(gunObject.transform.position, gunObject.transform.forward * 200f, Color.yellow, 200f);
        //Debug.DrawRay(gunObject.transform.position, targetObject.transform.position - gunObject.transform.position, Color.red, 200f);
    }

    /// <summary>
    /// Calculate angle between the vectors of gun and target
    /// </summary>
    /// <param name="objectA">Gun object</param>
    /// <param name="objectB">Target object</param>
    /// <returns></returns>
    private float GetAngle (GameObject objectA, GameObject objectB) {
        Vector3 targetVector = objectB.transform.position - objectA.transform.position;
        Vector3 crosshairVector = (objectA.transform.forward);

        return Vector3.Angle(targetVector, crosshairVector);
    }

    /// <summary>
    /// Reset the camera rotation and move target to new location on the wall
    /// </summary>
    public override void AgentReset () {
        // reset gun rotation and move target to other location
        gunComponent.ResetCamera();
        target.MoveTarget();
    }

    /// <summary>
    /// The observations collected by the bot are the forward vector of gun and displacement vector to the target to the bot
    /// </summary>
    public override void CollectObservations () {
        // add observations about gun
        AddVectorObs(gunObject.transform.position);
        AddVectorObs(gunObject.transform.rotation);
        // add observations about target
        AddVectorObs(targetObject.transform.position);
        AddVectorObs(targetObject.transform.position - gunObject.transform.position);
    }

    /// <summary>
    /// The bot can take 3 actions, move vertically, horizontally or fire. Currently rewards are not being 
    /// given for firing, only aiming
    /// 
    /// Rewards ar given as followns
    /// +10      -> lining up with the target
    /// +0.1    -> moving towards target
    /// -0.1    -> moving out of wall bounds
    /// -0.001   -> moving away from target
    /// -0.001   -> every frame
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void AgentAction (float[] vectorAction) {
        // Actions, size = 3
        // Based on values, move the aim or fire
        float vertical = vectorAction[0];
        float horizontal = vectorAction[1];
        float fire = vectorAction[2];
        if (vertical > 0.5) {
            gunComponent.MoveGun(1);
        }
        else if (vertical < -0.5) {
            gunComponent.MoveGun(6);
        }
        if (horizontal > 0.5) {
            gunComponent.MoveGun(4);
        }
        if (horizontal < -0.5) {
            gunComponent.MoveGun(3);
        }
        if (fire > 0.9f) {
            gunComponent.Fire();
        }

        // Rewards 
        // if correctly moving aim towards target
        if (aiming) {
            AddReward(0.1f);
        } 
        //else {
        //    AddReward(-0.01f);
        //}

        if (timeUp) {
            DebugText.AddDebugText("done cause time");
            timeUp = false;
            AddReward(-0.5f);
            Done();
        }

        // Limit the aim to only wall
        if (Physics.Raycast(gunObject.transform.position, gunObject.transform.forward, out RaycastHit hit)) {
            if (hit.transform.tag != "target") {
                if (hit.transform.tag != "wall") {
                    //DebugText.AddDebugText("going out of bounds");
                    AddReward(-0.1f);
                    Done();
                }
            }
            if (hit.transform.CompareTag("target")) {
                AddReward(100f);
                Done();
            }
        }

        // encourage bot to finish the level quickly
        AddReward(-0.1f);

        //Debug.Log("Reward" + GetReward());
    }

    public override float[] Heuristic () {
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
