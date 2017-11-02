using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxSpeed;
    public float movementAccel;
    public float turnAccel;
    public float maxTurnSpeed;
    public float jumpImpulse;

    public Camera camera;

    private float turnSpeed = 0;
    private float vTurnSpeed = 0;
    private bool noclip = false;

    private Rigidbody body;

    // Use this for initialization
    void Start () {
	    body = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButtonDown("ResetPlayer")) {
            Reset();
        }
        if (Input.GetButtonDown("ResetMaze"))
        {
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
        if (Input.GetAxis("Horizontal") < -0.4f) {
            TurnLeft();
        }
        else if (Input.GetAxis("Horizontal") > 0.4f) {
            TurnRight();
        }
        else {
            if (turnSpeed > 0) {
                turnSpeed -= turnAccel * 3 * Time.fixedDeltaTime;
                if (turnSpeed < 0) {
                    turnSpeed = 0;
                }
            }
            else if (turnSpeed < 0) {
                turnSpeed += turnAccel * 3 * Time.fixedDeltaTime;
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
                vTurnSpeed -= turnAccel * 3 * Time.fixedDeltaTime;
                if (vTurnSpeed < 0) {
                    vTurnSpeed = 0;
                }
            }
            else if (vTurnSpeed < 0) {
                vTurnSpeed += turnAccel * 3 * Time.fixedDeltaTime;
                if (vTurnSpeed > 0) {
                    vTurnSpeed = 0;
                }
            }
        }

        // Miscellaneous Buttons
        if (Input.GetButtonDown("NoClip")) {
            ToggleNoclip();
        }

        CapSpeed();

        transform.Rotate(new Vector3(0, turnSpeed * Time.fixedDeltaTime, 0));
        AdjustCamera();
    }

    private void Jump() {
        if (Physics.Raycast(transform.position, -transform.up, 1.25f)) {
            body.velocity += transform.up * jumpImpulse;
        }
    }

    private void MoveForward() {
        body.velocity += transform.forward * movementAccel * Time.fixedDeltaTime;
    }

    private void MoveBackward() {
        body.velocity += transform.forward * -1 * movementAccel * Time.fixedDeltaTime;
    }

    private void TurnLeft() {
        turnSpeed -= turnAccel * Time.fixedDeltaTime;
        if (turnSpeed < -maxTurnSpeed) {
            turnSpeed = -maxTurnSpeed;
        }
    }

    private void TurnRight() {
        turnSpeed += turnAccel * Time.fixedDeltaTime;
        if (turnSpeed > maxTurnSpeed) {
            turnSpeed = maxTurnSpeed;
        }
    }

    private void MoveUp() {
        body.velocity += transform.up * movementAccel * Time.fixedDeltaTime;
    }

    private void MoveDown() {
        body.velocity += transform.up * -1 * movementAccel * Time.fixedDeltaTime;
    }

    private void LookUp() {
        vTurnSpeed -= turnAccel * Time.fixedDeltaTime;
        if (vTurnSpeed < -maxTurnSpeed) {
            vTurnSpeed = -maxTurnSpeed;
        }
    }

    private void LookDown() {
        vTurnSpeed += turnAccel * Time.fixedDeltaTime;
        if (vTurnSpeed > maxTurnSpeed) {
            vTurnSpeed = maxTurnSpeed;
        }
    }

    private void CapSpeed() {
        if (Vector3.Scale(body.velocity, new Vector3(1, 0, 1)).sqrMagnitude > maxSpeed * maxSpeed) {
            Vector3 vel = body.velocity.normalized * maxSpeed;
            body.velocity = Vector3.Scale(body.velocity, Vector3.up)
                + Vector3.Scale(vel, new Vector3(1, 0, 1));
        }
    }

    private void ToggleNoclip() {
        noclip = !noclip;
        Collider c = GetComponent<Collider>();
        c.enabled = !noclip;
        body.useGravity = !noclip;
    }

    private void AdjustCamera() {
        camera.transform.localEulerAngles += new Vector3(vTurnSpeed * Time.fixedDeltaTime, 0, 0);
        Vector3 angles = camera.transform.localEulerAngles;
    }

    private void Reset() {
        body.velocity = new Vector3(0, 0, 0);
        camera.transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(2, 0, 2);
        noclip = false;
    }
}
