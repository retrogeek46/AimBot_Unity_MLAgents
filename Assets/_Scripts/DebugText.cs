using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    public static Text debugTextBox;
    public Text debugTextBoxSetter;
    public float resetTime = 100f;
    public float currentTime = 0f;

    void Start () {
        debugTextBox = debugTextBoxSetter;
    }

    // Update is called once per frame
    void Update () {
        if (currentTime > resetTime) {
            currentTime = 0;
            debugTextBox.text = "";
        }
        currentTime += 0.01f;
    }

    public static void AddDebugText (string message) {
        debugTextBox.text = message;
    }

    public static void AppendDebugText (string message) {
        debugTextBox.text += "\n" + message;
    }
}
