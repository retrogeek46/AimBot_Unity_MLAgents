using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject targetPrefab;
    public float currentTime = 0;           // public to watch countdown
    public float targetTimeLimit = 1f;
    public static bool targetHit = false;
    public static int totalTargetsYet;

    void Start () {
        //Application.targetFrameRate = 30;
        totalTargetsYet = 1;
    }

    // Update is called once per frame
    void Update() {
        // if set time has passed, despawn current target and spawn at another place
        //if (currentTime > targetTimeLimit) {
        //    Respawn();
        //}
        // if target has been shot down then respawn target
        if (targetHit) {
            targetHit = false;
            Respawn();
        }
        //if (Input.GetKeyDown(KeyCode.P)) {
        //    Debug.Log("paused");
        //    Time.timeScale = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.L)) {
        //    Time.timeScale = 1;
        //}
        //currentTime += 0.01f;
    }

    public void Respawn () {
        currentTime = 0;
        totalTargetsYet++;
        Despawn();
        Spawn();
    }

    private void Despawn() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        for (int i = 0; i < targets.Length; i++) {
            GameObject.Destroy(targets[i]);
        }
    }

    private void Spawn() {
        Vector3 spawnLocation = getSpawnLocation();
        Instantiate(targetPrefab, spawnLocation, Quaternion.identity);
    }

    private Vector3 getSpawnLocation() {
        Vector3 validLocation = new Vector3(Random.Range(-10.0f, 10.0f),  Random.Range(13.0f, 2.0f), -0.6f);
        return validLocation;
    }
}
