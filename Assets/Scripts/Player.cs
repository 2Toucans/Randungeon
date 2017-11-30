using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxSpeed;
    public float movementAccel;
    public float turnAccel;
    public float maxTurnSpeed;
    public float jumpImpulse;

    public float friction;

    public bool useGravity;

    public Shader cameraEffectShader;

    public Camera mainCamera;

    public Light worldLight;

    private float turnSpeed = 0;
    private float vTurnSpeed = 0;
    private bool noclip = false;
    public bool isNight = false;
    public bool flashlightEnabled = false;
    public bool fogEnabled = false;

    private Vector3 velocity;

    private CharacterController controller;

    // Use this for initialization
    void Start () {
	    controller = GetComponent<CharacterController>();
        mainCamera.SetReplacementShader(cameraEffectShader, null);
        Shader.SetGlobalInt("_Night", 0);
        Shader.SetGlobalInt("_FogEnabled", 0);
        Shader.SetGlobalInt("_FlashlightEnabled", 0);
    }

    void Update() {
        if (Input.GetButtonDown("ResetPlayer")) {
            Reset();
        }
        if (Input.GetButtonDown("ResetMaze")) {
            Reset();
        }
        if (Input.GetButtonDown("Jump") && !noclip) {
            Jump();
        }
        else if (Input.GetButton("Jump") && noclip) {
            MoveUp();
        }
        if (Input.GetButton("Crouch") && noclip) {
            MoveDown();
        }
        if (Input.GetAxis("Vertical") > 0.4f) {
            MoveForward();
        }
        else if (Input.GetAxis("Vertical") < -0.4f) {
            MoveBackward();
        }
        else {
            ApplyFriction();
        }
        if (Input.GetAxis("Horizontal") < -0.4f) {
            TurnLeft();
        }
        else if (Input.GetAxis("Horizontal") > 0.4f) {
            TurnRight();
        }
        else {
            if (turnSpeed > 0) {
                turnSpeed -= turnAccel * 3 * Time.deltaTime;
                if (turnSpeed < 0) {
                    turnSpeed = 0;
                }
            }
            else if (turnSpeed < 0) {
                turnSpeed += turnAccel * 3 * Time.deltaTime;
                if (turnSpeed > 0) {
                    turnSpeed = 0;
                }
            }
        }

        // Looking around
        if (Input.GetAxis("Look") > 0) {
            LookUp();
        }
        else if (Input.GetAxis("Look") < 0) {
            LookDown();
        }
        else {
            if (vTurnSpeed > 0) {
                vTurnSpeed -= turnAccel * 3 * Time.deltaTime;
                if (vTurnSpeed < 0) {
                    vTurnSpeed = 0;
                }
            }
            else if (vTurnSpeed < 0) {
                vTurnSpeed += turnAccel * 3 * Time.deltaTime;
                if (vTurnSpeed > 0) {
                    vTurnSpeed = 0;
                }
            }
        }

        // Miscellaneous Buttons
        if (Input.GetButtonDown("NoClip")) {
            ToggleNoclip();
        }

        if (Input.GetButtonDown("ToggleDay")) {
            ToggleDay();
        }

        if (Input.GetButtonDown("ToggleFog")) {
            ToggleFog();
        }

        if (Input.GetButtonDown("ToggleFlashlight")) {
            ToggleFlashlight();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        AddGravity();
        ApplyFriction();
        CapSpeed();

        controller.Move(velocity * Time.deltaTime);

        transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
        AdjustCamera();

        velocity = controller.velocity;
    }

    private void Jump() {
        if (Physics.Raycast(transform.position, -transform.up, 1.25f)) {
            velocity += transform.up * jumpImpulse;
        }
    }

    private void MoveForward() {
        velocity += transform.forward * movementAccel * Time.deltaTime;
    }

    private void MoveBackward() {
        velocity += transform.forward * -1 * movementAccel * Time.deltaTime;
    }

    private void AddGravity() {
        if (useGravity) {
            velocity += Physics.gravity * 0.1f;
        }
    }

    private void TurnLeft() {
        turnSpeed -= turnAccel * Time.deltaTime;
        if (turnSpeed < -maxTurnSpeed) {
            turnSpeed = -maxTurnSpeed;
        }
    }

    private void TurnRight() {
        turnSpeed += turnAccel * Time.deltaTime;
        if (turnSpeed > maxTurnSpeed) {
            turnSpeed = maxTurnSpeed;
        }
    }

    private void MoveUp() {
        velocity += transform.up * movementAccel * Time.deltaTime;
    }

    private void MoveDown() {
        velocity += transform.up * -1 * movementAccel * Time.deltaTime;
    }

    private void ApplyFriction() {
        Vector3 xVel = Vector3.Scale(velocity, new Vector3(1, 0, 1));
        if (velocity.sqrMagnitude > 0) {
            velocity -= xVel.normalized * friction * Time.deltaTime;
            if (Vector3.Dot(Vector3.Scale(velocity, new Vector3(1, 0, 1)), xVel) < 0) {
                velocity = Vector3.Scale(velocity, Vector3.up);
            }
        }
        else {

        }
    }

    private void LookUp() {
        vTurnSpeed -= turnAccel * Time.deltaTime;
        if (vTurnSpeed< -maxTurnSpeed) {
            vTurnSpeed = -maxTurnSpeed;
        }
    }

    private void LookDown() {
        vTurnSpeed += turnAccel * Time.deltaTime;
        if (vTurnSpeed > maxTurnSpeed) {
            vTurnSpeed = maxTurnSpeed;
        }
    }

    private void CapSpeed() {
        float dSpeed = maxSpeed;
        if (Vector3.Scale(velocity, new Vector3(1, 0, 1)).sqrMagnitude > dSpeed * dSpeed) {
            Vector3 vel = velocity.normalized * dSpeed;
            velocity = Vector3.Scale(velocity, Vector3.up)
                + Vector3.Scale(vel, new Vector3(1, 0, 1));
        }
    }

    private void ToggleNoclip() {
        noclip = !noclip;
        controller.detectCollisions = !noclip;
        useGravity = !noclip;
    }

    private void AdjustCamera() {
        mainCamera.transform.localEulerAngles += new Vector3(vTurnSpeed * Time.deltaTime, 0, 0);
        Vector3 angles = mainCamera.transform.localEulerAngles;
    }

    private void Reset() {
        velocity = new Vector3(0, 0, 0);
        mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(2, 0, 2);
        noclip = false;
    }

    private void ToggleDay() {
        isNight = !isNight;
        if (worldLight != null) {
            worldLight.transform.Rotate(new Vector3(isNight ? -70 : 70, 0, 0));
            worldLight.intensity = isNight ? 0 : 1;
        }
        Shader.SetGlobalInt("_Night", isNight ? 1 : 0);
    }

    private void ToggleFog() {
        fogEnabled = !fogEnabled;
        Shader.SetGlobalInt("_FogEnabled", fogEnabled ? 1 : 0);
    }

    private void ToggleFlashlight() {
        flashlightEnabled = !flashlightEnabled;
        Shader.SetGlobalInt("_FlashlightEnabled", flashlightEnabled ? 1 : 0);
    }
}
