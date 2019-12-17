using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject       targetInstance;
    public int              cell;                       // the cell in which this environment lies
    public float            currentTime = 0;            // public to watch countdown
    public float            targetTimeLimit = 1f;
    public bool             targetHit = false;
    public bool             doMove = true;

    void Start () {
        //Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update() {
        //if set time has passed, despawn current target and spawn at another place
        if (currentTime > targetTimeLimit && doMove) {
            MoveTarget();
        }

        // if target has been shot down then respawn target
        if (targetHit) {
            MoveTarget();
        }

        // increment timer keeping float
        currentTime += 0.01f;
    }

    /// <summary>
    /// Move the target to a new location on the wall
    /// </summary>
    public void MoveTarget () {
        // reset time and change transform
        currentTime = 0;
        targetInstance.transform.position = getSpawnLocation(cell);
    }

    /// <summary>
    /// Generate a random spot on the wall to which the target should be moved
    /// </summary>
    /// <param name="cell">The index of cell to which this target belongs</param>
    /// <returns></returns>
    private Vector3 getSpawnLocation(int cell) {
        // get necessary multipliers based on the cell
        int rowMultiplier = (int) cell%3;
        int columnMultiplier = (int) cell/3;

        // generate a location
        Vector3 validLocation = new Vector3(Random.Range(-10.0f, 10.0f) + (250 * rowMultiplier), 
                                            Random.Range(13.0f, 2.0f),
                                            -0.6f + (250 * columnMultiplier));
        return validLocation;
    }
}
