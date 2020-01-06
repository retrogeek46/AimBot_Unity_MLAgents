using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    public static Text  debugTextBox;           // static textbox to pass in the static
    public Text         debugTextBoxSetter;     // reference to the debug textbox object
    public float        resetTime = 100f;       // time after which the textbox is cleared
    public float        currentTime = 0f;       // var to keep track of how much time has passed

    void Start () {
        // get reference to the textbox and pass to the static object
        debugTextBox = debugTextBoxSetter;
    }

    // Update is called once per frame
    void Update () {
        // clear the textbox after some time
        if (currentTime > resetTime) {
            currentTime = 0;
            debugTextBox.text = "";
        }
        currentTime += 0.01f;
    }

    /// <summary>
    /// Add text to the debug textbox and clear the old one
    /// </summary>
    /// <param name="message">The message to show</param>
    public static void AddDebugText (string message) {
        debugTextBox.text = message;
    }

    /// <summary>
    /// Add text to the debug textbox without clearing the old one
    /// </summary>
    /// <param name="message">The message to show</param>
    public static void AppendDebugText (string message) {
        debugTextBox.text += "\n" + message;
    }
}
