using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyState { 
    left,
    right,
    up,
    down,
    fire,
    released
}

public class InputManager : MonoBehaviour {

    public static bool leftPressed, rightPressed, upPressed, downPressed, firePressed, runPressed, jumpPressed, jumpKeptPressed, pausePressed, interactPressed, anyKeyPressed, movementKeyPressed;
    public bool keyboard, controller, Android;
    public static bool inputActive;
    public bool showLeft, showRight, showJump, showJumpKept, showInteract, showAnyKey;
    public static KeyState currentState;

    /////////////////////////////////////////////////////////////////////////////////////////////////////

    //Reference to joystick gameobject, variables
    //public GameObject BG, LR_Button;
    //static bool onLR_Button;
    //Vector3 originialPos;
    //Touch touch;
    //int i;
    //RaycastHit2D hit;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start() {
#if UNITY_ANDROID
        keyboard = false;
        Android = true;
        controller = false;
        
#endif

#if UNITY_EDITOR
        keyboard = true;
        Android = false;
        controller = false;
#endif

        inputActive = true;
        leftPressed = false;
        rightPressed = false;
        jumpPressed = false;
        jumpKeptPressed = false;
        interactPressed = false;
        pausePressed = false;
        runPressed = false;

        //////////////////////////////////////////////////////////////////////////////////

        //Initializing joystick stuff
        //if (GameObject.Find("LR_Button") != null) {
        //    originialPos = LR_Button.transform.position;
        //}

        //////////////////////////////////////////////////////////////////////////////////
    }

    void Update() {
        if (inputActive) {
            if (keyboard && !Android) {
                //Left button
                if (Input.GetKey(KeyCode.A)) {
                    anyKeyPressed = true;
                    leftPressed = true;
                    currentState = KeyState.left;
                } else leftPressed = false;
                //Right button
                if (Input.GetKey(KeyCode.D)) {
                    anyKeyPressed = true;
                    rightPressed = true;
                    currentState = KeyState.right;
                } else rightPressed = false;
                //Up button
                if (Input.GetKey(KeyCode.W)) {
                    anyKeyPressed = true;
                    upPressed = true;
                    currentState = KeyState.up;
                } else upPressed = false;
                //Down button
                if (Input.GetKey(KeyCode.S)) {
                    anyKeyPressed = true;
                    downPressed = true;
                    currentState = KeyState.down;
                } else downPressed = false;
                //Fire button
                if (Input.GetKeyDown(KeyCode.F)) {
                    anyKeyPressed = true;
                    firePressed = true;
                    currentState = KeyState.fire;
                } else firePressed = false;
                //Jump button
                if (Input.GetKeyDown(KeyCode.Space)) {
                    anyKeyPressed = true;
                    jumpPressed = true;
                } else jumpPressed = false;
                if (Input.GetKey(KeyCode.Space)) {
                    anyKeyPressed = true;
                    jumpKeptPressed = true;
                } else jumpKeptPressed = false;
                //Run button
                if (Input.GetKey(KeyCode.LeftShift)) {
                    anyKeyPressed = true;
                    runPressed = true;
                } else runPressed = false;
                //Interact button
                if (Input.GetKey(KeyCode.E)) {
                    anyKeyPressed = true;
                    interactPressed = true;
                } else interactPressed = false;
                //Pause button
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    anyKeyPressed = true;
                    pausePressed = true;
                } else pausePressed = false;
                //if any movement inducing key is pressed
                if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) {
                    movementKeyPressed = true;
                    anyKeyPressed = false;
                } else movementKeyPressed = false;
                //If no key held down set to false
                if (!Input.anyKey) {
                    anyKeyPressed = false;
                    currentState = KeyState.released;
                }

            }
            //if controller is used
            if (controller && !Android) {
                //Left button
                if (Input.GetAxis("LeftStick") < -0.01) {
                    anyKeyPressed = true;
                    leftPressed = true;
                } else leftPressed = false;
                //Right button
                if (Input.GetAxis("LeftStick") > 0.01) {
                    anyKeyPressed = true;
                    rightPressed = true;
                } else rightPressed = false;
                //Jump button
                if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
                    anyKeyPressed = true;
                    jumpPressed = true;
                } else jumpPressed = false;
                if (Input.GetKey(KeyCode.Joystick1Button1)) {
                    anyKeyPressed = true;
                    jumpKeptPressed = true;
                } else jumpKeptPressed = false;
                //Run button
                if (Input.GetKey(KeyCode.Joystick1Button3)) {
                    anyKeyPressed = true;
                    runPressed = true;
                } else runPressed = false;
                //Interact button
                if (Input.GetKey(KeyCode.Joystick1Button2)) {
                    anyKeyPressed = true;
                    interactPressed = true;
                } else interactPressed = false;
                //Pause button
                if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
                    anyKeyPressed = true;
                    pausePressed = true;
                } else pausePressed = false;
                // If no key held down set to false
                if (!Input.anyKey && Input.GetAxis("LeftStick") == 0) anyKeyPressed = false;
            }
            if (Android) {
                if (jumpPressed == true) {
                    StartCoroutine(SetJumpFalse());
                }
            }
        }

        showAnyKey = anyKeyPressed;
        showLeft = leftPressed;
        showRight = rightPressed;
        showJump = jumpPressed;
        showJumpKept = jumpKeptPressed;
        showInteract = interactPressed;

    }
    //Coroutine to set jumpPressed false after a short period of time
    private IEnumerator SetJumpFalse() {
        yield return new WaitForEndOfFrame();
        jumpPressed = false;
    }
    //Android Specific code, getters for the bools
    public void Left(bool state) {
        leftPressed = state;
    }

    public void Right(bool state) {
        rightPressed = state;
    }

    public void Jump(bool state) {
        jumpPressed = state;
    }

    public void JumpKept(bool state) {
        jumpKeptPressed = state;
    }

    public void Run(bool state) {
        runPressed = state;
    }

    public void Interact(bool state) {
        interactPressed = state;
    }

    public void Pause(bool state) {
        pausePressed = state;
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////

    //functions for joystick touch controls
    //public void OnLR_Button(bool state) {
    //    onLR_Button = state;
    //}
    //public void DragCalled() {
    //    LR_Button.transform.position = Input.mousePosition;
    //    if (LR_Button.transform.position.x < originialPos.x) {
    //        leftPressed = true;
    //        rightPressed = false;
    //    }
    //    if (LR_Button.transform.position.x > originialPos.x) {
    //        leftPressed = false;
    //        rightPressed = true;
    //    }
    //}

    /////////////////////////////////////////////////////////////////////////////////////////////////////
}
